using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Models
{
    internal class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string ShipAdress { get; set; }
        public string ShipCountry { get; set; }
        public string ShippingMethod { get; set; }
        public string PaymentMethod { get; set; }
        public int SubTotal { get; set; }

        ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    }
}
