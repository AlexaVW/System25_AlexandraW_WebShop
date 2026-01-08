using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop.Edit
{
    internal class Read
    {
        public static void ReadCategories()
        {
            using (var db = new WebShopDbContext())
            {
                foreach (var category in db.Categories)
                {
                    Console.WriteLine(category.Id + "\t" + category.Name);
                }
            }
        }

        public static void ReadProducts()
        {
            using (var db = new WebShopDbContext())
            {
                foreach (var product in db.Products)
                {
                    Console.WriteLine(product.Id + "\t" + product.Name + "\t" + product.PricePerUnit + "\t" + product.UnitsInStock + "\t" +
                        product.Description + "\t" + product.Supplier + "\t" + product.IsOnSale + "\t" + product.CategoryId);
                }
            }
        }

        public static void ReadCartItem()
        {
            using (var db = new WebShopDbContext())
            {
                foreach (var cartItem in db.Cart)
                {
                    Console.WriteLine(cartItem.Id + "\t" + cartItem.ProductAmount + "\t" + cartItem.IsPayed + "\t" + cartItem.ProductId + "\t" + cartItem.product);
                }
            }
        }

        public static void ReadOrderHistory()
        {
            using (var db = new WebShopDbContext())
            {
                foreach (var order in db.Orders)
                {
                    Console.WriteLine(order.Id + "\t" + order.OrderDate + "\t" + order.CustomerName + "\t" + 
                        order.ShipAdress + "\t" + order.ShipCountry + "\t" + order.ShippingMethod + "\t" + 
                        order.PaymentMethod + "\t" + order.SubTotal);
                }
            }
        }

    }
}
