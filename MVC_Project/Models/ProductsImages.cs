using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Models
{
    public class ProductsImages
    {
        public int ProdId { get; set; }
        public Product Product { get; set; }

        public int ColorId { get; set; }
        public ProductColor ItemColor { get; set; }
        
        public string ImgSrc { get; set; }
    }
}
