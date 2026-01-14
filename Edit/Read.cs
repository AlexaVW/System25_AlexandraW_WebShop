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
                foreach (var cartItem in db.Cart.Include(c=> c.product))
                {
                    Console.WriteLine("Id: " + cartItem.Id + "\t" + "Amount: " + cartItem.ProductAmount + "\t" + "IsPayed?: " + cartItem.IsPayed + "\t" + "Product Id: " + cartItem.ProductId + "\t" + cartItem.product.Name); //Lägga till namnet på produkten?
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
