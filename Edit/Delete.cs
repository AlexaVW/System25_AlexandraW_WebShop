using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop.Edit
{
    internal class Delete
    {
        public static void DeleteCategory()
        {
            using (var db = new WebShopDbContext())
            {
                Read.ReadCategories();
                Console.WriteLine();
                Console.WriteLine("Choose Id to delete a category");
                int selectedCategory = int.Parse(Console.ReadLine());
                var deleteCategory = (from c in db.Categories
                                     where c.Id == selectedCategory
                                     select c).SingleOrDefault();
                if (deleteCategory != null)
                {
                    db.Categories.Remove(deleteCategory);
                    db.SaveChanges();
                }
            }

        }
        public static void DeleteProduct()
        {
            using (var db = new WebShopDbContext())
            {
                Read.GetProductsAsync(new Models.WebShopDbContext()); 
                Console.WriteLine();
                Console.WriteLine("Choose Id to delete product");
                int selectedProduct = int.Parse(Console.ReadLine());
                var deleteProduct = (from p in db.Products
                                     where p.Id == selectedProduct
                                     select p).SingleOrDefault();
                if (deleteProduct != null)
                {
                    db.Products.Remove(deleteProduct);
                    db.SaveChanges();
                }
            }
        }
        public static void DeleteCartItem() //Genom användaren
        {
            using (var db = new WebShopDbContext())
            {
                Read.WriteCartItems();
                Console.WriteLine();
                Console.WriteLine("Choose Id to delete product");
                int selectedProduct = int.Parse(Console.ReadLine());
                var deleteProduct = (from c in db.Cart
                                     where c.Id == selectedProduct
                                     select c).SingleOrDefault();
                if (deleteProduct != null)
                {
                    db.Cart.Remove(deleteProduct);
                    db.SaveChanges();
                }
            }
        }
    }
}
