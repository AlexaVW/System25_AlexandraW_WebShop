using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Webshop.Edit;
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
                'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P',
                'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L'
            };
            return chars;
        }

        public static int GetButtonIndex(string selectedChar)
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
                case "A":
                    value = 10;
                    break;
                case "S":
                    value = 11;
                    break;
                case "D":
                    value = 12;
                    break;
                case "F":
                    value = 13;
                    break;
                case "G":
                    value = 14;
                    break;
                case "H":
                    value = 15;
                    break;
                case "J":
                    value = 16;
                    break;
                case "K":
                    value = 17;
                    break;
                case "L":
                    value = 18;
                    break;

            }
            return value;
        }

        public static void AddProductToCart(List<Product> productsOnSale, string selectedChar)
        {
            int selectedProduct = Helpers.GetButtonIndex(selectedChar); //Får ut t.ex värde Q = 0, W = 1

            //Nytt fönster som endast visar information om vald produkt.
            if (selectedProduct >= 0 && selectedProduct < productsOnSale.Count)
            {
                Create.CreateCartItem(productsOnSale[selectedProduct]);
                Console.WriteLine("Added " + productsOnSale[selectedProduct].Name + " to cart");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
            }
        }

        public static double CalculateAllCartItemsPrice()
        {
            double totalPrice = 0;
            using (var db = new WebShopDbContext())
            {
                foreach (var cartItem in db.CartItems.Where(c => c.IsPayed == false).Include(c => c.product))
                {
                    totalPrice += cartItem.product.PricePerUnit * cartItem.ProductAmount;
                }
                return totalPrice;
            }
        }

        public static double CalculateTax(double price)
        {
            double calculatedTax = 0;
            double tax = 0.25;
            calculatedTax = price * tax;
            return calculatedTax;
        }

        public static List<CartItem> GetCartItemsNotPayed() 
        {
            List<CartItem> cartItemsNotPayed = new List<CartItem>();
            using(var db = new WebShopDbContext())
            {
                foreach(var cartItem in db.CartItems.Include(c => c.product).Where(c => c.IsPayed == false))
                {
                    cartItemsNotPayed.Add(cartItem);
                }
            }
            return cartItemsNotPayed;
        }

        








    }
}
