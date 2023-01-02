using System;
using dotnet_test.Models.Dtos;
using dotnet_test.Models;
using AutoMapper;

namespace dotnet_test
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, CreateUser>().ReverseMap();
            CreateMap<User, GetUser>().ReverseMap();
            CreateMap<GetUser, User>().ReverseMap();
            CreateMap<CreateUser, GetUser>().ReverseMap();
            CreateMap<GetUser, CreateUser>().ReverseMap();
            CreateMap<CreateUser, User>().ReverseMap();
            CreateMap<User, UpdateUser>().ReverseMap();
        }
    }
}