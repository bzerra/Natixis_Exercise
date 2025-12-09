using MovieRental.Rental;

namespace MovieRental.Controllers.ViewModel;

public class RentalViewModelResponse
{
    public int id { get; set; }
    public int daysRented { get; set; }
    public EPayment paymentMethod { get; set; }
    public Movie.Movie movie { get; set; }
    public Customer.Customer customer { get; set; }

    public RentalViewModelResponse(Rental.Rental rental)
    {
        this.id = rental.Id;
        this.daysRented = rental.DaysRented;
        this.paymentMethod = rental.PaymentMethod;
        this.movie = rental.Movie;
        this.customer = rental.Customer;
    }
}