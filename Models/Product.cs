using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Models
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double PricePerUnit { get; set; }
        public int UnitsInStock { get; set; }
        public string Description { get; set; }
        public string Supplier { get; set; }
        public bool IsOnSale { get; set; }
        public int CategoryId { get; set; } //FK
        public Category Category { get; set; } //Gör så det är lätt att komma åt i koden

        public Product(string name, double pricePerUnit, int unitsInStock, string description, string supplier, bool isOnSale, int categoryId)
        {
            Name = name;
            PricePerUnit = pricePerUnit;
            UnitsInStock = unitsInStock;
            Description = description;
            Supplier = supplier;
            IsOnSale = isOnSale;
            CategoryId = categoryId;
            
        }
        public Product()
        {

        }

    }
}
