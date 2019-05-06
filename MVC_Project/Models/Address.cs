using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set;}
        public int BuildNumber { get; set;}
        public string City { get; set;}
    }
}
