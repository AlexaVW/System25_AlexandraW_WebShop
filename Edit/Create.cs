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
        
        public static void CreateCartItem(Product product)
        {
            using (var db  = new WebShopDbContext())
            {
                int productAmount = 1; // Amount starts with 1
                bool isPaid = false; // isPaid is false until after payment of the cart item
                int productId = product.Id;
                
                CartItem newCartItem = new CartItem(productAmount, isPaid, productId);
                // If the product with the same id is already in cart = Add to cart, else = add product amount
                var alreadyInCart = db.CartItems.Where(ci => ci.IsPaid == false).Where(ci => ci.ProductId == productId).SingleOrDefault();
                try
                {
                    if (alreadyInCart != null)
                    {
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

            // To show total cart price
            double totalCartPrice = Helpers.CalculateAllCartItemsPrice();

            List<CartItem> cartItems = Helpers.GetCartItemsNotPaid();
            
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

                //Show cart info
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

                //Show cart info again
                Read.ShowCartItemsInCheckout();
                ShowFinalPrice(shippingCost, totalPriceWithShipping);
                
                ConfirmPayment(orders, db);

                
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
            int pay;
            bool validInput = int.TryParse(Console.ReadLine(), out pay); 
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
            //var myOrders = db.Orders.Where(o => o.CartItem.IsPaid == false).Include(o => o.CartItem).ThenInclude(c => c.product);
            Console.WriteLine("Press [1] to pay");
            int pay;
            bool validInput = int.TryParse(Console.ReadLine(), out pay);

            if (pay == 1)
            {
                string message = "";
                bool isInStock = true;
                try
                {
                    // All orders in this order gets the same date
                    DateTime orderDate = DateTime.Now;

                    // Looping through one order per cart item
                    foreach (Order order in orders) 
                    {
                        //Adding order to the database
                        db.Orders.Add(order);
                        
                        // Setting todays orderDate
                        order.OrderDate = orderDate;
                        
                        // Need to get the cart items where cart item id are the same as order.CartItemId. Need to include cartitem.product.
                        // Because the order is not in the database yet
                        var cartItem = db.CartItems.Where(c => c.Id == order.CartItemId).Include(c => c.product).SingleOrDefault();

                        // If the amount of the product is more than units in stock - Payment failed
                        if (order.CartItem.ProductAmount > cartItem.product.UnitsInStock)
                        {
                            isInStock = false;
                            message += "There are not enough of this product in stock: " + order.CartItem.product.Name + "\n";
                        }
                        else
                        {
                            // If there are enough products in stock - Units in stock decreases with amount of product and the cartitem is paid
                            order.CartItem.product.UnitsInStock -= order.CartItem.ProductAmount;
                            order.CartItem.IsPaid = true;
                        }

                    }
                    // If the product is in stock - Save changes
                    if (isInStock == true)
                    {
                        db.SaveChanges();
                        message = "Your payment is done";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ops... Something went wrong");
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine();
                Console.WriteLine(message); // message depends on if the payment went through or not
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
                WindowStructure.HomePage();
            }
        }

        

        public static List<Order> AddOrderToList(string name, string address, string country, string shippingMethod, string paymentMethod, WebShopDbContext db, List<CartItem> cartItems)
        {
            // Loops through every cartitem, adds one order per cartitem
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
