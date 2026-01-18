using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                var orderDateGroups = db.Orders.Include(o => o.CartItem).ThenInclude(c => c.product).GroupBy(o => o.OrderDate).ToList();
                //var orderNameGroups = db.Orders.GroupBy(o => o.CustomerName).ToList();
                
                foreach (var group in orderDateGroups) 
                {
                    Console.WriteLine("ORDERDATE: " + group.Key);
                    double subTotal = 0;
                    bool firstRow = true;
                    foreach(var order in group)
                    {
                        if(firstRow == true)
                        {
                            Console.WriteLine("Id: " + order.Id);
                            Console.WriteLine("CustomerName: " + order.CustomerName);
                            Console.WriteLine();
                            Console.WriteLine("Address: " + order.ShipAdress + 
                                "\n" + "Country: " + order.ShipCountry + 
                                "\n" + "Shipping Method: " + order.ShippingMethod + 
                                "\n" + "Payment method: " + order.PaymentMethod);
                            firstRow = false;
                        }

                        Console.WriteLine("CartItem Id: " + order.CartItemId +
                            "\n" + "Product name: " + order.CartItem.product.Name +
                            "\n" + "Price: " + order.SubTotal + " SEK");

                        subTotal += order.SubTotal;
                    }
                    Console.WriteLine();
                    Console.WriteLine("Total price for products: " + subTotal);
                    Console.WriteLine("----------------------------------");
                }
            }
        }

    }
}
