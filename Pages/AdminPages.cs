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
                            //Sidan för Edit Categories
                            break;
                        case AdminMenu.Edit_Costumers:
                            PrintMenuEditCostumers();
                            //Sidan för Edit Costumers
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
                        case EditProducts.Edit_Product_Name:
                            //Sida för edit product name
                            break;
                        case EditProducts.Edit_Description:
                            //Sida för edit desctiption
                            break;
                        case EditProducts.Edit_Price:
                            //Sida för edit price
                            break;
                        case EditProducts.Edit_Category:
                            //Sida för edit category
                            break;
                        case EditProducts.Edit_Supplier:
                            //Sida för edit supplier
                            break;
                        case EditProducts.Edit_Units_In_Stock:
                            //Sida för edit units in stock
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
                        case EditCategories.Edit_Food:
                            //Sida för edit food
                            break;
                        case EditCategories.Edit_Treats:
                            //Sida för edit treats
                            break;
                        case EditCategories.Edit_Toys:
                            //Sida för edit toys
                            break;
                        case EditCategories.Edit_Accessories:
                            //Sida för edit Accessories
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
        public static void PrintMenuEditCostumers()
        {
            bool isRunning = true;
            while (isRunning)
            {
                foreach (int i in Enum.GetValues(typeof(EditCostumers)))
                {
                    Console.WriteLine(i + ". " + Enum.GetName(typeof(EditCostumers), i).Replace('_', ' '));
                }
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int num))
                {
                    Console.Clear();
                    switch ((EditCostumers)num)
                    {
                        case EditCostumers.Edit_Information:
                            //Sida för edit information
                            break;
                        case EditCostumers.Edit_Order_History:
                            //Sida för edit order history
                            break;
                        case EditCostumers.Go_Back:
                            isRunning = false;
                            break;
                        case EditCostumers.Exit:
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
