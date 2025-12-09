using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.App.Models;
using WPF.App.Service.Customer;
using WPF.App.Service.Rental;
using WPF.App.ViewModel.Customer;
using WPF.App.ViewModel.Movie;

namespace WPF.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICustomerService _customerService;
        private IMovieService _movieService;
        private IRentalService _rentalService;

        public MainWindow()
        {
            InitializeComponent();
            VisibilityHidderAll();

            _customerService = new CustomerService();
            _movieService = new MovieService();
            _rentalService = new RentalService();

        }

        private void VisibilityHidderAll()
        {
            CustomerArea.Visibility = Visibility.Hidden;
            CustomCreate.Visibility = Visibility.Hidden;

            MovieArea.Visibility = Visibility.Hidden;
            MovieCreate.Visibility = Visibility.Hidden;

            RentalArea.Visibility = Visibility.Hidden;
            RenatalCreate.Visibility = Visibility.Hidden;
        }

        private void Button_Click_Customer(object sender, RoutedEventArgs e)
        {
            VisibilityHidderAll();
            CustomerArea.Visibility = Visibility.Visible;
            CustomCreate.Visibility = Visibility.Visible;            

            var dataList = _customerService.GetByName("");
            CustomerList.ItemsSource = dataList.Entity;
        }

        private void Button_Click_Movies(object sender, RoutedEventArgs e)
        {
            VisibilityHidderAll();
            MovieArea.Visibility = Visibility.Visible;
            MovieCreate.Visibility = Visibility.Visible;

            // Carregar a lista
            var dataMovies = _movieService.GetAll();

            MovieList.ItemsSource = dataMovies.Entity ;
        }

        private void Button_Click_Rentals(object sender, RoutedEventArgs e)
        {
            VisibilityHidderAll();
            RentalArea.Visibility = Visibility.Visible;
            RenatalCreate.Visibility = Visibility.Visible;

            // Carregar a lista
            var data = _rentalService.GetByNameCustom("");
            RentalList.ItemsSource = data.Entity;

            CbMovie.ItemsSource = _movieService.GetAll().Entity;
            CbCustomer.ItemsSource = _customerService.GetByName("").Entity;
        }

        private void BtnCadastrarCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = _customerService.Save(new CustomerViewModelRequest() { Name = TxtCustomerName.Text });
                if(result.Status != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show($"System Message. {string.Join(Environment.NewLine, result.MsgsErro.Select(kvp => $"{kvp.Key}: {kvp.Value}")) }");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error in this operation. {ex.Message}");                
            }
            

            var dataList = _customerService.GetByName("");
            CustomerList.ItemsSource = dataList.Entity;
        }

        private void BtnCadastrarMovie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = _movieService.Save(new MovieViewModelRequest() { Title = TxtMovieTitle.Text, PriceRental = double.Parse(TxtMoviePrice.Text) });
                if (result.Status != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show($"System Message. {string.Join(Environment.NewLine, result.MsgsErro.Select(kvp => $"{kvp.Key}: {kvp.Value}"))}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error in this operation. {ex.Message}");
            }
            
            var dataMovies = _movieService.GetAll();
            MovieList.ItemsSource = dataMovies.Entity;
        }

        private void BtnCadastrarRental_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = _rentalService.Save(new RentalViewModelRequest() { CustomerId = int.Parse(CbCustomer.SelectedValue.ToString()), MovieId = int.Parse(CbMovie.SelectedValue.ToString()), paymentMethod = int.Parse(((ComboBoxItem) CbPaymentType.SelectedItem).Tag.ToString()), DaysRented = int.Parse(TxtRentalDays.Text)  });
                if (result.Status != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show($"System Message. {string.Join(Environment.NewLine, result.MsgsErro.Select(kvp => $"{kvp.Key}: {kvp.Value}"))}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error in this operation. {ex.Message}");
            }

            var data = _rentalService.GetByNameCustom("");
            RentalList.ItemsSource = data.Entity;
        }        
    }
}