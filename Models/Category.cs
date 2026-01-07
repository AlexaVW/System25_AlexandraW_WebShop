using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Webshop.Models
{
    internal class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //En kategori kan ha många produkter
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
