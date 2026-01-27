using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Webshop.Models;

namespace Webshop.Pages
{
    internal class Statistics
    {
        // Statistics (Admin)
        public static void GetBestSellingProducts()
        {
            using (var db = new Connections.WebShopDbContext())
            {
                // Gets cart items that are paid
                var cartItems = db.CartItems
                    .Include(ci => ci.product).ThenInclude(p => p.Category)
                    .Where(ci => ci.IsPaid == true).ToList();

                // Grouping on ProductId
                var productGroups = cartItems.GroupBy(ci => ci.ProductId);

                // Order the Cart Items (that are grouped on ProductId) by decending. Calculating the sum of ProductAmount.
                productGroups = productGroups.OrderByDescending(group => group.Sum(ci => ci.ProductAmount));

                // Selecting the first 10
                productGroups = productGroups.Take(10);

                // Printing the best selling products
                PrintBestSellingProducts(productGroups);
            }
        }

        public static void GetBestSellingCategories()
        {
            using (var db = new Connections.WebShopDbContext())
            {
                // Gets cart items that are paid
                var cartItems = db.CartItems
                    .Include(ci => ci.product).ThenInclude(p => p.Category)
                    .Where(ci => ci.IsPaid == true).ToList();

                // Grouping on the products CategoryId
                var productGroups = cartItems.GroupBy(ci => ci.product.CategoryId);

                // Order the Cart Items (that are grouped on CategoryId) by decending. Calculating the sum of ProductAmount.
                productGroups = productGroups.OrderByDescending(group => group.Sum(ci => ci.ProductAmount));

                // Selecting the first 10
                productGroups = productGroups.Take(10);
                
                // Printing the best selling categories
                PrintBestSellingCategories(productGroups);
            }
        }

        public static void GetBestSellingHay()
        {
            using (var db = new Connections.WebShopDbContext())
            {
                // Gets cart items that are paid
                var cartItems = db.CartItems
                    .Include(ci => ci.product).ThenInclude(p => p.Category)
                    .Where(ci => ci.IsPaid == true).ToList();

                // Gets products that has the string "Hay" in product name. Grouping by ProductId
                var productGroups = cartItems.Where(ci => ci.product.Name.Contains("Hay") && ci.product.Category.Name == "Food").GroupBy(ci => ci.ProductId);

                // Order the Cart Items (that are grouped on ProductId) by decending. Calculating the sum of ProductAmount.
                productGroups = productGroups.OrderByDescending(group => group.Sum(ci => ci.ProductAmount));

                // Selecting the first 10
                productGroups = productGroups.Take(10);

                // Printing best selling hay
                PrintBestSellingHay(productGroups);
            }
        }
        
        public static void GetSalesOrderedBySupplier()
        {
            using (var db = new Connections.WebShopDbContext())
            {
                // Gets cart items that are paid
                var cartItems = db.CartItems
                    .Include(ci => ci.product)
                    .Where(ci => ci.IsPaid == true).ToList();

                // Grouping cart items on Supplier
                var supplierGroups = cartItems.GroupBy(ci => ci.product.Supplier).ToList();

                // Order the Cart Items (that are grouped on Supplier) by decending. Calculating the sum of ProductAmount
                supplierGroups = supplierGroups.OrderByDescending(group => group.Sum(ci => ci.ProductAmount)).ToList();

                // Selecting the first 10
                supplierGroups = supplierGroups.Take(10).ToList();

                // Printing supplier with best sales
                PrintSalesOrderedBySupplier(supplierGroups);
            }
        }

        public static void PrintBestSellingProducts(IEnumerable<IGrouping<int, CartItem>> groups)
        {
            // Showing the product name,
            // amount sold of that product and
            // total amount earned from that product

            Console.WriteLine("Best selling products");
            Console.WriteLine();
            foreach (var group in groups)
            {
                string productName = group.First().product.Name;
                int amountSold = group.Sum(ci => ci.ProductAmount);
                double pricePerUnit = group.First().product.PricePerUnit;
                double amountEarned = pricePerUnit * amountSold;

                Console.WriteLine($"Product: {productName}");
                Console.WriteLine($"Amount sold: {amountSold} | Amount earned: {amountEarned.ToString("N2")} SEK");
                Console.WriteLine();
            }
        }

        public static void PrintBestSellingCategories(IEnumerable<IGrouping<int, CartItem>> groups)
        {
            Console.WriteLine("Best selling categories");
            Console.WriteLine();
            foreach (var group in groups)
            {
                string categoryName = group.First().product.Category.Name;
                int amountSold = group.Sum(ci => ci.ProductAmount);

                Console.WriteLine($"Category: {categoryName}");
                Console.WriteLine($"Amount sold: {amountSold}");
                Console.WriteLine();
            }
        }

        public static void PrintBestSellingHay(IEnumerable<IGrouping<int, CartItem>> groups)
        {
            Console.WriteLine("Best selling hay");
            Console.WriteLine();
            foreach (var group in groups)
            {
                string productName = group.First().product.Name;
                int amountSold = group.Sum(ci => ci.ProductAmount);
                double pricePerUnit = group.First().product.PricePerUnit;
                double amountEarned = pricePerUnit * amountSold;

                Console.WriteLine($"Product: {productName}");
                Console.WriteLine($"Amount sold: {amountSold} | Amount earned: {amountEarned.ToString("N2")} SEK");
                Console.WriteLine();
            }
        }

        public static void PrintSalesOrderedBySupplier(List<IGrouping<string, CartItem>> groups)
        {
            Console.WriteLine("Sales ordered by supplier");
            Console.WriteLine();
            foreach (var group in groups)
            {
                string supplierName = group.Key; // supplierName = the grouping of Supplier
                int amountProductsSold = group.Sum(ci => ci.ProductAmount);
                double pricePerUnit = group.First().product.PricePerUnit;
                double amountEarnedFromProducts = pricePerUnit * amountProductsSold;

                Console.WriteLine($"Supplier: {supplierName}");
                Console.WriteLine($"Amount sold: {amountProductsSold} | Amount earned: {amountEarnedFromProducts.ToString("N2")} SEK");
                Console.WriteLine();
            }
        }
    }
}
