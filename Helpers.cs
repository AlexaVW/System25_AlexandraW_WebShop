using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Webshop.Connections;
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
                productsOnSale = db.Products.Where(p => p.IsOnSale == true).ToList();
            }
            return productsOnSale;
        }

        // Gets the categories with their products in based on selected Id
        public static List<Product> GetCategoryProducts(int selectedCategoryId) 
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

        // To print out the buttons to select from when selecting product
        public static List<char> GetButtonKeys()
        {
            List <char> chars = new List<char>() 
            { 
                'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P',
                'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 
                'Z', 'X', 'C', 'V', 'B', 'N', 'M'
            };
            return chars;
        }

        // To get which index the user clicked on
        public static int GetButtonIndex(string selectedButton)
        {
            int index = "QWERTYUIOPASDFGHJKLZXCVBNM".IndexOf(selectedButton.ToUpper());
            return index;
            
            //int value = -1;
            //switch (selectedButton.ToUpper())
            //{
            //    case "Q":
            //        value = 0;
            //        break;
            //    case "W":
            //        value = 1;
            //        break;
            //    case "E":
            //        value = 2;
            //        break;
            //    case "R":
            //        value = 3;
            //        break;
            //    case "T":
            //        value = 4;
            //        break;
            //    case "Y":
            //        value = 5;
            //        break;
            //    case "U":
            //        value = 6;
            //        break;
            //    case "I":
            //        value = 7;
            //        break;
            //    case "O":
            //        value = 8;
            //        break;
            //    case "P":
            //        value = 9;
            //        break;
            //    case "A":
            //        value = 10;
            //        break;
            //    case "S":
            //        value = 11;
            //        break;
            //    case "D":
            //        value = 12;
            //        break;
            //    case "F":
            //        value = 13;
            //        break;
            //    case "G":
            //        value = 14;
            //        break;
            //    case "H":
            //        value = 15;
            //        break;
            //    case "J":
            //        value = 16;
            //        break;
            //    case "K":
            //        value = 17;
            //        break;
            //    case "L":
            //        value = 18;
            //        break;
            //    case "Z":
            //        value = 19;
            //        break;
            //    case "X":
            //        value = 20;
            //        break;
            //    case "C":
            //        value = 21;
            //        break;
            //    case "V":
            //        value = 22;
            //        break;
            //    case "B":
            //        value = 23;
            //        break;
            //    case "N":
            //        value = 24;
            //        break;
            //    case "M":
            //        value = 25;
            //        break;
            //}
            //return value;
        }

        public static void AddProductToCart(List<Product> productsOnSale, string selectedChar)
        {
            int selectedProduct = Helpers.GetButtonIndex(selectedChar); // Gets the value Q = 0, W = 1, E = 2 for example

            // Adding the selected product to cart
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
                List<CartItem> cartItems = GetCartItemsNotPaid();
                foreach (var cartItem in cartItems)
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

        public static List<CartItem> GetCartItemsNotPaid() 
        {
            List<CartItem> cartItemsNotPaid = new List<CartItem>();
            using(var db = new WebShopDbContext())
            {
                foreach(var cartItem in db.CartItems.Where(ci => ci.IsPaid == false).Include(ci => ci.product))
                {
                    cartItemsNotPaid.Add(cartItem);
                }
            }
            return cartItemsNotPaid;
        }
    }
}
