using System.ComponentModel.DataAnnotations;
using MovieRental.Controllers.ViewModel;

namespace MovieRental.Movie
{
	public class Movie
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		public double PriceRental { get; set; }

		public Movie(MovieViewModelRequest vmMovie)
		{
			Title = vmMovie.Title;
			PriceRental = vmMovie.PriceRental;
		}

		public Movie()
		{
		}
	}
}
