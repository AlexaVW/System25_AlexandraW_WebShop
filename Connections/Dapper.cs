using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Webshop.Models;

namespace Webshop.Connections
{
    internal class Dapper
    {
        // Search with dapper
        
        static string connString = GetConnstring();

        private static string GetConnstring()
        {
            var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

            var connStr = config["MySettings:ConnectionStringAzure"]; 
            return connStr;
        }
        
        // Returns a list of products with matching search string
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
