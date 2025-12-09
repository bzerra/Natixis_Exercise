using MovieRental.Controllers.DomainClass;

namespace MovieRental.Rental;

public interface IRentalFeatures
{
	Task<DomainClass<Rental>> Save(Rental rental);
	Task<DomainClass<IEnumerable<Rental>>> GetRentalsByCustomerName(string customerName);
}