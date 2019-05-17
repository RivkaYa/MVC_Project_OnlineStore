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
        [Key][Column(Order = 0)]
        public Product ProdId { get; set; }
        [Key][Column(Order = 1)]
        public ProductSize ItemSize{ get; set; }
        [Key][Column(Order = 2)]
        public ProductColor Color { get; set; }
        public int Quantity { get; set; }
    }

  
}
