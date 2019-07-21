using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Models
{
    public enum UserType
    {
        Customer,
        Manager,
        Provider
    }
    public class User
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }
        public Address Address { get; set; }
        public double PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Cart cart { get; set; }


    }
}
