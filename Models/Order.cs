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
        public double ItemPrice { get; set; }
        public int CartItemId { get; set; }

        public CartItem CartItem { get; set; } //Ett cartitem har en order

        public Order(string customerName, string shipAdress, string shipCountry, string shippingMethod, string paymentMethod, double subTotal, int cartItemId)
        {
            CustomerName = customerName;
            ShipAdress = shipAdress;
            ShipCountry = shipCountry;
            ShippingMethod = shippingMethod;
            PaymentMethod = paymentMethod;
            ItemPrice = subTotal;
            CartItemId = cartItemId;
        }
        public Order()
        {
            
        }

    }
}
