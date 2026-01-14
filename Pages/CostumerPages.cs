using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Edit;
using Webshop.Menus;

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
                        SearchPage();
                        break;
                    case '8':
                        isRunning = false;
                        break;
                    case '9':
                        Environment.Exit(0);
                        break;
                }
                
            }
        }
        
        
        //public static void ProductPage() //Skicka in en produkt
        //{
        //    Console.WriteLine("Product Page");
        //    ProductPageMenu();
        //    bool isRunning = true;
        //    while (isRunning)
        //    {
        //        ConsoleKeyInfo key = Console.ReadKey(true);
        //        Console.Clear();
        //        switch (key.KeyChar) 
        //        {
        //            case '1':
        //                CartPage();
        //                break;
        //            case '8': 
        //                isRunning = false;
        //                break;
        //            case '9':
        //                Environment.Exit(0);
        //                break;
        //        }
        //    }
        //}
        public static void SearchPage()
        {
            Console.WriteLine("Search Page");
            SearchPageMenu();
            //Metod för att söka efter produkt
            
            bool isRunning = true;
            while (isRunning)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.Clear();
                switch (key.KeyChar) 
                {
                    case '8': // Go back
                        isRunning = false;
                        break;
                    case '9':
                        Environment.Exit(0);
                        break;
                }
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
                        Read.WriteCartItems();
                        break;
                    case '2': //Update product
                        Update.UpdateCartItem();
                        break;
                    case '3': //Delete product
                         Delete.DeleteCartItem();
                        break;
                    case '4': //Checkout
                        //Create Order metod
                    break;
                        break;
                    case '8': //Go Back
                        isRunning = false;
                        break;
                    case '9':
                        Environment.Exit(0);
                        break;
                }
            }
        }
        public static void CheckoutPage()
        {
            Console.WriteLine("Checkout page");
            CheckoutAndPayMenu();
            //Metod för skriva in kontaktuppgifter
            
            //Shipping:
            //[1] Express shipping (1-2 days) 69 SEK
            //[2] Basic shipping (3-5 days) 49 SEK
            bool isRunning = true;
            while (isRunning)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.Clear();
                switch (key.KeyChar) //ToDo: Fixa så man kan skriva stor/liten bokstav
                {
                    case '8': 
                        isRunning = false;
                        break;
                    case '9':
                        Environment.Exit(0);
                        break;
                }
            }
        }
        public static void PayPage()
        {
            Console.WriteLine("Pay page");
            CheckoutAndPayMenu();
            //Metod för printa ut varukorgen
            //Metod för välja payment method
            //Metod för Räkna ihop summan
            //"Press [P] to pay

            bool isRunning = true;
            while (isRunning)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.Clear();
                switch (key.KeyChar) 
                {
                    case '8': 
                        isRunning = false;
                        break;
                    case '9':
                        Environment.Exit(0);
                        break;
                }
            }
            //Töm cart
            //Skickas till hemsidan
            WindowStructure.HomePage();
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
        
        public static void ProductPageMenu()
        {
            Console.WriteLine("[1] Go To Cart");
            Console.WriteLine("[8] Go Back");
            Console.WriteLine("[9] Exit");
        }

        public static void SearchPageMenu()
        {
            Console.WriteLine("[8] Go Back");
            Console.WriteLine("[9] Exit");
        }

        public static void CartPageMenu()
        {
            Console.WriteLine("[1] Read CartItem");
            Console.WriteLine("[2] Edit Amount Of Product");
            Console.WriteLine("[3] Delete Product");
            Console.WriteLine("[8] Go Back");
            Console.WriteLine("[9] Exit");
        }
        public static void CheckoutAndPayMenu()
        {
            Console.WriteLine("[8] Go Back");
            Console.WriteLine("[9] Exit");
        }

    }
}
