using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Models
{
    public enum Category
    {
        Shirt,
        Skirt,
        Jacket,
        Dress
    }

    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Img { get; set; }
        public Category category{ get; set; }
        public bool isTradable { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
