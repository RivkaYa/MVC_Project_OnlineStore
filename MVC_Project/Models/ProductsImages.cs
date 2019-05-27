using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Models
{
    public class ProductsImages
    {
        public int ProdId { get; set; }
        public ProductColor ItemColor { get; set; }
        public List<string> Img { get; set; }
    }
}
