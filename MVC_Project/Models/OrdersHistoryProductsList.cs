using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Models
{
    public class OrdersHistoryProductsList
    {
        public int OrderId { get; set; }
        public OrdersHistory OrdersHistory { get; set; }

        public int ProdId { get; set; }
        public Product Product { get; set; }

        public int SizeId { get; set; }
        public ProductSize ProductSize { get; set; }

        public int ColorId { get; set; }
        public ProductColor ProductColor { get; set; }

        public int Quantity { get; set; }
    }
}
