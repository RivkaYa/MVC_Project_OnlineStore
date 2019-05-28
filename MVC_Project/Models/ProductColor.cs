using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Models
{
    public class ProductColor
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string RGB { get; set; }

        public ICollection<ProductsQuantity> Quantity { get; set; }
        public ICollection<OrdersHistoryProductsList> Orders { get; set; }
        public ICollection<ProductsImages> Images { get; set; }
    }
}
