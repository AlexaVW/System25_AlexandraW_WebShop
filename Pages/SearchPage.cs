using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Webshop.Edit;
using Webshop.Models;

namespace Webshop.Pages
{
    internal class SearchPage
    {
        public static void SearchProduct()
        {
            Console.Write("Search product: ");
            string searchString = Console.ReadLine().ToUpper();
            Console.WriteLine();
            using (var db = new WebShopDbContext())
            {
                var selectedProductName = db.Products.Include(p => p.Category).Where(p => p.Name.Contains(searchString)).ToList();
                var selectedProductDescription = db.Products.Include(p => p.Category).Where(p => p.Description.Contains(searchString)).ToList();
                var selectedSupplier = db.Products.Include(p => p.Category).Where(p => p.Supplier.Contains(searchString)).ToList();

                //Lägger alla produktsökningar i samma lista
                var productsSearch = new List<Product>();
                productsSearch.AddRange(selectedProductName);
                productsSearch.AddRange(selectedProductDescription);
                productsSearch.AddRange(selectedSupplier);
                productsSearch = productsSearch.Distinct().ToList();
                
                for(int i = 0; i < productsSearch.Count; i++)
                {
                    if (Helpers.GetChars()[i] != -1) //Ifall index inte är -1
                    {
                        string pressKey = $"Press [" + Helpers.GetChars()[i] + "] To Buy";
                        Console.WriteLine(productsSearch[i].Name);
                        Console.WriteLine(productsSearch[i].Category.Name);
                        Console.WriteLine(productsSearch[i].Description);
                        Console.WriteLine(productsSearch[i].PricePerUnit + " SEK");
                        Console.WriteLine(pressKey);
                        Console.WriteLine();
                    }
                }
                string key = Console.ReadKey(true).KeyChar.ToString().ToUpper();
                Helpers.AddProductToCart(productsSearch, key);
                Console.Clear();
            }
        }
    }
}
