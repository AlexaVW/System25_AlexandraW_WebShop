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
        // Show Categories
        public static void ShowCategories(WebShopDbContext db)
        {
            Console.WriteLine("Categories");
            foreach (var category in db.Categories)
            {
                Console.WriteLine(("Id: " + category.Id).PadRight(8) + " Category name: " + category.Name);
            }
            Console.WriteLine();
        }

        // Show Products
        public static void ShowProducts(WebShopDbContext db)
        {
            if (db.Products.ToList().Count > 0)
            {
                // Getting the length of the product information
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
                        + "Supplier: " + product.Supplier.PadRight(supplierLength)
                        + "On sale: " + product.IsOnSale.ToString().PadRight(onSaleLength)
                        + "Category Id: " + product.CategoryId);
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No items found");
            }
        }

        public static void ShowCartItems()
        {
            List<CartItem> cartItems = Helpers.GetCartItemsNotPaid();
            if(cartItems.Count > 0)
            {
                int idLength = cartItems.Max(ci => ci.Id.ToString().Length) + 2;
                int amountLength = cartItems.Max(ci => ci.ProductAmount.ToString().Length) + 2;
                int isPaidLength = cartItems.Max(ci => ci.IsPaid.ToString().Length) + 2;
                int productIdLength = cartItems.Max(ci => ci.ProductId.ToString().Length) + 2;
                int productNameLength = cartItems.Max(ci => ci.product.Name.Length) + 2;

                foreach (var cartItem in cartItems)
                {
                    Console.WriteLine("Cart Id: " + cartItem.Id.ToString().PadRight(idLength)
                        + "|Product Id: " + cartItem.ProductId.ToString().PadRight(productIdLength)
                        + cartItem.product.Name.PadRight(productNameLength)
                        + "IsPaid?: " + cartItem.IsPaid.ToString().PadRight(isPaidLength)
                        + "Amount: " + cartItem.ProductAmount.ToString().PadRight(amountLength)
                        + "Price per unit: " + cartItem.product.PricePerUnit + " SEK");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No items found");
            }
        }

        // Show short information about the cart items in checkout
        public static void ShowCartItemsInCheckout()
        {
            List<CartItem> cartItems = Helpers.GetCartItemsNotPaid();
            int amountLength = cartItems.Max(ci => ci.ProductAmount.ToString().Length) + 2;
            int productNameLength = cartItems.Max(ci => ci.product.Name.Length) + 2;

            Console.WriteLine();
            foreach (var cartItem in cartItems)
            {
                Console.WriteLine("Amount: " + cartItem.ProductAmount.ToString().PadRight(amountLength)
                    + cartItem.product.Name.PadRight(productNameLength) 
                    + cartItem.product.PricePerUnit + " SEK");
            }
            Console.WriteLine();
        }

        // Returns a group of DateTime on Order, used for UpdateOrder and DeleteOrder
        public static List<IGrouping<DateTime, Order>> ShowOrderHistoryAndGetOrderNumber(WebShopDbContext db)
        {
            Console.Write("\x1b[3J\x1b[H\x1b[2J"); //Clears console properly. Too much text outside of window view
            Console.Clear();

            // A list with orders that includes cartitem and product. Grouping on their orderdate.
            var orderDateGroups = db.Orders
                .Include(o => o.CartItem)
                .ThenInclude(ci => ci.product)
                .GroupBy(o => o.OrderDate).ToList();

            // Declaring an index that are used for OrderNumber.
            // (Can't use orderId because there are one order per cart item with different OrderIds)
            int index = 0;
            // For each orderGroup (that are grouped on Date)
            foreach (var dateGroup in orderDateGroups)
            {
                Console.WriteLine("ORDERDATE: " + dateGroup.Key);
                Console.WriteLine("Order Number: " + (index + 1) + "\n");

                // Calculating the price for all the items in the group
                double subTotal = dateGroup.Sum(o => o.ItemPrice);

                bool firstRow = true;
                // For each cartitem in dateGroup
                foreach (var item in dateGroup)
                {
                    // Printing one time
                    if (firstRow)
                    {
                        Console.WriteLine(item.CustomerName);
                        Console.WriteLine(item.ShipAdress);
                        Console.WriteLine(item.ShipCountry + "\n");
                        firstRow = false; 
                    }
                    // Printing for every cartitem
                    Console.WriteLine("Product: " + item.CartItem.product.Name +
                        ", PricePerUnit: " + item.CartItem.product.PricePerUnit + " SEK" +
                        ", Amount: " + item.CartItem.ProductAmount + "x");

                }
                Console.WriteLine("\nSubTotal for products: " + subTotal.ToString("N2") + " SEK");
                Console.WriteLine("------------------------------------------------------------");

                index++;
            }
            return orderDateGroups;
        }
    }
}
