using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Edit;
using Webshop.Menus;
using Webshop.Connections;

namespace Webshop.Pages
{
    internal class AdminPages
    {
        public static void PrintMenuFirstAdminPage()
        {
            bool isRunning = true;
            while (isRunning)
            {
                foreach (int i in Enum.GetValues(typeof(AdminMenu)))
                {
                    Console.WriteLine(i + ". " + Enum.GetName(typeof(AdminMenu), i).Replace('_', ' '));
                }
                if(int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int num))
                {
                    Console.Clear();
                    switch ((AdminMenu)num)
                    {
                        case AdminMenu.Edit_Products:
                            PrintMenuEditProducts();
                            break;
                        case AdminMenu.Edit_Categories:
                            PrintMenuEditCategories();
                            break;
                        case AdminMenu.Edit_Orders:
                            PrintMenuEditOrders();
                            break;
                        case AdminMenu.Show_Statistics:
                            PrintMenuShowStatistics();
                            break;
                        case AdminMenu.Go_Back:
                            isRunning = false;
                            break;
                        case AdminMenu.Exit:
                            Environment.Exit(0);
                            break;
                    }
                    Console.Clear();
                }
                
            }
        }
        public static void PrintMenuEditProducts()
        {
            bool isRunning = true;
            while (isRunning)
            {
                foreach (int i in Enum.GetValues(typeof(EditProducts)))
                {
                    Console.WriteLine(i + ". " + Enum.GetName(typeof(EditProducts), i).Replace('_', ' '));
                }
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int num))
                {
                    Console.Clear();
                    switch ((EditProducts)num)
                    {
                        case EditProducts.View_Products:
                            Read.ShowProducts(new WebShopDbContext());
                            Console.ReadKey();
                            break;
                        case EditProducts.Add_Product:
                            Create.CreateProduct();
                            break;
                        case EditProducts.Update_Product:
                            Update.UpdateProduct();
                            break;
                        case EditProducts.Delete_Product:
                            Delete.DeleteProduct();
                            break;
                        case EditProducts.Go_Back:
                            isRunning = false;
                            break;
                        case EditProducts.Exit:
                            Environment.Exit(0);
                            break;
                    }
                    Console.Clear();
                }
            }


        }
        public static void PrintMenuEditCategories()
        {
            bool isRunning = true;
            while (isRunning)
            {
                foreach (int i in Enum.GetValues(typeof(EditCategories)))
                {
                    Console.WriteLine(i + ". " + Enum.GetName(typeof(EditCategories), i).Replace('_', ' '));
                }
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int num))
                {
                    Console.Clear();
                    switch ((EditCategories)num)
                    {
                        case EditCategories.View_Categories:
                            Read.ShowCategories(new WebShopDbContext());
                            Console.ReadKey();
                            break;
                        case EditCategories.Add_Category:
                            Create.CreateCategory();
                            break;
                        case EditCategories.Update_Category:
                            Update.UpdateCategory();
                            break;
                        case EditCategories.Delete_Category:
                            Delete.DeleteCategory();
                            break;
                        case EditCategories.Go_Back:
                            isRunning = false;
                            break;
                        case EditCategories.Exit:
                            Environment.Exit(0);
                            break;
                    }
                    Console.Clear();
                }
            }
        }
        public static void PrintMenuEditOrders()
        {
            bool isRunning = true;
            while (isRunning)
            {
                foreach (int i in Enum.GetValues(typeof(EditOrders)))
                {
                    Console.WriteLine(i + ". " + Enum.GetName(typeof(EditOrders), i).Replace('_', ' '));
                }
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int num))
                {
                    Console.Clear();
                    switch ((EditOrders)num)
                    {
                        case EditOrders.View_Order_History:
                            Read.ShowOrderHistoryAndGetOrderNumber(new WebShopDbContext());
                            //Read.ShowOrderHistory(new WebShopDbContext());
                            Console.ReadKey();
                            break;
                        case EditOrders.Update_Customer_Information:
                            Update.UpdateCustomerInformation();
                            break;
                        case EditOrders.Delete_Order:
                            Delete.DeleteOrder();
                            break;
                        case EditOrders.Go_Back:
                            isRunning = false;
                            break;
                        case EditOrders.Exit:
                            Environment.Exit(0);
                            break;
                    }
                    Console.Clear();
                }
            }
        }

        public static void PrintMenuShowStatistics()
        {
            bool isRunning = true;
            while (isRunning)
            {
                foreach (int i in Enum.GetValues(typeof(ShowStatistics)))
                {
                    Console.WriteLine(i + ". " + Enum.GetName(typeof(ShowStatistics), i).Replace('_', ' '));
                }
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int num))
                {
                    Console.Clear();
                    switch ((ShowStatistics)num)
                    {
                        case ShowStatistics.Show_Best_Selling_Products:
                            Statistics.GetBestSellingProducts();
                            Console.ReadKey();
                            break;
                        case ShowStatistics.Show_Most_Popular_Category:
                            Statistics.GetBestSellingCategories();
                            Console.ReadKey();
                            break;
                        case ShowStatistics.Show_Most_Popular_Hay:
                            Statistics.GetBestSellingHay();
                            Console.ReadKey();
                            break;
                        case ShowStatistics.Show_Sales_Sorted_By_Supplier:
                            Statistics.GetSalesOrderedBySupplier();
                            Console.ReadKey();
                            break;
                        case ShowStatistics.Go_Back:
                            isRunning = false;
                            break;
                        case ShowStatistics.Exit:
                            Environment.Exit(0);
                            break;
                    }
                    Console.Clear();
                }
                
            }
        }
    }
}
