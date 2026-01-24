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
        public bool IsPaid { get; set; }
        public int ProductId { get; set; } //FK
        public Product product { get; set; }

        // One cart item can have one product

        public CartItem(int productAmount, bool isPaid, int productId)
        {
            ProductAmount = productAmount;
            IsPaid = isPaid;
            ProductId = productId;
        }
        public CartItem()
        {
            
        }
    }
}
