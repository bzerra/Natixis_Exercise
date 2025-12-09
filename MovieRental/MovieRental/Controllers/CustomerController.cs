using Microsoft.AspNetCore.Mvc;
using MovieRental.Controllers.ViewModel;
using MovieRental.Customer;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerFeatures _features;


        public CustomerController(ICustomerFeatures features)
        {
            _features = features;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerViewModelRequest vmCustomer)
        {
            var dataCustomer = new Customer.Customer(vmCustomer);
            
            var result = await _features.Save(dataCustomer);
            
            if(result.Success) return Ok(result);
            else return StatusCode((int) result.Status, result);
        }
        
        [HttpGet]
        public IActionResult Get([FromQuery] string? customerName)
        {
            return Ok(_features.GetByCustomerName(customerName));
        }

	}
}
