using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop
{
    internal class Helpers
    {
        public static List<Product> GetProductsOnSale()
        {
            List<Product> productsOnSale = new List<Product>();
            using (var db = new WebShopDbContext())
            {
                productsOnSale = (from s in db.Products
                                    where s.IsOnSale == true
                                    select s).ToList();
            }
            return productsOnSale;

        }
    }
}
