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
        // Delete a category (Admin)
        public static void DeleteCategory()
        {
            using (var db = new WebShopDbContext())
            {
                Read.ShowCategories(db);
                Console.Write("Choose Id to delete a category: ");
                bool validInput = int.TryParse(Console.ReadLine(), out int selectedId);
                var selectedCategory = (from c in db.Categories
                                     where c.Id == selectedId
                                      select c).SingleOrDefault();
                DeleteSelectedCategory(selectedCategory, db);
            }
        }
        private static void DeleteSelectedCategory(Category category, WebShopDbContext db)
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
                    Console.WriteLine("You can't delete this category, it contains products \n");
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            }
        }

        // Delete a product (Admin)
        public static void DeleteProduct()
        {
            using (var db = new WebShopDbContext())
            {
                Read.ShowProducts(db); 
                Console.Write("Choose Id to delete product: ");
                bool validInput = int.TryParse(Console.ReadLine(), out int selectedId);
                var selectedProduct = (from p in db.Products
                                     where p.Id == selectedId
                                     select p).SingleOrDefault();
                DeleteSelectedProduct(selectedProduct, db);
            }
        }
        private static void DeleteSelectedProduct(Product product, WebShopDbContext db)
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

        // Delete a product from cart (Customer) 
        public static void DeleteCartItem() 
        {
            using (var db = new WebShopDbContext())
            {
                Read.ShowCartItems();
                Console.Write("Choose Id to remove product from cart: ");
                bool validInput = int.TryParse(Console.ReadLine(), out int selectedId);
                var selectedCartItem = (from ci in db.CartItems
                                       where ci.Id == selectedId
                                       select ci).SingleOrDefault();
                DeleteSelectedCartItem(selectedCartItem, db);
            }
        }
        private static void DeleteSelectedCartItem(CartItem cartItem, WebShopDbContext db)
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

        // Delete an order (Admin)
        public static void DeleteOrder() 
        {
            using (var db = new WebShopDbContext())
            {
                var orderGroups = Read.ShowOrderHistoryAndGetOrderNumber(db);
                Console.Write("Enter Order Number to delete order: ");
                
                bool validInput = int.TryParse(Console.ReadLine(), out int selectedOrderNumber) && selectedOrderNumber < orderGroups.Count;
                selectedOrderNumber -= 1;
                if (validInput)
                {
                    // Selects a group of orders (One cart item can have many orders)
                    var selectedOrderGroup = orderGroups[selectedOrderNumber];
                    
                    // To Delete every order that has the selected number - make it to a list. Then delete
                    var selectedOrder = selectedOrderGroup.ToList();
                    DeleteSelectedOrder(selectedOrder, db);
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    Console.ReadKey();
                }
            }
        }
        private static void DeleteSelectedOrder(List<Order> orders, WebShopDbContext db)
        {
            if (orders != null)
            {
                try
                {
                    db.Orders.RemoveRange(orders);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ops... Something went wrong");
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            }
        }
    }
}
