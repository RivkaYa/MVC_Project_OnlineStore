using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Models
{
   
    public class ProductsQuantity
    {
        public int ProdId { get; set; }

        public Product Product { get; set; }

        public int SizeId{ get; set; }
        public ProductSize ProductSize { get; set; }

        public int ColorId { get; set; }
        public ProductColor ProductColor { get; set; }

        public int Quantity { get; set; }
    }

}
