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
            if (db.Products != null)
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
            // A list with orders that includes cartitem and product. Grouping on their orderdate.
            var orderDateGroups = GetOrdersGroupedByOrderDate(db);

            for (int i = 0; i < orderDateGroups.Count; i++)
            {
                // Giving an index for the groups so it's possible to print Order Number
                // Because there are one order per cart item it's not possible to print the orderId so we make an OrderNumber instead
                var group = orderDateGroups[i];
                Console.WriteLine("Order Number: " + (i + 1));
                Console.WriteLine("ORDERDATE: " + group.Key); // Prints one orderDate for every order

                PrintOrderHistory(group);
                PrintSubtotal(group);
            }
            return orderDateGroups;
        }

        private static List<IGrouping<DateTime, Order>> GetOrdersGroupedByOrderDate(WebShopDbContext db)
        {
            return db.Orders
                .Include(o => o.CartItem)
                .ThenInclude(ci => ci.product)
                .GroupBy(o => o.OrderDate).ToList();
        }

        private static void PrintOrderHistory(IGrouping<DateTime, Order> group)
        {
            // So that it only prints the order information once
            bool firstRow = true;
            foreach (var order in group)
            {
                // Prints
                if (firstRow)
                {
                    Console.WriteLine("CustomerName: " + order.CustomerName);
                    Console.WriteLine();
                    Console.WriteLine("Address: " + order.ShipAdress +
                        "\n" + "Country: " + order.ShipCountry +
                        "\n" + "Shipping Method: " + order.ShippingMethod +
                        "\n" + "Payment method: " + order.PaymentMethod);
                    firstRow = false;
                }
                // Printing for every cart item
                Console.WriteLine("CartItem Id: " + order.CartItemId +
                    "\n" + "Product name: " + order.CartItem.product.Name + " " + order.CartItem.ProductAmount + "x" +
                    "\n" + "Price: " + order.ItemPrice + " SEK");
            }
        }

        private static void PrintSubtotal(IGrouping<DateTime, Order> group)
        {
            // Calculating the price for the Items in the group
            double subTotal = group.Sum(g => g.ItemPrice);

            Console.WriteLine();
            Console.WriteLine("Total price for products: " + subTotal.ToString("N2"));
            Console.WriteLine("----------------------------------");
        }
    }
}
