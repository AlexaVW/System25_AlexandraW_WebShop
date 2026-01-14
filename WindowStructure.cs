using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Webshop.Edit;
using Webshop.Models;
using Webshop.Pages;
using WindowDemo;
using static System.Net.Mime.MediaTypeNames;

namespace Webshop
{
    internal class WindowStructure
    {
        public static void HomePage()
        {
            DrawHomePageWindows();
            List<Product> productsOnSale = Helpers.GetProductsOnSale();

            DrawProductsOnSaleWindows(productsOnSale);
            string key = Console.ReadKey(true).KeyChar.ToString().ToUpper();
            
            Helpers.AddProductToCart(productsOnSale, key);
            Console.Clear();
            switch (key)
            {
                case "A":
                    Pages.AdminPages.PrintMenuFirstAdminPage();
                    break;
                case "1":
                    Pages.CostumerPages.ShoppingPage();
                    break;
                case "2":
                    Pages.CostumerPages.CartPage();
                    break;
                case "9":
                    Environment.Exit(0);
                    break;

            }
        }

        public static void CategoryPage(List<Product> categoryProducts)
        {
            bool isRunning = true;
            while (isRunning)
            {
                DrawCategoryPageMenu(categoryProducts);
                DrawCategoryProductsWindows(categoryProducts);

                string key = Console.ReadKey(true).KeyChar.ToString().ToUpper();
                ShowMoreProductInfo(categoryProducts[Helpers.GetCharValue(key)]);

                Console.Clear();
                switch (key)
                {
                    case "1":
                        CostumerPages.CartPage();
                        break;
                    case "8":
                        isRunning = false; //Kan ej gå ur loopen
                        break;
                    case "9":
                        Environment.Exit(0);
                        break;

                }
            }
            
        }


        public static void DrawHomePageWindows()
        {
            //Skriver ut butikens namn
            List<string> shopText = new List<string> { "Teddy's Rabbit Supplies" };
            var windowTop = new Window("", 35, 1, shopText);
            windowTop.Draw();

            //Skriver ut välkomsttext
            List<string> welcomeText = new List<string> { "Welcome", "to Teddy’s Rabbit Supplies", "where you can find", "quality products", "for your bunny!" };
            var windowTop1 = new Window("", 70, 5, welcomeText);
            windowTop1.Draw();

            //Skriver ut Adminmenyn
            List<string> adminMenuText = new List<string> { "[A] Admin Menu" };
            var windowsTop2 = new Window("Admin", 2, 5, adminMenuText);
            windowsTop2.Draw();

            //Skriver ut kundens meny
            List<string> costumerText = new List<string> { "[1] Shopping Page", "[2] Cart", "[9] Exit" };
            var windowTop3 = new Window("Costumer Menu", 2, 8, costumerText);
            windowTop3.Draw();
        }

        public static void DrawProductsOnSaleWindows(List<Product> productsOnSale)
        {
            //Positioner för fönstrerna
            int onSaleTopPad = 15;
            int posLeft = 2;
            int padWindowProduct = 4;

            //Loopar igenom produkterna som är On Sale
            for (int i = 0; i < productsOnSale.Count; i++)
            {
                //Ritar ut produkterna i fönster
                string pressKey = $"Press [" + Helpers.GetChars()[i] + "] To Buy";
                List<string> text = new List<string> { productsOnSale[i].Name, productsOnSale[i].Description, productsOnSale[i].PricePerUnit.ToString() + " SEK", pressKey };
                var productWindow = new Window("On Sale", posLeft, onSaleTopPad, text);
                productWindow.Draw();

                //Fönstrets längd beror på texternas längd
                if (productsOnSale[i].Description.Length > productsOnSale[i].Name.Length)
                {
                    posLeft += productsOnSale[i].Description.Length + padWindowProduct;
                }
                else if (productsOnSale[i].Name.Length > productsOnSale[i].Description.Length)
                {
                    posLeft += productsOnSale[i].Name.Length + padWindowProduct;
                }
                else
                {
                    posLeft += pressKey.Length + padWindowProduct;
                }
            }
        }



        public static void DrawCategoryPageMenu(List<Product> productsInCategory)
        {
            //Skriver ut menyn
            List<string> menuText = new List<string> { "[1] Go To Cart", "[8] Go Back", "[9] Exit" };
            var windowsTop = new Window("", 1, 1, menuText);
            windowsTop.Draw();

            //Skriver ut det valda kategorinamnet
            List<string> CategoryName = new List<string> { productsInCategory[0].Category.Name };
            var windowTop1 = new Window("", 1, 7, CategoryName);
            windowTop1.Draw();

        }

        public static void DrawCategoryProductsWindows(List<Product> productsInCategory)
        {
            //Positioner för fönsterna
            int posLeft = 1;
            int posTop = 10;
            int padWindowProduct = 4;

            //Loopar igenom listan som skickats in i metoden med alla produkter i kategorierna
            for (int i = 0; i < productsInCategory.Count; i++)
            {
                string pressKey = $"Press [" + Helpers.GetChars()[i] + "] To Show More";

                //Ritar ut fönstret för produkten i kategorin
                List<string> product = new List<string> { productsInCategory[i].Name, productsInCategory[i].PricePerUnit.ToString() + " SEK", pressKey };
                var productWindow = new Window("", posLeft, posTop, product);
                productWindow.Draw();
                if (pressKey.Length > productsInCategory[i].Name.Length)
                {
                    posLeft += pressKey.Length + padWindowProduct;
                }
                else
                {
                    posLeft += productsInCategory[i].Name.Length + padWindowProduct;
                }
            }
        }

        public static void ShowMoreProductInfo(Product product) 
        {
            Console.Clear();
            Console.WriteLine(product.Name);
            Console.WriteLine(product.PricePerUnit + " SEK");
            Console.WriteLine(product.Description);
            Console.WriteLine("Press B to add to cart");
            string key = Console.ReadKey(true).KeyChar.ToString().ToUpper();
            if (key == "B")
            {
                Create.CreateCartItem(product);
            }
        }
    }
}
