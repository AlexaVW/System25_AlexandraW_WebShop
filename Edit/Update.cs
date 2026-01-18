using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop.Edit
{
    internal class Update
    {
        public static void UpdateCategory()
        {
            using (var db = new WebShopDbContext())
            {
                Read.ReadCategories();
                Console.WriteLine("Enter Id:");
                int selectedId = int.Parse(Console.ReadLine());
                var selectedCategoryName = (from c in db.Categories
                                         where c.Id == selectedId
                                         select c).SingleOrDefault();
                if(selectedCategoryName != null)
                {
                    Console.Write("Update categoryname: ");
                    var newCategoryName = Console.ReadLine();
                    selectedCategoryName.Name = newCategoryName;
                    db.SaveChanges();
                }
            }
        }

        public static void UpdateProduct()
        {
            using (var db = new WebShopDbContext())
            {
                Read.GetProductsAsync(new Models.WebShopDbContext());
                Console.WriteLine("Enter Id:");
                int selectedId = int.Parse(Console.ReadLine());
                var selectedProduct = (from p in db.Products
                                    where p.Id == selectedId
                                    select p).SingleOrDefault();
                if(selectedProduct != null)
                {
                    Console.WriteLine("Enter product name:");
                    string newProductName = Console.ReadLine();
                    selectedProduct.Name = newProductName;

                    Console.WriteLine("Enter price for the product:");
                    double newPricePerUnit = double.Parse(Console.ReadLine());
                    selectedProduct.PricePerUnit = newPricePerUnit;

                    Console.WriteLine("Enter number of units in stock:");
                    int newUnitsInStock = int.Parse(Console.ReadLine());
                    selectedProduct.UnitsInStock = newUnitsInStock;

                    Console.WriteLine("Enter a description:");
                    string newDescription = Console.ReadLine();
                    selectedProduct.Description = newDescription;

                    Console.WriteLine("Enter the name of the supplier:");
                    string newSupplier = Console.ReadLine();
                    selectedProduct.Supplier = newSupplier;

                    Console.WriteLine("Is the product on sale? 1 = Yes. 0 = No");
                    bool newIsOnSale = false;
                    int onSale = int.Parse(Console.ReadLine());
                    if (onSale == 1)
                    {
                        newIsOnSale = true;
                    }
                    selectedProduct.IsOnSale = newIsOnSale;

                    Console.WriteLine("Enter the categoryId: ");
                    int newCategoryId = int.Parse(Console.ReadLine());
                    selectedProduct.CategoryId = newCategoryId;
                    db.SaveChanges();
                }
            }
        }
        public static void UpdateOrder()
        {
            using (var db = new WebShopDbContext())
            {
                Read.ReadOrderHistory();
                Console.WriteLine("Enter Id:");
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
                    // Vad händer om man skriver ogiltigt namn?

                    db.SaveChanges();
                }
            }
        }

        public static void UpdateCartItem()
        {
            bool isRunning = true;
            while (isRunning)
            {
                using (var db = new WebShopDbContext())
                {
                    Read.WriteCartItems();

                    Console.WriteLine("Choose Id to change amount of product");
                    int selectedId = int.Parse(Console.ReadLine());
                    var selectedProduct = (from c in db.CartItems
                                           where c.Id == selectedId
                                           select c).SingleOrDefault();
                    if (selectedProduct != null)
                    {
                        Console.WriteLine("Increase amount of this product: 1");
                        Console.WriteLine("Decrease amount of this product: 2");
                        int increaseOrDecrease = int.Parse(Console.ReadLine());
                        if (increaseOrDecrease == 1)
                        {
                            selectedProduct.ProductAmount += 1;
                            Console.WriteLine("Added 1");
                        }
                        else if (increaseOrDecrease == 2)
                        {
                            selectedProduct.ProductAmount -= 1;
                            Console.WriteLine("Removed 1");
                        }
                        else
                        {
                            Console.WriteLine("Press 1 or 2 to change amount");
                        }
                        db.SaveChanges();
                        Console.Clear();
                    }
                }
            
                
                
                

                

            }
        }
    }
}
