using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Models
{
    public class ProductSize
    {
        public int Id { get; set; }
        public string Size { get; set; }

        public ICollection<ProductsQuantity> Quantity { get; set; }
        public ICollection<Cart> CartItems { get; set; }
        public ICollection<OrdersHistoryProductsList> Orders { get; set; }
    }
}
