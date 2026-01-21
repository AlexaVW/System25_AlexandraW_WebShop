using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Webshop.Connections;
using Webshop.Models;

namespace Webshop.Edit
{
    internal class Read
    {
        public static void ShowCategories(WebShopDbContext db)
        {
            Console.WriteLine("Categories");
            foreach (var category in db.Categories)
            {
                Console.WriteLine(("Id: " + category.Id).PadRight(8) + " Category name: " + category.Name);
            }
            Console.WriteLine();

        }

        public static void ShowProducts(WebShopDbContext db)
        {
            Console.WriteLine("Products");
            foreach (var product in db.Products.ToList())
            {
                Console.WriteLine("Id: " + product.Id + "\t" + product.Name + "\t" + product.PricePerUnit + " SEK" + "\t" + "In stock: " + product.UnitsInStock + "\t" +
                    product.Description + "\t" + "Supplier: "+ product.Supplier + "\t" + "On sale: " + product.IsOnSale + "\t" + "Category Id: " + product.CategoryId);
            }
            Console.WriteLine();
        }

        public static void ShowCartItems(WebShopDbContext db)
        {
            List<CartItem> cartItems = Helpers.GetCartItemsNotPayed();

            foreach (var cartItem in cartItems)
            {
                Console.WriteLine("Id: " + cartItem.Id + "\t" + "Amount: " + cartItem.ProductAmount +
                    "\t" + "IsPayed?: " + cartItem.IsPayed + "\t" + "Product Id: " + cartItem.ProductId +
                    "\t" + cartItem.product.Name + "\t" + cartItem.product.PricePerUnit);
            }
            Console.WriteLine();
        }

        public static void ShowCartItemsInCheckout(WebShopDbContext db)
        {
            foreach (var cartItem in db.CartItems.Where(c => c.IsPayed == false).Include(c => c.product))
            {
                Console.WriteLine("Amount: " + cartItem.ProductAmount + "\t" + cartItem.product.Name + "\t" + cartItem.product.PricePerUnit + " SEK");
            }
            Console.WriteLine();
        }

        public static void ShowOrderHistory(WebShopDbContext db)
        {
            var orderDateGroups = db.Orders.Include(o => o.CartItem).ThenInclude(c => c.product).GroupBy(o => o.OrderDate).ToList();

            foreach (var group in orderDateGroups)
            {
                Console.WriteLine("ORDERDATE: " + group.Key);
                double subTotal = group.Sum(g => g.ItemPrice);
                bool firstRow = true;
                foreach (var order in group)
                {
                    if (firstRow == true)
                    {
                        Console.WriteLine("CustomerName: " + order.CustomerName);
                        Console.WriteLine();
                        Console.WriteLine("Address: " + order.ShipAdress +
                            "\n" + "Country: " + order.ShipCountry +
                            "\n" + "Shipping Method: " + order.ShippingMethod +
                            "\n" + "Payment method: " + order.PaymentMethod);
                        firstRow = false;
                    }

                    Console.WriteLine("CartItem Id: " + order.CartItemId +
                        "\n" + "Product name: " + order.CartItem.product.Name + " " + order.CartItem.ProductAmount + "x" +
                        "\n" + "Price: " + order.ItemPrice + " SEK");

                    //subTotal += order.ItemPrice;
                }
                Console.WriteLine();
                Console.WriteLine("Total price for products: " + subTotal);
                Console.WriteLine("----------------------------------");
            }

        }

    }
}
