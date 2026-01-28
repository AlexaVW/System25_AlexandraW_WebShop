using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Webshop.Connections;
using Webshop.Models;

namespace Webshop.Edit
{
    internal class Update
    {
        // Update category (Admin)
        public static void UpdateCategory()
        {
            using (var db = new WebShopDbContext())
            {
                Read.ShowCategories(db);
                Console.WriteLine("Enter Id:");
                bool validIdInput = int.TryParse(Console.ReadLine(), out int selectedId);
                var selectedCategory = (from c in db.Categories
                                         where c.Id == selectedId
                                         select c).SingleOrDefault();
                UpdateCategoryName(selectedCategory, db);
            }
        }
        private static void UpdateCategoryName(Category category, WebShopDbContext db)
        {
            if (category != null)
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to not change this column");
                Console.Write("Update category name: ");
                var newCategoryName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newCategoryName))
                {
                    try
                    {
                        category.Name = newCategoryName;
                        db.Update(category);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ops... Something went wrong");
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                }
            }
        }

        // Update product (Admin)
        public static void UpdateProduct()
        {
            using (var db = new WebShopDbContext())
            {
                Read.ShowProducts(db);
                Console.Write("Enter Id: ");
                int selectedId;
                bool validIdInput = int.TryParse(Console.ReadLine(), out selectedId);
                
                var selectedProduct = (from p in db.Products
                                       where p.Id == selectedId
                                       select p).SingleOrDefault();
                
                UpdateProductName(selectedProduct, db);

                UpdateProductPrice(selectedProduct, db);

                UpdateUnitsInStock(selectedProduct, db);

                UpdateDescription(selectedProduct, db);

                UpdateSupplier(selectedProduct, db);

                UpdateOnSale(selectedProduct, db);

                UpdateCategoryId(selectedProduct, db);
            }
        }

        private static void UpdateProductName(Product product, WebShopDbContext db)
        {
            if (product != null)
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to not change this column");
                Console.Write("Enter product name: ");

                string newProductName = Console.ReadLine();

                if (!string.IsNullOrEmpty(newProductName))
                {
                    try
                    {
                        product.Name = newProductName;
                        db.Update(product);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ops... Something went wrong");
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                }
            }
        }

        private static void UpdateProductPrice(Product product, WebShopDbContext db)
        {
            if (product != null)
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to not change this column");
                Console.Write("Enter price for the product: ");

                bool validInput = double.TryParse(Console.ReadLine(), out double newPricePerUnit) && newPricePerUnit > 0;

                // If input is not valid - nothing will change
                if (validInput)
                {
                    try
                    {
                        product.PricePerUnit = newPricePerUnit;
                        db.Update(product);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ops... Something went wrong");
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                }
            }
        }
        private static void UpdateUnitsInStock(Product product, WebShopDbContext db)
        {
            if (product != null)
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to not change this column");
                Console.Write("Enter number of units in stock: ");

                bool validInput = int.TryParse(Console.ReadLine(), out int newUnitsInStock) && newUnitsInStock > 0;
                if (validInput)
                {
                    try
                    {
                        product.UnitsInStock = newUnitsInStock;
                        db.Update(product);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ops... Something went wrong");
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                }
            }
        }

        private static void UpdateDescription(Product product, WebShopDbContext db)
        {
            if (product != null)
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to not change this column");
                Console.Write("Enter a description: ");
                string newDescription = Console.ReadLine();

                // If input is null or empty string - Nothing will change
                if (!string.IsNullOrEmpty(newDescription))
                {
                    try
                    {
                        product.Description = newDescription;
                        db.Update(product);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ops... Something went wrong");
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                }
            }

        }
        private static void UpdateSupplier(Product product, WebShopDbContext db)
        {
            if (product != null)
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to not change this column");
                Console.Write("Enter the name of the supplier: ");
                string newSupplier = Console.ReadLine();

                if (!string.IsNullOrEmpty(newSupplier))
                {
                    try
                    {
                        product.Supplier = newSupplier;
                        db.Update(product);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ops... Something went wrong");
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                }
            }
        }

        private static void UpdateOnSale(Product product, WebShopDbContext db)
        {
            if (product != null)
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to not change this column");
                Console.Write("Is the product on sale? 1 = Yes. 2 = No: ");

                bool newIsOnSale = false;
                string onSaleString = Console.ReadLine();
                if (!string.IsNullOrEmpty(onSaleString))
                {
                    try
                    {
                        int onSale = int.Parse(onSaleString);
                        if (onSale == 1)
                        {
                            
                            newIsOnSale = true;
                        }
                        else if (onSale == 2)
                        {
                            newIsOnSale = false;
                        }

                        product.IsOnSale = newIsOnSale;
                        db.Update(product);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ops... Something went wrong");
                        Console.WriteLine("This Update won't be saved");
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                }
            }
        }

        private static void UpdateCategoryId(Product product, WebShopDbContext db)
        {
            if (product != null)
            {
                Console.WriteLine();
                Read.ShowCategories(db);
                Console.WriteLine("Press Enter to not change this column");
                Console.Write("Enter the categoryId: ");

                bool validInput = int.TryParse(Console.ReadLine(), out int newCategoryId) && newCategoryId > 0;
                if (validInput)
                {
                    try
                    {
                        product.CategoryId = newCategoryId;
                        db.Update(product);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ops... Something went wrong");
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                }
            }
        }

        // Update Customer information (Admin)
        public static void UpdateCustomerInformation()
        {
            using (var db = new WebShopDbContext())
            {
                // The Order groups that are grouped by OrderDate
                var orderGroups = Read.ShowOrderHistoryAndGetOrderNumber(db);
                
                // Choosing one of the orders. -1 because the list starts on 0
                Console.Write("Choose order number to update information about the customer: ");
                bool validInput = int.TryParse(Console.ReadLine(), out int selectedOrderNumber) && selectedOrderNumber <= orderGroups.Count;
                selectedOrderNumber -= 1;

                if (validInput)
                {
                    // Saving the selected OrderGroup
                    var selectedOrderGroup = orderGroups[selectedOrderNumber];

                    // (One order is made for every cart item)
                    // Making it to a list incase there are more than one order
                    var selectedOrder = selectedOrderGroup.ToList();

                    UpdateCustomerName(selectedOrder, db);

                    UpdateCustomerAddress(selectedOrder, db);

                    UpdateCustomerCountry(selectedOrder, db);
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }

        private static void UpdateCustomerName(List<Order> orders, WebShopDbContext db)
        {
            if (orders != null)
            {
                Console.WriteLine("\nPress Enter to not change this column");
                Console.Write("Enter new customer name: ");
                var newCustomerName = Console.ReadLine();
                
                // If the string is not empty
                if (!string.IsNullOrEmpty(newCustomerName))
                {
                    try
                    {
                        // Looping through every order incase the customer has more than one cart item. 
                        foreach(var order in orders)
                        {
                            // Updating CustomerName for every order
                            order.CustomerName = newCustomerName;
                        }
                        // Updating the whole list of orders
                        db.UpdateRange(orders);

                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ops... Something went wrong");
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                }
            }
        }
        private static void UpdateCustomerAddress(List<Order> orders, WebShopDbContext db)
        {
            if (orders != null)
            {
                Console.WriteLine("\nPress Enter to not change this column");
                Console.Write("Enter new address: ");
                var newAddress = Console.ReadLine();
                if (!string.IsNullOrEmpty(newAddress))
                {
                    try
                    {
                        foreach (var order in orders)
                        {
                            order.ShipAdress = newAddress;
                            
                        }
                        db.UpdateRange(orders);
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
        private static void UpdateCustomerCountry(List<Order> orders, WebShopDbContext db)
        {
            if (orders != null)
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to not change this column");
                Console.Write("Enter new country: ");
                var newCountry = Console.ReadLine();
                if (!string.IsNullOrEmpty(newCountry))
                {
                    try
                    {
                        foreach (var order in orders)
                        {
                            order.ShipCountry = newCountry;
                        }
                        db.UpdateRange(orders);
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
        
        // Update CartItem (Customer)
        public static void UpdateCartItem()
        {
            using (var db = new WebShopDbContext())
            {
                Read.ShowCartItems();

                Console.WriteLine("Choose Id to change amount of product");
                bool validIdInput = int.TryParse(Console.ReadLine(), out int selectedId);
                var selectedCartItem = (from c in db.CartItems
                                        where c.Id == selectedId
                                        select c).SingleOrDefault();
                UpdateCartItemAmount(selectedCartItem, db);
            }
        }

        // Update CartItem Amount (Customer)
        private static void UpdateCartItemAmount(CartItem cartItem, WebShopDbContext db)
        {
            if (cartItem != null)
            {
                Console.WriteLine("Press [1] to Increase amount of this product");
                Console.WriteLine("Press [2] to Decrease amount of this product");
                bool validInput = int.TryParse(Console.ReadLine(),out int increaseOrDecrease);
                if (increaseOrDecrease == 1)
                {
                    Console.Write("Amount to Increase: ");
                    int numberToIncrease;
                    bool validIncreaseInput = int.TryParse(Console.ReadLine(), out numberToIncrease);
                    try
                    {
                        cartItem.ProductAmount += numberToIncrease;
                        db.Update(cartItem);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ops... Something went wrong");
                        Console.WriteLine(ex.Message);
                    }

                }
                else if (increaseOrDecrease == 2)
                {
                    Console.Write("Amount to Decrease: ");

                    int numberToDecrease;
                    bool validDecreaseInput= int.TryParse(Console.ReadLine(), out numberToDecrease);
                    try
                    {
                        // Decreases the amount of product
                        cartItem.ProductAmount -= numberToDecrease;
                        // If the amount becomes 0 the product is deleted from the cart
                        if (cartItem.ProductAmount < 1)
                        {
                            db.CartItems.Remove(cartItem);
                        }
                        db.Update(cartItem);
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
    }
}
