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
                Read.ReadProducts();
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
                int selectedId = int.Parse(Console.ReadLine());
                var selectedOrder = (from c in db.Orders
                                            where c.Id == selectedId
                                            select c).SingleOrDefault();
                if (selectedOrder != null)
                {
                    Console.WriteLine("Enter new costumername:");
                    string newCostumerName = Console.ReadLine();
                    selectedOrder.CustomerName = newCostumerName;

                    Console.WriteLine("Enter new shipping address:");
                    string newShippingAdress = Console.ReadLine();
                    selectedOrder.ShipAdress = newShippingAdress;

                    Console.WriteLine("Enter new country:");
                    string newShippingCountry = Console.ReadLine();
                    selectedOrder.ShipCountry = newShippingCountry;

                    db.SaveChanges();
                }
            }
        }
    }
}
