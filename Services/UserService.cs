using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_test.Models;
using dotnet_test.Models.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace dotnet_test.Services
{
    public class UserService : IUserService // has been derived from IUserService interface
    {
        private static readonly string[] Names = new[]
        {
            "Sam", "Jhon", "Alp", "Dany", "Enes", "Tonny", "Jack"
        };

        private static readonly string[] secondNames = new[]
        {
            "Do", "Alias", "Gergin", "Woo", "Bedevi", "Wrackler", "Taylor"
        };
        private static List<User> userList = new List<User>();

        // access database via usercontext
        private readonly UserContext _userContext;
        private readonly IMapper _mapper;
        

        // constructor
        public UserService(
            UserContext userContext, 
            IMapper mapper
        ) 
        {
            // Dependency injection
            _userContext = userContext;
            _mapper = mapper;

            // creating a random list and return it.
            for (int i = 0; i < 10; i++)
            {
                userList.Add
                (
                    new User
                    {
                        Id = i,
                        Name = Names[Random.Shared.Next(Names.Length)],
                        SecondName = secondNames[Random.Shared.Next(Names.Length)],
                        Age = Random.Shared.Next(18, 30),
                        Username = "NewUser_" + Names[Random.Shared.Next(Names.Length)],
                        CreateDate = DateTime.Now,
                    }
                );
            }


        }
        public async Task<CreateUser> Create(CreateUser newUser)
        {
            
            // userList.Add(newUser); // increase id by manuel ops.
            User user = new User();
            user = _mapper.Map<User>(newUser);
            await _userContext.Users.AddAsync(user);
            await _userContext.SaveChangesAsync();
            return newUser;
        }

        public User Get(int id)
        {
            var usrObj = userList.FirstOrDefault(usr => usr.Id == id);
            return usrObj != null ? usrObj : new User(); // return null user
        }

        public async Task<List<User>> GetAll()
        {
            List<User> userDbList = await _userContext.Users.ToListAsync();
            return userDbList;
        }

        public async Task<List<User>> Remove(int id)
        {
            // var usrLst =  new List<User> (userList.Where<User>(usr => usr.Id != id));
            User user = _mapper.Map<User>(_userContext.Users.FirstOrDefault(usr => usr.Id == id));
            _userContext.Remove(user);
            await _userContext.SaveChangesAsync();
            List<User> userDbList = await _userContext.Users.ToListAsync();
            return  userDbList;
        }

        public async Task<User> Update(UpdateUser updateUser)
        {
            User user = new User();
            user = _mapper.Map<User>(_userContext.Users.FirstOrDefault(usr => usr.Id == updateUser.Id));
            try 
            {
                if (user != null) 
                {
                    user.Name = updateUser.Name;
                    user.SecondName = updateUser.SecondName;
                    user.Age = updateUser.Age;
                    user.Username = updateUser.Username;
                    user.Password = updateUser.Password;
                    _userContext.Users.Update(user);
                    await _userContext.SaveChangesAsync();
                }
                
                
            }
            catch(Exception ex) {
                var message = ex; // used instead serviceresponse
            }
            return user != null ? user : new User();
        }
    }
}