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
                if (Helpers.GetChars()[i] != -1) //Ifall index inte är -1
                {
                    string pressKey = $"Press [" + Helpers.GetChars()[i] + "] To Buy";
                    Console.WriteLine(productsSearch[i].Name);
                    Console.WriteLine(productsSearch[i].Description);
                    Console.WriteLine(productsSearch[i].PricePerUnit + " SEK");
                    Console.WriteLine(pressKey);
                    Console.WriteLine();
                }
            }
            string key = Console.ReadKey(true).KeyChar.ToString().ToUpper();
            Helpers.AddProductToCart(productsSearch, key);
        }
    }
}
