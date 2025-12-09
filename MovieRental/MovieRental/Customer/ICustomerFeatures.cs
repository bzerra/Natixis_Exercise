using MovieRental.Controllers.DomainClass;

namespace MovieRental.Customer;

public interface ICustomerFeatures
{
    DomainClass<List<Customer>> GetByCustomerName(string customerName);
    
    Task<DomainClass<Customer>> Save(Customer customer);
}