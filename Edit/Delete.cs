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
                int selectedId;
                bool validInput = int.TryParse(Console.ReadLine(), out selectedId);
                var selectedCategory = (from c in db.Categories
                                     where c.Id == selectedId
                                      select c).SingleOrDefault();
                DeleteSelectedCategory(selectedCategory, db);
            }
        }
        public static void DeleteSelectedCategory(Category category, WebShopDbContext db)
        {
            if (category != null)
            {
                try
                {
                    db.Categories.Remove(category);
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
        public static void DeleteProduct()
        {
            using (var db = new WebShopDbContext())
            {
                Read.ShowProducts(db); 
                Console.Write("Choose Id to delete product: ");
                int selectedId;
                bool validInput = int.TryParse(Console.ReadLine(), out selectedId);
                var selectedProduct = (from p in db.Products
                                     where p.Id == selectedId
                                     select p).SingleOrDefault();
                DeleteSelectedProduct(selectedProduct, db);
            }
        }
        public static void DeleteSelectedProduct(Product product, WebShopDbContext db)
        {
            if (product != null)
            {
                try
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ops... Something went wrong");
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static void DeleteCartItem() 
        {
            using (var db = new WebShopDbContext())
            {
                Read.ShowCartItems();
                Console.Write("Choose Id to remove product from cart: ");
                int selectedId;
                bool validInput = int.TryParse(Console.ReadLine(), out selectedId);
                var selectedCartItem = (from ci in db.CartItems
                                       where ci.Id == selectedId
                                       select ci).SingleOrDefault();
                DeleteSelectedCartItem(selectedCartItem, db);
            }
        }
        public static void DeleteSelectedCartItem(CartItem cartItem, WebShopDbContext db)
        {
            if (cartItem != null)
            {
                try
                {
                    db.CartItems.Remove(cartItem);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ops... Something went wrong");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void DeleteOrder() 
        {
            using (var db = new WebShopDbContext())
            {
                var orderGroups = Read.ShowOrderHistoryAndGetOrderNumber(db);
                Console.Write("Enter Order Number to delete order: ");
                int selectedOrderNumber = int.Parse(Console.ReadLine()) -1;

                var selectedOrderGroup = orderGroups[selectedOrderNumber];
                var selectedOrder = selectedOrderGroup.ToList();
                DeleteSelectedOrder(selectedOrder, db);
            }
        }
        public static void DeleteSelectedOrder(List<Order> orders, WebShopDbContext db)
        {
            if (orders != null)
            {
                try
                {
                    foreach (var order in orders)
                    {
                        db.Orders.Remove(order);
                    }
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
