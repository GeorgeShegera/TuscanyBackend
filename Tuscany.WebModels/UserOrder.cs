using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuscany.WebModels
{
    public class UserOrder(string tourName, DateTime dateTime,
                      string img, string? paymentMethod,
                      decimal price, string status)
    {
        public string TourName { get; set; } = tourName;
        public DateTime DateTime { get; set; } = dateTime;
        public string Img { get; set; } = img;
        public string? PaymentMethod { get; set; } = paymentMethod;
        public decimal Price { get; set; } = price;
        public string Status { get; set; } = status;
    }
}
