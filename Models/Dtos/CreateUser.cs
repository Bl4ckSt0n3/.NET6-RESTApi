using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models.Dtos
{
    public class CreateUser
    {
        public string? Name { get; set; }
        public string? SecondName { get; set; }
        public int Age { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime CreateDate = DateTime.UtcNow;
    }
}