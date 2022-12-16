using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_test.Models;
using dotnet_test.Models.Dtos;

namespace dotnet_test.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        User Get(int id);
        Task<List<User>> Remove(int id);
        Task<CreateUser> Create (CreateUser newUser);
        Task<User> Update (UpdateUser updateUser);
    }
}