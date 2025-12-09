using System.Net;
using MovieRental.Controllers.DomainClass;
using MovieRental.Data;

namespace MovieRental.Movie
{
	public class MovieFeatures : IMovieFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		public MovieFeatures(MovieRentalDbContext movieRentalDb)
		{
			_movieRentalDb = movieRentalDb;
		}
		
		public DomainClass<Movie> Save(Movie movie)
		{
			var classDomain = new DomainClass<Movie>(movie);

			#region Validação

			if (movie.PriceRental <= 0)
				classDomain.MsgsErro.Add("PriceRental", "The value cannot be zero.");

			if (string.IsNullOrEmpty(movie.Title))
				classDomain.MsgsErro.Add("Title", "The value cannot be empty.");

			if (classDomain.Success is false)
			{
				classDomain.AddStatusCode(HttpStatusCode.BadRequest);
				return classDomain;
			}
			
			if (_movieRentalDb.Movies.Any(x => x.Title.Equals(movie.Title)))
				classDomain.MsgsErro.Add("Title", "The value shown is already in the database.");
			
			if (classDomain.Success is false)
			{
				classDomain.AddStatusCode(HttpStatusCode.Conflict);
				return classDomain;
			}


			#endregion

			#region Persistencia

			_movieRentalDb.Movies.Add(movie);
			var dbStatus =_movieRentalDb.SaveChanges();

			if (dbStatus <= 0)
			{
				classDomain.MsgsErro.Add("Database", "There was an error in this operation.");
				classDomain.AddStatusCode(HttpStatusCode.InternalServerError);
			}

			return classDomain;

			#endregion
		}

		// TODO: tell us what is wrong in this method? Forget about the async, what other concerns do you have?
		public DomainClass<List<Movie>> GetAll()
		{
			var classDomain = new DomainClass<List<Movie>>(_movieRentalDb.Movies.ToList());

			return classDomain;
		}


	}
}
