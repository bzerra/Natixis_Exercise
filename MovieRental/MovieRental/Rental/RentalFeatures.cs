using System.Net;
using Microsoft.EntityFrameworkCore;
using MovieRental.Controllers.DomainClass;
using MovieRental.Data;
using MovieRental.PaymentProviders;

namespace MovieRental.Rental
{
	public class RentalFeatures : IRentalFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		public RentalFeatures(MovieRentalDbContext movieRentalDb)
		{
			_movieRentalDb = movieRentalDb;
		}

		//TODO: make me async :(
		public async Task<DomainClass<Rental>> Save(Rental rental)
		{
			var classDomain = new DomainClass<Rental>(rental);

			#region Validação

			#region Entity

			if (rental.DaysRented <= 0)
				classDomain.MsgsErro.Add("DaysRented", "The value cannot be zero.");
			
			if (new[] { EPayment.PayPal, EPayment.MbWay }.Contains(rental.PaymentMethod) is false)
				classDomain.MsgsErro.Add("EPayment", "Select a valid payment method.");

			if (classDomain.Success is false)
			{
				classDomain.AddStatusCode(HttpStatusCode.BadRequest);
				return classDomain;
			}

			#endregion

			#region Rule Table

			if (await _movieRentalDb.Rentals.AnyAsync(x => x.MovieId.Equals(rental.MovieId) && x.CustomerId.Equals(rental.CustomerId)))
				classDomain.MsgsErro.Add("Rental", "The customer cannot make the same rental twice.");
			
			if (classDomain.Success is false)
			{
				classDomain.AddStatusCode(HttpStatusCode.Conflict);
				return classDomain;
			}

			#endregion

			#region Rule Payment

			var dataMovie = await _movieRentalDb.Movies.FirstOrDefaultAsync(x => x.Id.Equals(rental.MovieId));
			
			if (rental.PaymentMethod.Equals(EPayment.MbWay))
			{
				if (await new MbWayProvider().Pay(dataMovie.PriceRental) is false)
					classDomain.MsgsErro.Add("Payment", "There is an error in the payment process.(MbWay)");
			}
			else if (rental.PaymentMethod.Equals(EPayment.PayPal))
			{
				if (await new PayPalProvider().Pay(dataMovie.PriceRental) is false)
					classDomain.MsgsErro.Add("Payment", "There is an error in the payment process. (PayPal)");
			}
			
			if (classDomain.Success is false)
			{
				classDomain.AddStatusCode(HttpStatusCode.PaymentRequired);
				return classDomain;
			}

			#endregion
            
			#endregion

			#region Persistencia

			_movieRentalDb.Rentals.Add(rental);
			var dbStatus = await _movieRentalDb.SaveChangesAsync();

			if (dbStatus <= 0)
			{
				classDomain.MsgsErro.Add("Database", "There was an error in this operation.");
				classDomain.AddStatusCode(HttpStatusCode.InternalServerError);
			}

			return classDomain;

			#endregion
			
		}

		//TODO: finish this method and create an endpoint for it
		public async Task<DomainClass<IEnumerable<Rental>>> GetRentalsByCustomerName(string customerName)
		{
			IEnumerable<Rental> data;

            if (string.IsNullOrEmpty(customerName))
			{
                data = await _movieRentalDb.Rentals
                .Include(x => x.Customer).Include(x => x.Movie).ToListAsync();
            }
			else
			{
                data = await _movieRentalDb.Rentals
                .Include(x => x.Customer).Include(x => x.Movie)
                .Where(x => x.Customer.Name.Equals(customerName)).ToListAsync();
            }			
			
			return new DomainClass<IEnumerable<Rental>>(data);
		}

	}
}
