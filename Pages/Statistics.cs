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
        public static void PrintBestSellingProducts()
        {
            Console.WriteLine("Best selling products");
            Console.WriteLine();
            using (var db = new Connections.WebShopDbContext())
            {
                //Hämta cartItems som är betalda
                var cartItems = db.CartItems
                    .Include(ci => ci.product).ThenInclude(p => p.Category)
                    .Where(ci => ci.IsPayed == true).ToList();

                //Grupperar på produkt id
                var productGroups = cartItems.GroupBy(ci => ci.ProductId);

                //Sorterar innan take
                productGroups = productGroups.OrderByDescending(group => group.Sum(ci => ci.ProductAmount));

                //Väljer ut de 10 första
                productGroups = productGroups.Take(10);

                foreach (var group in productGroups) 
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
            
        }

        public static void PrintMostPopularCategories()
        {
            
            Console.WriteLine("Best selling categories");
            Console.WriteLine();
            using (var db = new Connections.WebShopDbContext())
            {
                //Hämta cartItems som är betalda
                var cartItems = db.CartItems
                    .Include(ci => ci.product).ThenInclude(p => p.Category)
                    .Where(ci => ci.IsPayed == true).ToList();

                //Grupperar på kategori id
                var productGroups = cartItems.GroupBy(ci => ci.product.CategoryId);

                //Sorterar innan take. Sorterar på summan av gruppens mängd sålda produkter
                productGroups = productGroups.OrderByDescending(group => group.Sum(ci => ci.ProductAmount));

                //Väljer ut de 10 första
                productGroups = productGroups.Take(10);

                foreach (var group in productGroups)
                {
                    string categoryName = group.First().product.Category.Name;
                    int amountSold = group.Sum(ci => ci.ProductAmount); //Summera gruppens productAmount

                    Console.WriteLine($"Category: {categoryName}");
                    Console.WriteLine($"Amount sold: {amountSold}");
                    Console.WriteLine();
                }
            }
        }

        public static void PrintMostPopularHay()
        {
            Console.WriteLine("Best selling hay");
            Console.WriteLine();
            using (var db = new Connections.WebShopDbContext())
            {
                //Hämta cartItems som är betalda
                var cartItems = db.CartItems
                    .Include(ci => ci.product).ThenInclude(p => p.Category)
                    .Where(ci => ci.IsPayed == true).ToList();

                //Grupperar på produkt id
                var productGroups = cartItems.Where(ci => ci.product.Name.Contains("Hay")).GroupBy(ci => ci.ProductId);

                //Sorterar innan take
                productGroups = productGroups.OrderByDescending(group => group.Sum(ci => ci.ProductAmount));

                //Väljer ut de 10 första
                productGroups = productGroups.Take(10);

                foreach (var group in productGroups)
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
        }

        
        public static void PrintSalesOrderedBySupplier()
        {
            Console.WriteLine("Sales ordered by supplier");
            Console.WriteLine();
            using (var db = new Connections.WebShopDbContext())
            {
                //Hämta cartItems som är betalda
                var cartItems = db.CartItems
                    .Include(ci => ci.product)
                    .Where(ci => ci.IsPayed == true).ToList();

                //Grupperar på Supplier
                var supplierGroups = cartItems
                    .GroupBy(ci => ci.product.Supplier);

                //Sorterar innan take
                supplierGroups = supplierGroups.OrderByDescending(group => group.Sum(ci => ci.ProductAmount));

                //Väljer ut de 10 första
                supplierGroups = supplierGroups.Take(10).ToList();

                foreach (var group in supplierGroups)
                {
                    string supplierName = group.Key;
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
    
}
