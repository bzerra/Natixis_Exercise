using System;
using System.Collections.Generic;
using System.Text;

namespace WPF.App.ViewModel.Movie
{
    public  class RentalViewModelRequest
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public int paymentMethod { get; set; }
        public int DaysRented { get; set; }
    }
}
