using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;
using WindowDemo;

namespace Webshop
{
    internal class WindowStructure
    {
        public static void DrawHomePage()
        {
            List<string> shopText = new List<string> { "Teddy's Rabbit Supplies" };
            var windowTop = new Window("", 35, 1, shopText);
            windowTop.Draw();

            List<string> welcomeText = new List<string> { "Welcome", "to Teddy’s Rabbit Supplies", "where you can find", "quality products", "for your bunny!" };
            var windowTop1 = new Window("", 70, 5, welcomeText);
            windowTop1.Draw();

            List<string> adminText = new List<string> { "[A] Admin Menu" };
            var windowsTop2 = new Window("Admin", 2, 5, adminText);
            windowsTop2.Draw();

            List<string> costumerText = new List<string> { "[1] Shopping Page", "[2] Cart", "[9] Exit" };
            var windowTop3 = new Window("Costumer Menu", 2, 8, costumerText);
            windowTop3.Draw();

            int offerTopPad = 15;

            List <Product> productsOnSale = Helpers.GetProductsOnSale();

            int posLeft = 2;

            for (int i = 0; i < productsOnSale.Count; i++)
            {
                List<string> text = new List<string> { productsOnSale[i].Name, productsOnSale[i].Description, productsOnSale[i].PricePerUnit.ToString() + " SEK", "Press [Q] to buy" };
                var productWindow = new Window("Offer", posLeft, offerTopPad, text);
                productWindow.Draw();
                posLeft += productsOnSale[i].Description.Length + 4;
            }
            
        }

        public static void DrawCategoryPage()
        {
            List<string> menuText = new List<string> { "[1] Go To Cart", "[8] Go Back", "[9] Exit" };
            var windowsTop = new Window("", 1, 1, menuText);
            windowsTop.Draw();

            List<string> CategoryName = new List<string> { "Food" };
            var windowTop1 = new Window("", 1, 7, CategoryName);
            windowTop1.Draw();

            int leftPadding = 30;
            List<string> product1 = new List<string> { "Oxford Hay", "Price: 299 SEK", "Press [Q] to show more" };
            var windowTop2 = new Window("", 1, 10, product1);
            windowTop2.Draw();

            List<string> product2 = new List<string> { "Nordic Quality Hay", "Price: 399 SEK", "Press [W] to show more" };
            var windowTop3 = new Window("", 1 + leftPadding, 10, product2);
            windowTop3.Draw();

            List<string> product3 = new List<string> { "Alpine Hay", "Price: 225 SEK", "Press [E] to show more" };
            var windowTop4 = new Window("", 1 + leftPadding * 2, 10, product3);
            windowTop4.Draw();

            List<string> product4 = new List<string> { "Nordic Quality Pellets", "Price: 145 SEK", "Press [R] to show more" };
            var windowTop5 = new Window("", 1 + leftPadding * 3, 10, product4);
            windowTop5.Draw();

            List<string> product5 = new List<string> { "Selective Pellets", "Price: 139 SEK", "Press [T] to show more" };
            var windowTop6 = new Window("", 1 + leftPadding * 4, 10, product5);
            windowTop6.Draw();

        }
    }
}
