using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_test.Models;

namespace dotnet_test.Services
{
    public interface IUserService
    {
        List<User> GetAll();
        User Get(int id);
        List<User> Remove(int id);
        List<User> Create (User newUser);
        List<User> Update (User updtUser);
    }
}