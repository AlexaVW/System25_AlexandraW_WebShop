using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop.Edit
{
    internal class Create
    {
        public static void CreateCategory()
        {
            using (var db = new WebShopDbContext())
            {
                Console.WriteLine("Categories:");
                Read.ReadCategories();
                Console.WriteLine("Enter category name:");
                string categoryName = Console.ReadLine();

                Category newCategory = new Category(categoryName);
                db.Categories.Add(newCategory);
                db.SaveChanges();
            }
            Console.Clear();
        }

        public static void CreateProduct() //Lägg till produkt
        {
            using (var db = new WebShopDbContext())
            {
                Console.WriteLine("Categories:");
                Read.ReadCategories();
                Console.WriteLine();
                Console.WriteLine("Products:");
                Read.ReadProducts();
                Console.WriteLine();
                Console.WriteLine("Enter product name:");
                string productName = Console.ReadLine();

                Console.WriteLine("Enter price for the product:");
                double pricePerUnit = double.Parse(Console.ReadLine());

                Console.WriteLine("Enter number of units in stock:");
                int unitsInStock = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter a description:");
                string description = Console.ReadLine();

                Console.WriteLine("Enter the name of the supplier:");
                string supplier = Console.ReadLine();

                Console.WriteLine("Is the product on sale? 1 = Yes. 0 = No");
                bool isOnSale = false;
                int onSale = int.Parse(Console.ReadLine());
                if (onSale == 1)
                {
                    isOnSale = true;
                }

                Console.WriteLine("Enter the categoryId: ");
                int categoryId = int.Parse(Console.ReadLine());

                Product newProduct = new Product(productName, pricePerUnit, unitsInStock, description, supplier, isOnSale, categoryId);
                db.Products.Add(newProduct);
                db.SaveChanges();
            }
            Console.Clear();

        }
        public static void CreateCartItem() //Skicka in produkten eller produktId. Genom användaren
        {
            //Metod för att lägga till cart item. 
        }

        public static void CreateOrder() //Genom användaren
        {
            //Metod för att lägga till order. 
        }
    }
}
