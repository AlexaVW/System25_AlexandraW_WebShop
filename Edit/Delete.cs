using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;
using Webshop.Connections;

namespace Webshop.Edit
{
    internal class Delete
    {
        public static void DeleteCategory()
        {
            using (var db = new WebShopDbContext())
            {
                Read.ShowCategories(db);
                Console.Write("Choose Id to delete a category: ");
                int selectedCategory = int.Parse(Console.ReadLine());
                var deleteCategory = (from c in db.Categories
                                     where c.Id == selectedCategory
                                     select c).SingleOrDefault();
                if (deleteCategory != null)
                {
                    try
                    {
                        db.Categories.Remove(deleteCategory);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("You can't delete this category, it contains products");
                        Console.WriteLine();
                        Console.WriteLine(ex.Message);
                        
                    }
                }
            }
        }
        public static void DeleteProduct()
        {
            using (var db = new WebShopDbContext())
            {
                Read.ShowProducts(db); 
                Console.Write("Choose Id to delete product: ");
                int selectedProduct = int.Parse(Console.ReadLine());
                var deleteProduct = (from p in db.Products
                                     where p.Id == selectedProduct
                                     select p).SingleOrDefault();
                if (deleteProduct != null)
                {
                    try
                    {
                        db.Products.Remove(deleteProduct);
                        db.SaveChanges();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Ops... Something went wrong");
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        public static void DeleteCartItem() //Genom användaren
        {
            using (var db = new WebShopDbContext())
            {
                Read.ShowCartItems();
                Console.Write("Choose Id to remove product from cart: ");
                int selectedProduct = int.Parse(Console.ReadLine());
                var deleteProduct = (from c in db.CartItems
                                     where c.Id == selectedProduct
                                     select c).SingleOrDefault();
                if (deleteProduct != null)
                {
                    try
                    {
                        db.CartItems.Remove(deleteProduct);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ops... Something went wrong");
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public static void DeleteOrder() 
        {
            using(var db = new WebShopDbContext())
            {
                Read.ShowOrderHistory(db);
                Console.WriteLine("Choose Id to delete order");
                int selectedOrder = int.Parse(Console.ReadLine());
                var deleteOrder = (from o in db.Orders
                                   where o.Id == selectedOrder
                                   select o).SingleOrDefault();
                if (deleteOrder != null)
                {
                    try
                    {
                        db.Orders.Remove(deleteOrder);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ops... Something went wrong");
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
