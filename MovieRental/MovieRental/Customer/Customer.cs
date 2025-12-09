using System.ComponentModel.DataAnnotations;
using MovieRental.Controllers.ViewModel;

namespace MovieRental.Customer;

public class Customer
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public Customer()
    {
    }
    
    public Customer(CustomerViewModelRequest vmCustomer)
    {
        Name = vmCustomer.Name;
    }
    
}