using Webshop.Menus;
using WindowDemo;

namespace Webshop
{
    internal class Program
    {
        //To Do: Ladda upp på Git
        //To Do: Fixa Console.Clear
        //To Do: Fixa så man kan skriva stor/liten bokstav
        
        static void Main(string[] args)
        {
            bool isRunning = true;
            while (isRunning)
            {
                WindowStructure.DrawHomePage();
                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.Clear();
                switch (key.KeyChar)
                {
                    case 'a':
                        Pages.AdminPages.PrintMenuFirstAdminPage();
                        break;
                    case '1':
                        Pages.CostumerPages.ShoppingPage();
                        break;
                    case '2':
                        Pages.CostumerPages.CartPage();
                        break;
                    case '9':
                        Environment.Exit(0);
                        break;

                }
            }
        }
    }
}
