using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Menus;

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
                            //Sidan för Edit Products
                            break;
                        case AdminMenu.Edit_Categories:
                            PrintMenuEditCategories();
                            break;
                        case AdminMenu.Edit_Orders:
                            PrintMenuEditOrders();
                            //Sidan för Edit Orders
                            break;
                        case AdminMenu.Show_Statistics:
                            //Sidan för Show Statistics.
                            break;
                        case AdminMenu.Go_Back:
                            isRunning = false;
                            break;
                        case AdminMenu.Exit:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Skriv in en siffra");
                            break;
                    }
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
                            //Sida för edit product name
                            break;
                        case EditProducts.Add_Product:
                            Add.AddProduct();
                            break;
                        case EditProducts.Update_Product:
                            //Sida för edit price
                            break;
                        case EditProducts.Delete_Product:
                            //Sida för edit category
                            break;
                        case EditProducts.Go_Back:
                            isRunning = false;
                            break;
                        case EditProducts.Exit:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Skriv in en siffra");
                            break;
                    }
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
                            
                            break;
                        case EditCategories.Add_Category:
                            Add.AddCategory();
                            break;
                        case EditCategories.Update_Category:
                            
                            break;
                        case EditCategories.Delete_Category:
                            
                            break;
                        case EditCategories.Go_Back:
                            isRunning = false;
                            break;
                        case EditCategories.Exit:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Skriv in en siffra");
                            break;
                    }
                }
                Console.ReadKey();
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
                            //Sida för edit information
                            break;
                        case EditOrders.Update_Order:
                            //Sida för edit order history
                            break;
                        case EditOrders.Delete_Order:
                            //Sida för delete order
                        case EditOrders.Go_Back:
                            isRunning = false;
                            break;
                        case EditOrders.Exit:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Skriv in en siffra");
                            break;
                    }
                }
                Console.ReadKey();
            }
        }

    }
}
