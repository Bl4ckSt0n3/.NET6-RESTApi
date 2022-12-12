using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Code firs approach (*)
// We create the model classes first and ef core does the rest.(like database process)
namespace dotnet_test.Models
{
    public class User
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? SecondName { get; set; }
        public int Age { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime CreateDate { get; set; }

    }
}