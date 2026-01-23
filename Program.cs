using Webshop.Menus;
using Webshop.Pages;

namespace Webshop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isRunning = true;
            while (isRunning)
            {
                WindowStructure.HomePage();
            }
        }
    }
}
