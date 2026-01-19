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
        Edit_Orders,
        Show_Statistics,
        Go_Back = 8,
        Exit = 9
    }

    public enum EditProducts
    {
        View_Products = 1,
        Add_Product,
        Update_Product,
        Delete_Product,
        Go_Back = 8,
        Exit = 9
    }

    public enum EditCategories
    {
        View_Categories = 1,
        Add_Category,
        Update_Category,
        Delete_Category,
        Go_Back = 8,
        Exit = 9
    }

    public enum EditOrders
    {
        View_Order_History = 1,
        Update_Order,
        Delete_Order,
        Go_Back = 8,
        Exit = 9
    }

    public enum ShowStatistics
    {
        Show_Best_Selling_Products = 1,
        Show_Most_Popular_Category,
        Show_Most_Popular_Hay,
        Show_Orders_Per_Country,
        Show_Sales_Sorted_By_Supplier,
        Go_Back = 8,
        Exit = 9
    }
}
