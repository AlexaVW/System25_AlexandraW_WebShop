using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;
using Webshop.Connections;

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
                int selectedId = int.Parse(Console.ReadLine());
                var selectedProduct = (from p in db.Products
                                    where p.Id == selectedId
                                    select p).SingleOrDefault();
                if(selectedProduct != null)
                {
                    Console.Write("Enter product name: ");
                    string newProductName = Console.ReadLine();
                    selectedProduct.Name = newProductName;

                    Console.Write("Enter price for the product: ");
                    double newPricePerUnit = double.Parse(Console.ReadLine());
                    selectedProduct.PricePerUnit = newPricePerUnit;

                    Console.Write("Enter number of units in stock: ");
                    int newUnitsInStock = int.Parse(Console.ReadLine());
                    selectedProduct.UnitsInStock = newUnitsInStock;

                    Console.Write("Enter a description: ");
                    string newDescription = Console.ReadLine();
                    selectedProduct.Description = newDescription;

                    Console.Write("Enter the name of the supplier: ");
                    string newSupplier = Console.ReadLine();
                    selectedProduct.Supplier = newSupplier;

                    Console.Write("Is the product on sale? 1 = Yes. 2 = No: ");
                    bool newIsOnSale = false;
                    int onSale = int.Parse(Console.ReadLine());
                    if (onSale == 1)
                    {
                        newIsOnSale = true;
                    }
                    selectedProduct.IsOnSale = newIsOnSale;

                    Console.Write("Enter the categoryId: ");
                    int newCategoryId = int.Parse(Console.ReadLine());
                    selectedProduct.CategoryId = newCategoryId;
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
                Read.ShowCartItems(db);

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
