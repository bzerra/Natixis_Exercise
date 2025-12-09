using System;
using System.Collections.Generic;
using System.Text;

namespace WPF.App.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int DaysRented { get; set; }
        public EPayment paymentMethod { get; set; }
        public Movie Movie { get; set; }
        public Customer Customer { get; set; }
    }

    public enum EPayment
    {
        MbWay = 0,
        PayPal = 1
    }
}
