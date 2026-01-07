using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Webshop.Models
{
    internal class CartItem
    {
        public int Id { get; set; }
        public int ProductAmount { get; set; }
        public bool IsPayed { get; set; }
        public int ProductId { get; set; } //FK

        //Ett cart item kan ha en produkt

        public CartItem(int productAmount, bool isPayed, int productId)
        {
            ProductAmount = productAmount;
            IsPayed = isPayed;
            ProductId = productId;
        }
        public CartItem()
        {
            
        }


    }
}
