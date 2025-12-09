using WPF.App.ViewModel.Customer;
using WPF.App.ViewModel.DomainClass;
using WPF.App.ViewModel.Movie;

namespace WPF.App.Service.Rental
{
    internal interface IRentalService
    {
        DomainClass<Models.Rental> Save(RentalViewModelRequest vmRental);
        DomainClass<IEnumerable<Models.Rental>> GetByNameCustom(string name);
    }
}
