using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public ICollection<ProductsQuantity> productList { get; set; }

        public User Customer { get; set; }
    }
}
