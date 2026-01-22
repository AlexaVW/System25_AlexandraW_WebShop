using System;
using System.Collections.Generic;
using System.Drawing;
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
            int idLength = db.Products.Max(p => p.Id.ToString().Length) + 2;
            int nameLength = db.Products.Max(p => p.Name.Length) + 2;
            int priceLength = db.Products.Max(p => p.PricePerUnit.ToString().Length) + 2;
            int stockLength = db.Products.Max(p => p.UnitsInStock.ToString().Length) + 3;
            int descriptionLength = db.Products.Max(p => p.Description.Length) + 3;
            int supplierLength = db.Products.Max(p => p.Supplier.Length) + 2;
            int onSaleLength = db.Products.Max(p => p.IsOnSale.ToString().Length) + 2;
            
            
            Console.WriteLine("Products");
            foreach (var product in db.Products.ToList())
            {
                Console.WriteLine("Id: " + product.Id.ToString().PadRight(idLength)
                    + product.Name.PadRight(nameLength)
                    + "Price: " + product.PricePerUnit.ToString().PadRight(priceLength)
                    + "In stock: " + product.UnitsInStock.ToString().PadRight(stockLength) 
                    + product.Description.PadRight(descriptionLength)
                    + "Supplier: "+ product.Supplier.PadRight(supplierLength)
                    + "On sale: " + product.IsOnSale.ToString().PadRight(onSaleLength) 
                    + "Category Id: " + product.CategoryId);
            }
            Console.WriteLine();
        }

        public static void ShowCartItems()
        {
            List<CartItem> cartItems = Helpers.GetCartItemsNotPayed();
            try
            {
                int idLength = cartItems.Max(ci => ci.Id.ToString().Length) + 2;
                int amountLength = cartItems.Max(ci => ci.ProductAmount.ToString().Length) + 2;
                int isPayedLength = cartItems.Max(ci => ci.IsPayed.ToString().Length) + 2;
                int productIdLength = cartItems.Max(ci => ci.ProductId.ToString().Length) + 2;
                int productNameLength = cartItems.Max(ci => ci.product.Name.Length) + 2;

                foreach (var cartItem in cartItems)
                {
                    Console.WriteLine("Id: " + cartItem.Id.ToString().PadRight(idLength)
                        + "Amount: " + cartItem.ProductAmount.ToString().PadRight(amountLength)
                        + "IsPayed?: " + cartItem.IsPayed.ToString().PadRight(isPayedLength)
                        + "Product Id: " + cartItem.ProductId.ToString().PadRight(productIdLength)
                        + cartItem.product.Name.PadRight(productNameLength)
                        + "Price per unit: " + cartItem.product.PricePerUnit + " SEK");
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("No item found");
                Console.WriteLine(ex.Message);
            }
        }

        public static void ShowCartItemsInCheckout()
        {
            List<CartItem> cartItems = Helpers.GetCartItemsNotPayed();
            int amountLength = cartItems.Max(ci => ci.ProductAmount.ToString().Length) + 2;
            int productNameLength = cartItems.Max(ci => ci.product.Name.Length) + 2;

            foreach (var cartItem in cartItems)
            {
                Console.WriteLine("Amount: " + cartItem.ProductAmount.ToString().PadRight(amountLength) 
                    + cartItem.product.Name.PadRight(productNameLength) 
                    + cartItem.product.PricePerUnit + " SEK");
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

                }
                Console.WriteLine();
                Console.WriteLine("Total price for products: " + subTotal);
                Console.WriteLine("----------------------------------");
            }

        }

    }
}
