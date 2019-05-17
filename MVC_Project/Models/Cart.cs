using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Models
{
    public class Cart
    {
        public int id { get; set; }
        public Product ProdId { get; set; }
        public ProductSize ItemSize { get; set; }
        public ProductColor ItemColor { get; set; }
    }
}
