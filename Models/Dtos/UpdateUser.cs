using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models.Dtos
{
    public class UpdateUser
    {
        public long Id {get; set; }
        public string? Name { get; set; }
        public string? SecondName { get; set; }
        public int Age { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime CreateDate { get; set; }
    }
}