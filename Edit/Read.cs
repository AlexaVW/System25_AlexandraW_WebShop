using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        

        public static async Task GetProductsAsync(WebShopDbContext db)
        {
            foreach (var product in await db.Products.ToListAsync())
            {
                Console.WriteLine(product.Id + "\t" + product.Name + "\t" + product.PricePerUnit + "\t" + product.UnitsInStock + "\t" +
                    product.Description + "\t" + product.Supplier + "\t" + product.IsOnSale + "\t" + product.CategoryId);
            }
        }

        public static void WriteCartItems()
        {
            using (var db = new WebShopDbContext())
            {
                List <CartItem> cartItems = Helpers.GetCartItemsNotPayed();

                foreach (var cartItem in cartItems)
                {
                    Console.WriteLine("Id: " + cartItem.Id + "\t" + "Amount: " + cartItem.ProductAmount + 
                        "\t" + "IsPayed?: " + cartItem.IsPayed + "\t" + "Product Id: " + cartItem.ProductId + 
                        "\t" + cartItem.product.Name + "\t" + cartItem.product.PricePerUnit); 
                }
            }
        }

        public static void WriteCartItemsInCheckout()
        {
            using (var db = new WebShopDbContext())
            {
                foreach (var cartItem in db.Cart.Where(c => c.IsPayed == false).Include(c => c.product))
                {
                    Console.WriteLine("Amount: " + cartItem.ProductAmount + "\t"+ cartItem.product.Name + "\t" + cartItem.product.PricePerUnit + " SEK"); 
                }
            }
        }

        

        public static void ReadOrderHistory()
        {
            using (var db = new WebShopDbContext())
            {
                foreach (var order in db.Orders) //gruppera? på datum?
                {
                    //skriv ut över gripande info. Adress namn med mera.
                    Console.WriteLine(order.Id + "\t" + order.OrderDate + "\t" + order.CustomerName + "\t" + 
                        order.ShipAdress + "\t" + order.ShipCountry + "\t" + order.ShippingMethod + "\t" + 
                        order.PaymentMethod + "\t" + order.SubTotal + "\t"); //Få med cartitem id och produktens namn

                    //loopa igenom alla produkt namn och pris i ordern
                }
            }
        }

    }
}
