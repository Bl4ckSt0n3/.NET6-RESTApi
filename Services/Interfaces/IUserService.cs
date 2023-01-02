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
        Task<ServiceResponse<List<GetUser>>> GetAll();
        Task<ServiceResponse<GetUser>> Get(int id);
        Task<ServiceResponse<List<GetUser>>> Remove(int id);
        Task<ServiceResponse<List<GetUser>>> Create (CreateUser newUser);
        Task<ServiceResponse<List<GetUser>>> Update (UpdateUser updateUser);
    }
}