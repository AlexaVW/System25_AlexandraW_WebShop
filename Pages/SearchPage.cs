using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Webshop.Edit;
using Webshop.Models;

namespace Webshop.Pages
{
    internal class SearchPage
    {
        public static void SearchProduct()
        {
            Console.WriteLine("Search Page");
            var productsSearch = Connections.Dapper.SearchProduct();
            Console.WriteLine();
            for (int i = 0; i < productsSearch.Count; i++)
            {
                if (Helpers.GetButtonKeys()[i] != -1) //If index of selected button is not -1
                {
                    //Prints products with buttons to select a product
                    string pressKey = $"Press [" + Helpers.GetButtonKeys()[i] + "] To Buy";
                    Console.WriteLine(productsSearch[i].Name);
                    Console.WriteLine(productsSearch[i].Description);
                    Console.WriteLine(productsSearch[i].PricePerUnit + " SEK");
                    Console.WriteLine(pressKey); 
                    Console.WriteLine();
                }
            }
            // Adding the selected product to cart
            string button = Console.ReadKey(true).KeyChar.ToString().ToUpper();
            Helpers.AddProductToCart(productsSearch, button);
        }
    }
}
