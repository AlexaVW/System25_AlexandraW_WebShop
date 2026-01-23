using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Webshop.Models;
using Webshop.Connections;
using Microsoft.EntityFrameworkCore;

namespace Webshop.Edit
{
    internal class Create
    {
        public static void CreateCategory()
        {
            using (var db = new WebShopDbContext())
            {
                Read.ShowCategories(db);
                Console.WriteLine("Add a new category \n");
                Console.Write("Enter category name: ");
                string categoryName = Console.ReadLine();
                try
                {
                    Category newCategory = new Category(categoryName);
                    db.Categories.Add(newCategory);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ops... Something went wrong");
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static void CreateProduct() 
        {
            using (var db = new WebShopDbContext())
            {
                Read.ShowCategories(db);
                Read.ShowProducts(db);
                Console.WriteLine("Add a new product \n");

                Console.Write("Enter product name: ");
                string productName = Console.ReadLine();

                Console.Write("Enter product price: ");
                double pricePerUnit = double.Parse(Console.ReadLine());

                Console.Write("Enter number of units in stock: ");
                int unitsInStock = int.Parse(Console.ReadLine());

                Console.Write("Enter a description: ");
                string description = Console.ReadLine();

                Console.Write("Enter the name of the supplier: ");
                string supplier = Console.ReadLine();

                Console.Write("Is the product on sale? 1 = Yes. | 2 = No: ");
                bool isOnSale = false;
                int onSale = int.Parse(Console.ReadLine());
                if (onSale == 1)
                {
                    isOnSale = true;
                }
                Console.Write("Enter the categoryId: ");
                int categoryId = int.Parse(Console.ReadLine());
                
                try
                {
                    Product newProduct = new Product(productName, pricePerUnit, unitsInStock, description, supplier, isOnSale, categoryId);
                    db.Products.Add(newProduct);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ops... Something went wrong");
                    Console.WriteLine(ex.Message);
                }
            }
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
                var alreadyInCart = db.CartItems.Where(ci => ci.IsPayed == false).Where(ci => ci.ProductId == productId).SingleOrDefault();
                try
                {
                    if (alreadyInCart != null)
                    {
                        //Om produkten finns - plussa på 1 antal 
                        alreadyInCart.ProductAmount += 1;
                    }
                    else
                    {
                        db.CartItems.Add(newCartItem);
                    }
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Ops... Something went wrong");
                    Console.WriteLine(ex.Message);
                }
            }
        }
       
        public static void CreateOrder()
        {
            string name = "";
            string address = "";
            string country = "";

            string shippingMethod = "";
            string paymentMethod = "";

            int shippingCost = 0;

            //För att visa upp priset bara
            double totalCartPrice = Helpers.CalculateAllCartItemsPrice();

            List<CartItem> cartItems = Helpers.GetCartItemsNotPayed();
            
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
                int selectedShipping;
                bool validShippingInput = int.TryParse(Console.ReadLine(), out selectedShipping);
                if(selectedShipping == 1)
                {
                    shippingMethod = "Express Shipping";
                    shippingCost = 69;
                }
                else
                {
                    shippingMethod = "Basic Shipping";
                    shippingCost = 49;
                }

                paymentMethod = ChoosePaymentMethod();

                //Visa Cart info
                Read.ShowCartItemsInCheckout();
                ShowCheckoutInformation(paymentMethod, totalCartPrice, shippingCost);
                
                List<Order> orders = AddOrderToList(name, address, country, shippingMethod, paymentMethod, db, cartItems);

                ContinueToPay(orders, shippingCost);
            }
        }
        
        public static void Pay(List<Order> orders, int shippingCost)
        {
            Console.WriteLine("Pay Page");
            Console.WriteLine();
            using (var db = new WebShopDbContext())
            {
                double totalPriceInCart = Helpers.CalculateAllCartItemsPrice();
                double totalPriceWithShipping = totalPriceInCart + shippingCost;

                //Skriver ut info igen
                Read.ShowCartItemsInCheckout();
                ShowFinalPrice(shippingCost, totalPriceWithShipping);
                
                ConfirmPayment(orders, db);

                ShowConfirmationText();
            }
        }
        public static string ChoosePaymentMethod()
        {
            Console.WriteLine("Choose payment method");
            Console.WriteLine("[1] Card");
            Console.WriteLine("[2] Klarna");
            int selectedPayment;
            bool validPaymentInput = int.TryParse(Console.ReadLine(), out selectedPayment);

            if (selectedPayment == 1)
            {
                return "Card";
            }
            else
            {
                return "Klarna";
            }

            //return selectedPayment == 1 ? "Card" : "Klarna";
        }

        public static void ShowCheckoutInformation(string paymentMethod, double totalCartPrice, int shippingCost)
        {
            Console.WriteLine("Payment method: " + paymentMethod);
            Console.WriteLine("CartItem price: " + totalCartPrice + " SEK");
            Console.WriteLine("Shipping: " + shippingCost + " SEK");
            Console.WriteLine("Subtotal: " + (totalCartPrice + shippingCost) + " SEK");
        }

        public static void ContinueToPay(List<Order> orders, int shippingCost)
        {
            Console.WriteLine("Press [1] to continue");
            int pay = int.Parse(Console.ReadLine());
            if (pay == 1)
            {
                Console.Clear();
                Pay(orders, shippingCost);
            }
        }

        public static void ShowFinalPrice(int shippingCost, double totalPriceWithShipping)
        {
            Console.WriteLine("Shipping cost: " + shippingCost + " SEK");
            Console.WriteLine("Including tax: " + Helpers.CalculateTax(totalPriceWithShipping) + " SEK");
            Console.WriteLine("Subtotal: " + totalPriceWithShipping + " SEK");
        }

        public static void ConfirmPayment(List<Order> orders, WebShopDbContext db)
        {
            Console.WriteLine("Press [1] to pay");
            int pay;
            bool validInput = int.TryParse(Console.ReadLine(), out pay);

            if (pay == 1)
            {
                DateTime orderDate = DateTime.Now; //Blir samma datum för alla ordar eftersom den ligger utanför loopen
                foreach (Order order in orders)
                {
                    try
                    {
                        db.Orders.Add(order);
                        order.OrderDate = orderDate;
                        var cartItemsToPay = db.CartItems.Where(c => c.IsPayed == false).Include(ci => ci.product).ToList();

                        foreach (var cartItem in cartItemsToPay)
                        {
                            cartItem.product.UnitsInStock -= cartItem.ProductAmount;
                            cartItem.IsPayed = true;
                        }
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ops... Something went wrong");
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public static void ShowConfirmationText()
        {
            Console.WriteLine("Your payment is done. Welcome back.");
            Console.WriteLine("Press any key");
            Console.ReadKey();
            Console.Clear();
            WindowStructure.HomePage();
        }

        public static List<Order> AddOrderToList(string name, string address, string country, string shippingMethod, string paymentMethod, WebShopDbContext db, List<CartItem> cartItems)
        {
            //Loopar igenom, lägger till en order per cartitem.
            List<Order> orders = new List<Order>();
            foreach (CartItem cartItem in cartItems)
            {
                double itemPrice = cartItem.product.PricePerUnit * cartItem.ProductAmount;

                Order newOrder = new Order(name, address, country, shippingMethod, paymentMethod, itemPrice, cartItem.Id);
                orders.Add(newOrder);
            }
            return orders;
        }
    }
}
