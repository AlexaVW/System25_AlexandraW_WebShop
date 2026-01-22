using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Edit;
using Webshop.Menus;
using Webshop.Connections;

namespace Webshop.Pages
{
    internal class CostumerPages
    {
        public static void ShoppingPage()
        {
            bool isRunning = true;
            while (isRunning)
            {
                ShoppingPageMenu();
                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.Clear();
                switch (key.KeyChar) 
                {
                    case '1':
                        WindowStructure.CategoryPage(Helpers.GetCategoryProducts(5));
                        break;
                    case '2':
                        WindowStructure.CategoryPage(Helpers.GetCategoryProducts(2));
                        break;
                    case '3':
                        WindowStructure.CategoryPage(Helpers.GetCategoryProducts(3));
                        break;
                    case '4':
                        WindowStructure.CategoryPage(Helpers.GetCategoryProducts(4));
                        break;
                    case '5':
                        SearchPage.SearchProduct();
                        break;
                    case '8':
                        isRunning = false;
                        break;
                    case '9':
                        Environment.Exit(0);
                        break;
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
                    case '1': //Read CartItem
                        Read.ShowCartItems();
                        Console.ReadKey();
                        break;
                    case '2': //Update product
                        Update.UpdateCartItem();
                        Console.ReadKey();
                        break;
                    case '3': //Delete product
                         Delete.DeleteCartItem();
                        break;
                    case '4': //Checkout
                        Create.CreateOrder();
                        break;
                    case '8': //Go Back
                        isRunning = false;
                        break;
                    case '9':
                        Environment.Exit(0);
                        break;
                }
                Console.Clear();
            }
        }
        
        public static void ShoppingPageMenu()
        {
            Console.WriteLine("Categories");
            Console.WriteLine("[1] Food");
            Console.WriteLine("[2] Treats");
            Console.WriteLine("[3] Toys");
            Console.WriteLine("[4] Accessories");
            Console.WriteLine("[5] Search");
            Console.WriteLine("[8] Go Back");
            Console.WriteLine("[9] Exit");
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
