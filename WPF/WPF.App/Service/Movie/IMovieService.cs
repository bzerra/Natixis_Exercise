using WPF.App.ViewModel.Customer;
using WPF.App.ViewModel.DomainClass;
using WPF.App.ViewModel.Movie;

namespace WPF.App.Service.Customer
{
    internal interface IMovieService
    {
        DomainClass<Models.Movie> Save(MovieViewModelRequest vmMovie);
        DomainClass<IEnumerable<Models.Movie>> GetAll();
    }
}
