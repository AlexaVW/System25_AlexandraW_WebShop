using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Webshop.Models;

namespace Webshop
{
    internal class Helpers
    {
        public static List<Product> GetProductsOnSale()
        {
            List<Product> productsOnSale = new List<Product>();
            using (var db = new WebShopDbContext())
            {
                productsOnSale = (from s in db.Products
                                    where s.IsOnSale == true
                                    select s).ToList();
            }
            return productsOnSale;

        }

        public static List<Product> GetCategoryProducts(int selectedCategoryId) //Hämtar kategorierna med sina produkter i
        {
            using (var db = new WebShopDbContext())
            {
                var productsInCategory = db.Categories.Include(c => c.Products).Where(c => c.Id == selectedCategoryId).ToList();
                
                var listOfProducts = new List<Product>();

                foreach( var category in productsInCategory)
                {
                    listOfProducts.AddRange(category.Products);
                }
                return listOfProducts;
            }
        }

        public static List<char> GetChars()
        {
            List <char> chars = new List<char>() 
            { 
                'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P'
            };
            return chars;
        }

        public static int GetCharValue(string selectedChar)
        {
            int value = -1;
            switch (selectedChar.ToUpper())
            {
                case "Q":
                    value = 0;
                    break;
                case "W":
                    value = 1;
                    break;
                case "E":
                    value = 2;
                    break;
                case "R":
                    value = 3;
                    break;
                case "T":
                    value = 4;
                    break;
                case "Y":
                    value = 5;
                    break;
                case "U":
                    value = 6;
                    break;
                case "I":
                    value = 7;
                    break;
                case "O":
                    value = 8;
                    break;
                case "P":
                    value = 9;
                    break;
            }
            return value;
        }

        
    }
}
