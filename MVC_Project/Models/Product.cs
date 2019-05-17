using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Models
{
    //public enum Category
    //{
    //    Shirt,
    //    Skirt,
    //    Jacket,
    //    Dress
    //}

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public ProductCategory Category{ get; set; }
        public bool isTradable { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<ProductsQuantity> Quantity { get; set; }
    }
}
