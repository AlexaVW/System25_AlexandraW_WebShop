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
        // Getting products on sale
        public static List<Product> GetProductsOnSale(int amount)
        {
            using (var db = new WebShopDbContext())
            {
                return db.Products.Where(p => p.IsOnSale == true).Take(amount).ToList();
            }
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
        }

        // Adding product to cart when there are more than one to choose from
        public static void AddProductToCart(List<Product> products, string selectedChar)
        {
            int selectedProduct = Helpers.GetButtonIndex(selectedChar); // Gets the value Q = 0, W = 1, E = 2 for example

            // Adding the selected product to cart
            if (selectedProduct >= 0 && selectedProduct < products.Count)
            {
                Create.CreateCartItem(products[selectedProduct]);
                Console.WriteLine("Added " + products[selectedProduct].Name + " to cart");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
            }
        }

        // Calculating CartItems price in checkout
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

        // Calculating tax
        public static double CalculateTax(double price)
        {
            double calculatedTax = 0;
            double tax = 0.25;
            calculatedTax = price * tax;
            return calculatedTax;
        }

        // Getting CartItems not paid
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
