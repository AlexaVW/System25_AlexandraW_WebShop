using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Webshop.Models;

namespace Webshop.Edit
{
    internal class Create
    {
        public static void CreateCategory()
        {
            using (var db = new WebShopDbContext())
            {
                Console.WriteLine("Categories:");
                Read.ReadCategories();
                Console.WriteLine("Enter category name:");
                string categoryName = Console.ReadLine();

                Category newCategory = new Category(categoryName);
                db.Categories.Add(newCategory);
                db.SaveChanges();
            }
            Console.Clear();
        }

        public static void CreateProduct() //Lägg till produkt
        {
            using (var db = new WebShopDbContext())
            {
                Console.WriteLine("Categories:");
                Read.ReadCategories();
                Console.WriteLine();
                Console.WriteLine("Products:");
                Read.GetProductsAsync(new Models.WebShopDbContext());
                Console.WriteLine();
                Console.WriteLine("Enter product name:");
                string productName = Console.ReadLine();

                Console.WriteLine("Enter price for the product:");
                double pricePerUnit = double.Parse(Console.ReadLine());

                Console.WriteLine("Enter number of units in stock:");
                int unitsInStock = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter a description:");
                string description = Console.ReadLine();

                Console.WriteLine("Enter the name of the supplier:");
                string supplier = Console.ReadLine();

                Console.WriteLine("Is the product on sale? 1 = Yes. 0 = No");
                bool isOnSale = false;
                int onSale = int.Parse(Console.ReadLine());
                if (onSale == 1)
                {
                    isOnSale = true;
                }

                Console.WriteLine("Enter the categoryId: ");
                int categoryId = int.Parse(Console.ReadLine());

                Product newProduct = new Product(productName, pricePerUnit, unitsInStock, description, supplier, isOnSale, categoryId);
                db.Products.Add(newProduct);
                db.SaveChanges();
            }
            Console.Clear();

        }
        public static void CreateCartItem(Product product) //Skicka in produkten. Genom användaren
        {
            using (var db  = new WebShopDbContext())
            {
                int productAmount = 1;
                bool isPayed = false;
                int productId = product.Id;
                
                CartItem newCartItem = new CartItem(productAmount, isPayed, productId);
                //Kollar om produkten med Id finns i cartitem, annars använd update för ändra ProductAmount
                var alreadyInCart = db.Cart.FirstOrDefault(c=> c.ProductId  == productId);
                if (alreadyInCart != null)
                {
                    //Om produkten finns - plussa på 1 antal 
                    alreadyInCart.ProductAmount += 1;
                }
                else
                {
                    db.Cart.Add(newCartItem);
                }
                db.SaveChanges();

                //När man trycker på add to cart ska den produkten läggas till.
                //IsPayed är alltid false först. Blir true efter betalning
                //Product amount är alltid 1 först. Ändras i update 

            }
        }

        public static void CreateOrder()
        {
            string name = "";
            string address = "";
            string country = "";

            string shippingMethod = "";
            string paymentMethod = "";

            double subTotal = Helpers.CalculateTotalPrice();
            DateTime orderDate;
            
            Console.WriteLine("Checkout Page");
            Console.WriteLine();
            Console.WriteLine("Enter your shipping information");
            using (var db = new WebShopDbContext())
            {
                Console.Write("Name: ");
                name = Console.ReadLine();

                Console.Write("Address: ");
                address = Console.ReadLine();

                Console.Write("Country: ");
                country = Console.ReadLine();

                Console.WriteLine("Choose shipping method");
                Console.WriteLine("[1] Express shipping (1-2 days) 69 SEK");
                Console.WriteLine("[2] Basic shipping (3-5 days) 49 SEK");
                int choosenShippingMethod = int.Parse(Console.ReadLine());
                if( choosenShippingMethod == 1)
                {
                    shippingMethod = "Express Shipping";
                    subTotal += 69;
                }
                if( choosenShippingMethod == 2)
                {
                    shippingMethod = "Basic Shipping";
                    subTotal += 49;
                }

                Console.WriteLine("Choose payment method");
                Console.WriteLine("[3] Card");
                Console.WriteLine("[4] Klarna");
                int choosenPaymentMethod = int.Parse(Console.ReadLine());
                if( choosenPaymentMethod == 3)
                {
                    paymentMethod = "Card";
                }
                if ( choosenPaymentMethod == 4)
                {
                    paymentMethod = "Klarna";
                }

                orderDate = DateTime.Now;
                Read.WriteCartItemsInCheckout();
                Console.WriteLine();
                Console.WriteLine("Subtotal: " + subTotal + " SEK");

                Order newOrder = new Order(name, address, country, shippingMethod, paymentMethod, orderDate, subTotal);

                Console.WriteLine("Press [0] to continue");
                int pay = int.Parse(Console.ReadLine());
                if (pay == 0)
                {
                    Console.Clear();
                    PayPage(newOrder);
                }
            }

        }
        public static void PayPage(Order newOrder) 
        {
            
            Console.WriteLine("Pay Page");
            using (var db = new WebShopDbContext())
            {
                Read.WriteCartItemsInCheckout();
                Console.WriteLine("Tax: " + Helpers.CalculateTax(newOrder.SubTotal) + " SEK");
                Console.WriteLine("Order date: " + newOrder.OrderDate);
                Console.WriteLine("Subtotal: " + newOrder.SubTotal);
                Console.WriteLine("Press [0] to pay");
                int pay = int.Parse(Console.ReadLine());
                if (pay == 0)
                {
                    db.Orders.Add(newOrder);
                    db.SaveChanges();
                    Console.WriteLine("Your payment is done. Welcome back.");
                    //if cartItems isPayed - delete cartitems??
                }

            }


        }
    }
}
