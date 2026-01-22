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
                int selectedId = int.Parse(Console.ReadLine());
                var selectedCategoryName = (from c in db.Categories
                                         where c.Id == selectedId
                                         select c).SingleOrDefault();
                if(selectedCategoryName != null)
                {
                    Console.Write("Update category name: ");
                    var newCategoryName = Console.ReadLine();
                    selectedCategoryName.Name = newCategoryName;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch(Exception ex)
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
                Console.WriteLine("Enter Name:");
                string selectedName = Console.ReadLine().ToUpper();
                var selectedOrders = (from o in db.Orders
                                            where o.CustomerName == selectedName
                                            select o).ToList();
                if (selectedOrders != null)
                {
                    Console.WriteLine("Enter new costumername:");
                    string newCostumerName = Console.ReadLine();

                    Console.WriteLine("Enter new shipping address:");
                    string newShippingAdress = Console.ReadLine();

                    Console.WriteLine("Enter new country:");
                    string newShippingCountry = Console.ReadLine();
                    
                    foreach(var order in selectedOrders)
                    {
                        order.CustomerName = newCostumerName;
                        order.ShipAdress = newShippingAdress;
                        order.ShipCountry = newShippingCountry;
                    }
                    try
                    {
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

                try
                {
                    Console.WriteLine("Choose Id to change amount of product");
                    int selectedId = int.Parse(Console.ReadLine());
                    var selectedProduct = (from c in db.CartItems
                                           where c.Id == selectedId
                                           select c).SingleOrDefault();
                    if (selectedProduct != null)
                    {
                        Console.WriteLine("Press [1] to Increase amount of this product");
                        Console.WriteLine("Press [2] to Decrease amount of this product");
                        int increaseOrDecrease = int.Parse(Console.ReadLine());
                        if (increaseOrDecrease == 1)
                        {
                            Console.Write("Amount to Increase: ");
                            int numberToIncrease = int.Parse(Console.ReadLine());
                            selectedProduct.ProductAmount += numberToIncrease;
                        }
                        else if (increaseOrDecrease == 2)
                        {
                            Console.Write("Amount to Decrease: ");
                            int numberToDecrease = int.Parse(Console.ReadLine());
                            selectedProduct.ProductAmount -= numberToDecrease;
                        }
                        else
                        {
                            Console.WriteLine("Press 1 or 2 to change amount"); //Skrivs aldrig ut
                        }
                        db.SaveChanges();
                    }
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
