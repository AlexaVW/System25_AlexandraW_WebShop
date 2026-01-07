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
        public int CategoryId { get; set; } //FK
        
    }
}
