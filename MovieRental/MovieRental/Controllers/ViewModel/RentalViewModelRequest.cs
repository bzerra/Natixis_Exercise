using MovieRental.Rental;

namespace MovieRental.Controllers.ViewModel;

public class RentalViewModelRequest
{
    public int daysRented { get; set; }
    public EPayment paymentMethod { get; set; }
    public int movieId { get; set; }
    public int customerId { get; set; }      

}