using WPF.App.ViewModel.Customer;
using WPF.App.ViewModel.DomainClass;

namespace WPF.App.Service.Customer
{
    internal interface ICustomerService
    {
        DomainClass<Models.Customer> Save(CustomerViewModelRequest vmCustomer);
        DomainClass<IEnumerable<Models.Customer>> GetByName(string nameCustomer);
    }
}
