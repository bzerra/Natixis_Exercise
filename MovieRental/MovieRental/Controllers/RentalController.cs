using Microsoft.AspNetCore.Mvc;
using MovieRental.Controllers.ViewModel;
using MovieRental.Rental;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {

        private readonly IRentalFeatures _features;

        public RentalController(IRentalFeatures features)
        {
            _features = features;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RentalViewModelRequest vmRental)
        {
            var dataRental = new Rental.Rental(vmRental);
            var result = await _features.Save(dataRental);
            
            if(result.Success) return Ok(result);
            else return StatusCode((int) result.Status, result);
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? customerName)
        {
            var data = await _features.GetRentalsByCustomerName(customerName);
            return Ok(data);
        }

	}
}
