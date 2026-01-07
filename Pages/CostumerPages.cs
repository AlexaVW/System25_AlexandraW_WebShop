using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Menus;

namespace Webshop.Pages
{
    internal class CostumerPages
    {
        public static void ShoppingPage()
        {
            ShoppingPageMenu();
            bool isRunning = true;
            while (isRunning)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.Clear();
                switch (key.KeyChar) 
                {
                    case '1':
                        CategoryPage(); 
                        break;
                    case '2':
                        CategoryPage();
                        break;
                    case '3':
                        CategoryPage();
                        break;
                    case '4':
                        CategoryPage();
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
        public static void CategoryPage() //Skicka in lista av produkter
        {
            Console.WriteLine("Category Page");
            WindowStructure.DrawCategoryPage();
            bool isRunning = true;
            while (isRunning)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.Clear();
                switch (key.KeyChar) 
                {
                    case '1':
                        CartPage();
                        break;
                    case 'q':
                        ProductPage();
                        break;
                    case 'w':
                        ProductPage();
                        break;
                    case 'e':
                        ProductPage();
                        break;
                    case 'r':
                        ProductPage();
                        break;
                    case 't':
                        ProductPage();
                        break;
                    case '8': // Go back
                        isRunning = false;
                        break;
                    case '9':
                        Environment.Exit(0);
                        break;
                }
            }
        }
        public static void ProductPage() //Skicka in en produkt
        {
            Console.WriteLine("Product Page");
            ProductPageMenu();
            bool isRunning = true;
            while (isRunning)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.Clear();
                switch (key.KeyChar) 
                {
                    case '1':
                        CartPage();
                        break;// Go to home page
                    case '8': //Go back
                        isRunning = false;
                        break;
                    case '9':
                        Environment.Exit(0);
                        break;
                }
            }
        }
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
            Console.WriteLine("Cart page");
            CartPageMenu();
            bool isRunning = true;
            while (isRunning)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.Clear();
                switch (key.KeyChar) 
                {
                    case '1': //Edit amount of product
                        //Metod för att ändra antal
                        break;
                    case '2': //Delete product
                        //Metod för att ta bort produkt
                        break;
                    case '3': //Checkout
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
            WindowStructure.DrawHomePage();
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
            Console.WriteLine("[1] Edit Amount Of Product");
            Console.WriteLine("[2] Delete Product");
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
