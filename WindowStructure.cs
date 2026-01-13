using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;
using WindowDemo;
using static System.Net.Mime.MediaTypeNames;

namespace Webshop
{
    internal class WindowStructure
    {
        public static void DrawHomePage()
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

            //Skapar lista för produkterna som är on sale
            List <Product> productsOnSale = Helpers.GetProductsOnSale();
            
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
            //Om användaren trycker på en bokstav:
            string selectedChar = Console.ReadKey(true).KeyChar.ToString();

            int selectedProduct = Helpers.GetCharValue(selectedChar); //Får ut t.ex värde Q = 0, W = 1
            Console.Clear();

            //Nytt fönster som endast visar information om vald produkt.
            if (selectedProduct >= 0 && selectedProduct < productsOnSale.Count)
            {
                Console.WriteLine("Added " + productsOnSale[selectedProduct].Name + " to cart");
            }



        }



        public static void DrawCategoryPage(List<Product> productsInCategory) 
        {
            //Skriver ut menyn
            List<string> menuText = new List<string> { "[1] Go To Cart", "[8] Go Back", "[9] Exit" };
            var windowsTop = new Window("", 1, 1, menuText);
            windowsTop.Draw();

            //Skriver ut det valda kategorinamnet
            List<string> CategoryName = new List<string> { productsInCategory[0].Category.Name };
            var windowTop1 = new Window("", 1, 7, CategoryName);
            windowTop1.Draw();

            //Positioner för fönsterna
            int posLeft = 1;
            int posTop = 10;
            int padWindowProduct = 4;
            
            //Loopar igenom listan som skickats in i metoden med alla produkter i kategorierna
            for(int i = 0; i < productsInCategory.Count; i++)
            {
                string pressKey = $"Press [" + Helpers.GetChars()[i] + "] To Show More";
                
                //Ritar ut fönstret för produkten i kategorin
                List<string> product = new List<string> { productsInCategory[i].Name, productsInCategory[i].PricePerUnit.ToString() + " SEK", pressKey };
                var productWindow = new Window("", posLeft, posTop, product);
                productWindow.Draw();
                if(pressKey.Length > productsInCategory[i].Name.Length)
                {
                    posLeft += pressKey.Length + padWindowProduct;
                }
                else
                {
                    posLeft += productsInCategory[i].Name.Length + padWindowProduct;
                }
            }

            //Om användaren trycker på en bokstav:
            string selectedChar = Console.ReadKey(true).KeyChar.ToString();
            
            int selectedProduct = Helpers.GetCharValue(selectedChar); //Får ut t.ex värde Q = 0, W = 1
            Console.Clear();

            //Nytt fönster som endast visar information om vald produkt.
            if(selectedProduct >= 0 && selectedProduct < productsInCategory.Count)
            {
                Console.WriteLine(productsInCategory[selectedProduct].Name + "\n" +
                    "Price: " + productsInCategory[selectedProduct].PricePerUnit + " SEK" + "\n" +
                    productsInCategory[selectedProduct].Description + "\n" +
                    "Press B to add to Cart");
                //Metod för att lägga till i cart
            }
        }
    }
}
