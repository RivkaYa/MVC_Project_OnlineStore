using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<Product> ProductsList { get; set; }
    }
}
