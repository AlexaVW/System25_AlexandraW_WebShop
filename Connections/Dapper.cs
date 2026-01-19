using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Webshop.Models;

namespace Webshop.Connections
{
    internal class Dapper
    {
        static string connString = "data source=.\\SQLEXPRESS; initial catalog = WebShop; persist security info = True; Integrated Security = True; TrustServerCertificate=true;";

        public static List<Product> SearchProduct()
        {
            Console.Write("Search product: ");
            string searchString = Console.ReadLine().ToUpper();
            
            var productsSearch = new List<Product>();

            string sql = $"SELECT DISTINCT * " +
                $"FROM Products P " +
                $"WHERE P.Name LIKE '%{searchString}%' OR P.Description LIKE '%{searchString}%' OR P.Supplier LIKE '%{searchString}%'";

            using(var connection  = new SqlConnection(connString))
            {
                productsSearch = connection.Query<Product>(sql).ToList();
            }
            return productsSearch;
        }
        

        
    }
}
