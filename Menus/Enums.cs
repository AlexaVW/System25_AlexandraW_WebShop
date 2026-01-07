using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Menus
{
    public enum AdminMenu
    {
        Edit_Products = 1,
        Edit_Categories,
        Edit_Costumers,
        Show_Statistics,
        Go_Back = 8,
        Exit = 9
    }

    public enum EditProducts
    {
        Edit_Product_Name = 1,
        Edit_Description,
        Edit_Price,
        Edit_Category,
        Edit_Supplier,
        Edit_Units_In_Stock,
        Go_Back = 8,
        Exit = 9
    }

    public enum EditCategories
    {
        Edit_Food = 1,
        Edit_Treats,
        Edit_Toys,
        Edit_Accessories,
        Go_Back = 8,
        Exit = 9
    }

    public enum EditCostumers
    {
        Edit_Information = 1,
        Edit_Order_History,
        Go_Back = 8,
        Exit = 9
    }
}
