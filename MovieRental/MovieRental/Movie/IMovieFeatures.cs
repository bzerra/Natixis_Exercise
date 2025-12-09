using MovieRental.Controllers.DomainClass;

namespace MovieRental.Movie;

public interface IMovieFeatures
{
	DomainClass<Movie> Save(Movie movie);
	DomainClass<List<Movie>> GetAll();
}