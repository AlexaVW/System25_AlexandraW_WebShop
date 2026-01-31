using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Connections;
using Webshop.Edit;
using Webshop.Menus;
using Webshop.Models;

namespace Webshop.Pages
{
    internal class CostumerPages
    {
        public static void ShoppingPage()
        {
            bool isRunning = true;
            while (isRunning)
            {
                using (var db = new WebShopDbContext())
                {
                    Read.ShowCategories(db);
                    ShoppingPageMenu();
                    
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    string input = key.KeyChar.ToString().ToUpper();

                    bool validInput = int.TryParse(input, out int selectedId);
                    var selectedCategory = (from c in db.Categories
                                            where c.Id == selectedId
                                            select c).SingleOrDefault();
                    Console.Clear();
                    // If the input is a categoryId
                    if (selectedCategory != null)
                    {
                        WindowStructure.CategoryPage(Helpers.GetCategoryProducts(selectedId));
                    }
                    else
                    {
                        // If the input is not an int
                        switch (input)
                        {
                            case "Q":
                                SearchPage.SearchProduct();
                                break;
                            case "W":
                                isRunning = false;
                                break;
                            case "E":
                                Environment.Exit(0);
                                break;
                        }
                    }
                }
                Console.Clear();
            }
        }
        
        public static void CartPage()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Cart page");
                CartPageMenu();
                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.Clear();
                switch (key.KeyChar) 
                {
                    case '1': // Show CartItem
                        Read.ShowCartItems();
                        Console.ReadKey();
                        break;
                    case '2': // Update amount of cart item
                        Update.UpdateCartItem();
                        break;
                    case '3': // Delete cart item
                        Delete.DeleteCartItem();
                        break;
                    case '4': // Checkout
                        if (Helpers.GetCartItemsNotPaid().Count >= 1)
                        {
                            Create.CreateOrder();
                        }
                        else
                        {
                            Console.WriteLine("You have no items in your cart");
                            Console.ReadKey();
                        }
                            break;
                    case '8': // Go Back
                        isRunning = false;
                        break;
                    case '9': // Exit
                        Environment.Exit(0);
                        break;
                }
                Console.Clear();
            }
        }
        
        public static void ShoppingPageMenu()
        {
            Console.WriteLine("[Q] Search");
            Console.WriteLine("[W] Go Back");
            Console.WriteLine("[E] Exit");
        }
        public static void CartPageMenu()
        {
            Console.WriteLine("[1] Read CartItem");
            Console.WriteLine("[2] Edit Amount Of Product");
            Console.WriteLine("[3] Delete Product");
            Console.WriteLine("[4] Checkout");
            Console.WriteLine("[8] Go Back");
            Console.WriteLine("[9] Exit");
        }
    }
}
