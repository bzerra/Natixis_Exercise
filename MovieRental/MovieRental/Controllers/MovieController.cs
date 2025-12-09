using System.Net;
using Microsoft.AspNetCore.Mvc;
using MovieRental.Controllers.ViewModel;
using MovieRental.Movie;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {

        private readonly IMovieFeatures _features;

        public MovieController(IMovieFeatures features)
        {
            _features = features;
        }

        [HttpGet]
        public IActionResult Get()
        {
	        return Ok(_features.GetAll());
        }

        [HttpPost]
        public IActionResult Post([FromBody] MovieViewModelRequest vmMovie)
        {
            var dataMovie = new Movie.Movie(vmMovie);
            var result = _features.Save(dataMovie);
            
            if(result.Success) return Ok(result);
            else return StatusCode((int) result.Status, result);
        }
    }
}
