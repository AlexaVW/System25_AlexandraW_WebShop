using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Webshop.Connections;
using Webshop.Models;

namespace Webshop.Edit
{
    internal class Update
    {
        public static void UpdateCategory()
        {
            using (var db = new WebShopDbContext())
            {
                Read.ShowCategories(db);
                Console.WriteLine("Enter Id:");
                int selectedId;
                bool validIdInput = int.TryParse(Console.ReadLine(), out selectedId);
                var selectedCategory = (from c in db.Categories
                                         where c.Id == selectedId
                                         select c).SingleOrDefault();
                UpdateCategoryName(selectedCategory, db);
            }
        }
        public static void UpdateCategoryName(Category category, WebShopDbContext db)
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

                UpdateDescription(selectedProduct, db);

                UpdateOnSale(selectedProduct, db);

                UpdateCategoryId(selectedProduct, db);
            }
        }

        public static void UpdateProductName(Product product, WebShopDbContext db)
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

        public static void UpdateProductPrice(Product product, WebShopDbContext db)
        {
            if (product != null)
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to not change this column");
                Console.Write("Enter price for the product: ");

                bool validInput = double.TryParse(Console.ReadLine(), out double newPricePerUnit) && newPricePerUnit > 0;

                if (validInput)
                {
                    try
                    {
                        product.PricePerUnit = newPricePerUnit;
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ops... Something went wrong");
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid price");
                    Console.WriteLine("Nothing is changed");
                }
            }
        }
        public static void UpdateUnitsInStock(Product product, WebShopDbContext db)
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

        public static void UpdateDescription(Product product, WebShopDbContext db)
        {
            if (product != null)
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to not change this column");
                Console.Write("Enter a description: ");
                string newDescription = Console.ReadLine();

                if (!string.IsNullOrEmpty(newDescription))
                {
                    try
                    {
                        product.Name = newDescription;
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
        public static void UpdateSupplier(Product product, WebShopDbContext db)
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
                        product.Name = newSupplier;
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

        public static void UpdateOnSale(Product product, WebShopDbContext db)
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
                    int onSale = int.Parse(onSaleString);
                    if (onSale == 1)
                    {
                        newIsOnSale = true;
                        try
                        {
                            product.IsOnSale = newIsOnSale;
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

        public static void UpdateCategoryId(Product product, WebShopDbContext db)
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


        public static void UpdateCustomerInformation()
        {
            using (var db = new WebShopDbContext())
            {
                Read.ShowOrderHistory(db);
                
                Console.Write("Enter Name to change information:");
                string selectedName = Console.ReadLine().ToUpper();
                var selectedOrder = (from o in db.Orders
                                            where o.CustomerName == selectedName
                                            select o).FirstOrDefault();
                
                UpdateCustomerName(selectedOrder, db);

                UpdateCustomerAddress(selectedOrder, db);

                UpdateCustomerCountry(selectedOrder, db);
            }
        }
        public static void UpdateCustomerName(Order order, WebShopDbContext db)
        {
            if (order != null)
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to not change this column");
                Console.Write("Enter new customer name: ");
                var newCustomerName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newCustomerName))
                {
                    try
                    {
                        order.CustomerName = newCustomerName;
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
        public static void UpdateCustomerAddress(Order order, WebShopDbContext db)
        {
            if (order != null)
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to not change this column");
                Console.Write("Enter new address: ");
                var newAddress = Console.ReadLine();
                if (!string.IsNullOrEmpty(newAddress))
                {
                    try
                    {
                        order.ShipAdress = newAddress;
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
        public static void UpdateCustomerCountry(Order order, WebShopDbContext db)
        {
            if (order != null)
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to not change this column");
                Console.Write("Enter new country: ");
                var newCountry = Console.ReadLine();
                if (!string.IsNullOrEmpty(newCountry))
                {
                    try
                    {
                        order.ShipCountry = newCountry;
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
        public static void UpdateCartItem()
        {
            using (var db = new WebShopDbContext())
            {
                Read.ShowCartItems();

                Console.WriteLine("Choose Id to change amount of product");
                int selectedId;
                bool validIdInput = int.TryParse(Console.ReadLine(), out selectedId);
                var selectedCartItem = (from c in db.CartItems
                                        where c.Id == selectedId
                                        select c).SingleOrDefault();
                UpdateCartItemAmount(selectedCartItem, db);
            }
        }

        public static void UpdateCartItemAmount(CartItem cartItem, WebShopDbContext db)
        {
            if (cartItem != null)
            {
                Console.WriteLine("Press [1] to Increase amount of this product");
                Console.WriteLine("Press [2] to Decrease amount of this product");
                int increaseOrDecrease;
                bool validInpus = int.TryParse(Console.ReadLine(),out increaseOrDecrease);
                if (increaseOrDecrease == 1)
                {
                    Console.Write("Amount to Increase: ");
                    int numberToIncrease = int.Parse(Console.ReadLine());
                    try
                    {
                        cartItem.ProductAmount += numberToIncrease;
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
                    int numberToDecrease = int.Parse(Console.ReadLine());
                    try
                    {
                        cartItem.ProductAmount -= numberToDecrease;
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ops... Something went wrong");
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Press 1 or 2 to change amount"); //Skrivs aldrig ut
                }

            }
        }

    }
}
