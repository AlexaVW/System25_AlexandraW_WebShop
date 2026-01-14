using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Edit;
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
                            break;
                        case AdminMenu.Edit_Categories:
                            PrintMenuEditCategories();
                            break;
                        case AdminMenu.Edit_Orders:
                            PrintMenuEditOrders();
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
        public static async Task PrintMenuEditProducts()
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
                            await Read.GetProductsAsync(new Models.WebShopDbContext());
                            //Helpers.GetProductsOnSale();
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
                            Read.ReadCategories();
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
                            Read.ReadOrderHistory();
                            break;
                        case EditOrders.Update_Order:
                            Update.UpdateOrder();
                            break;
                        case EditOrders.Delete_Order:
                            //Metod för delete order??
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
