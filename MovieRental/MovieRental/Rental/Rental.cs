using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieRental.Controllers.ViewModel;

namespace MovieRental.Rental
{
	public class Rental
	{
		[Key]
		public int Id { get; set; }
		public int DaysRented { get; set; }
		public Movie.Movie? Movie { get; set; }
		public Customer.Customer? Customer { get; set; }


		[ForeignKey("Movie")]
		public int MovieId { get; set; }

		public EPayment PaymentMethod { get; set; }

		// TODO: we should have a table for the customers
		//public string CustomerName { get; set; }
		
		[ForeignKey("Customer")]
		public int CustomerId { get; set; }

		public Rental()
		{
		}
		
		public Rental(RentalViewModelRequest vmRental)
		{
			DaysRented = vmRental.daysRented;
			PaymentMethod = vmRental.paymentMethod;
			MovieId = vmRental.movieId;
			CustomerId = vmRental.customerId;
		}
	}

	public enum EPayment
	{
		MbWay = 0,
		PayPal = 1
	}
}
