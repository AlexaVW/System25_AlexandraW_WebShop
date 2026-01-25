using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Webshop.Edit;
using Webshop.Models;
using Webshop.Pages;
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
                case "0":
                    AdminPages.PrintMenuFirstAdminPage();
                    break;
                case "1":
                    CostumerPages.ShoppingPage();
                    break;
                case "2":
                    CostumerPages.CartPage();
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
                int productIndex = Helpers.GetButtonIndex(key); //Returns -1 if the button doesn't exist
                
                if (productIndex != -1)
                {
                    Console.Clear();
                    ShowMoreProductInfo(categoryProducts[Helpers.GetButtonIndex(key)]);
                }
                Console.Clear();
                switch (key)
                {
                    case "8":
                        isRunning = false; 
                        break;
                    case "9":
                        Environment.Exit(0);
                        break;
                }
            }
        }
        public static void DrawHomePageWindows()
        {
            // Printing the name of the webshop
            List<string> shopText = new List<string> { "Teddy's Rabbit Supplies" };
            var windowTop = new Window("", 35, 1, shopText);
            windowTop.Draw();

            // Printing a welcome text
            List<string> welcomeText = new List<string> { "Welcome", "to Teddy’s Rabbit Supplies", "where you can find", "quality products", "for your bunny!" };
            var windowTop1 = new Window("", 70, 5, welcomeText);
            windowTop1.Draw();

            // Printing the Admin menu
            List<string> adminMenuText = new List<string> { "[0] Admin Menu" };
            var windowsTop2 = new Window("Admin", 2, 5, adminMenuText);
            windowsTop2.Draw();

            // Printing the customer menu
            List<string> customerText = new List<string> { "[1] Shopping Page", "[2] Cart", "[9] Exit" };
            var windowTop3 = new Window("Customer Menu", 2, 8, customerText);
            windowTop3.Draw();
        }

        public static void DrawProductsOnSaleWindows(List<Product> productsOnSale)
        {
            // Positions for the windows
            int posTop = 15;
            int posLeft = 2;
            int padWindowProduct = 4;

            // Looping through the products on sale
            for (int i = 0; i < productsOnSale.Count; i++)
            {
                // Printing the products in windows
                string pressKey = $"Press [" + Helpers.GetButtonKeys()[i] + "] To Buy";
                List<string> text = new List<string> { productsOnSale[i].Name, productsOnSale[i].Description, productsOnSale[i].PricePerUnit.ToString() + " SEK", pressKey };
                var productWindow = new Window("On Sale", posLeft, posTop, text);
                productWindow.Draw();

                // The window length depends on the texts length
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
            // Printing the menu
            List<string> menuText = new List<string> { "[8] Go Back", "[9] Exit" };
            var windowsTop = new Window("", 1, 1, menuText);
            windowsTop.Draw();

            // Printing the selected category name
            List<string> CategoryName = new List<string> { productsInCategory[0].Category.Name };
            var windowTop1 = new Window("", 1, 7, CategoryName);
            windowTop1.Draw();
        }

        public static void DrawCategoryProductsWindows(List<Product> productsInCategory)
        {
            // Positions for the windows
            int posLeft = 1;
            int posTop = 10;
            int padWindowProduct = 4;

            // Looping through the list of selected products in the category
            for (int i = 0; i < productsInCategory.Count; i++)
            {
                string pressKey = $"Press [" + Helpers.GetButtonKeys()[i] + "] To Show More";

                // Printing the window for a product in the category
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
