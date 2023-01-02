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
            // for (int i = 0; i < 10; i++)
            // {
            //     userList.Add
            //     (
            //         new User
            //         {
            //             Id = i,
            //             Name = Names[Random.Shared.Next(Names.Length)],
            //             SecondName = secondNames[Random.Shared.Next(Names.Length)],
            //             Age = Random.Shared.Next(18, 30),
            //             Username = "NewUser_" + Names[Random.Shared.Next(Names.Length)],
            //             CreateDate = DateTime.Now,
            //         }
            //     );
            // }


        }
        public async Task<ServiceResponse<List<GetUser>>> Create(CreateUser newUserParam)
        {
            ServiceResponse<List<GetUser>> serviceResponse = new ServiceResponse<List<GetUser>>();
            try 
            {
                User user = _mapper.Map<User>(newUserParam);
                
                await _userContext.Users.AddAsync(user);
                await _userContext.SaveChangesAsync();
                
                // map data incoming from database to dto 
                serviceResponse.Data = (_userContext.Users.Select(u => _mapper.Map<GetUser>(u))).ToList(); // _userContext.User.tolistasync()
                serviceResponse.Success = true;
                serviceResponse.Message = "Success!";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                serviceResponse.Success = false;
                serviceResponse.Message = "Error";
            }

            return serviceResponse;
            
            // userList.Add(newUser); // increase id by manuel ops.
            // User user = new User();
            // user = _mapper.Map<User>(newUser);
            // await _userContext.Users.AddAsync(user);
            // await _userContext.SaveChangesAsync();
            // return newUser;
        }

        public async Task<ServiceResponse<GetUser>> Get(int id)
        {
            ServiceResponse<GetUser> serviceResponse = new ServiceResponse<GetUser>();
            // var usrObj = userList.FirstOrDefault(usr => usr.Id == id);
            // return usrObj != null ? usrObj : new User(); // return null user
            User user = await _userContext.Users.FirstAsync(usr => usr.Id == id);
            try
            {
                serviceResponse.Data = _mapper.Map<GetUser>(user);
                serviceResponse.Message = "Success";
                serviceResponse.Success = true;
            }
            catch (Exception e)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Error, " + e.ToString();
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUser>>> GetAll()
        {
            ServiceResponse<List<GetUser>> serviceResponse = new ServiceResponse<List<GetUser>>();
            try
            {
                List<User> dbUsers = await _userContext.Users.ToListAsync(); // this is how we use 'await' 
                serviceResponse.Data = dbUsers.Select(usr => _mapper.Map<GetUser>(usr)).ToList();
                serviceResponse.Message = "success";
                serviceResponse.Success = true;
            }
            catch (Exception e) 
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Error, " + e.ToString();
                serviceResponse.Success = false;
            }
            
            // List<User> userDbList = await _userContext.Users.ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUser>>> Remove(int id)
        {
            ServiceResponse<List<GetUser>> serviceResponse = new ServiceResponse<List<GetUser>>();
            User user = _mapper.Map<User>(_userContext.Users.FirstOrDefault(usr => usr.Id == id));
            try
            {
                _userContext.Remove(user);
                await _userContext.SaveChangesAsync();
                List<User> userDbList = await _userContext.Users.ToListAsync();
                serviceResponse.Data = userDbList.Select(usr => _mapper.Map<GetUser>(usr)).ToList();
                serviceResponse.Message = "success";
                serviceResponse.Success = true;
            }
            catch (Exception e) 
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Error, " + e.ToString();
                serviceResponse.Success = false;
            }
            // var usrLst =  new List<User> (userList.Where<User>(usr => usr.Id != id));
            // User user = _mapper.Map<User>(_userContext.Users.FirstOrDefault(usr => usr.Id == id));
            // _userContext.Remove(user);
            // await _userContext.SaveChangesAsync();
            // List<User> userDbList = await _userContext.Users.ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUser>>> Update(UpdateUser updateUser)
        {
            ServiceResponse<List<GetUser>> serviceResponse = new ServiceResponse<List<GetUser>>();
            User user = _mapper.Map<User>(_userContext.Users.FirstOrDefault(usr => usr.Id == updateUser.Id));
            try 
            {
                user.Name = updateUser.Name;
                user.SecondName = updateUser.SecondName;
                user.Age = updateUser.Age;
                user.Username = updateUser.Username;
                user.Password = updateUser.Password;
                _userContext.Users.Update(user);
                await _userContext.SaveChangesAsync();
                List<User> userDbList = await _userContext.Users.ToListAsync();

                // do the rest of service response things 
                serviceResponse.Data = userDbList.Select(usr => _mapper.Map<GetUser>(usr)).ToList();
                serviceResponse.Message = "success";
                serviceResponse.Success = true;

            }
            catch (Exception e) 
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Error, " + e.ToString();
                serviceResponse.Success = false;
            }
            // User user = new User();
            // user = _mapper.Map<User>(_userContext.Users.FirstOrDefault(usr => usr.Id == updateUser.Id));
            // try 
            // {
            //     if (user != null) 
            //     {
            //         user.Name = updateUser.Name;
            //         user.SecondName = updateUser.SecondName;
            //         user.Age = updateUser.Age;
            //         user.Username = updateUser.Username;
            //         user.Password = updateUser.Password;
            //         _userContext.Users.Update(user);
            //         await _userContext.SaveChangesAsync();
            //     }
                
                
            // }
            // catch(Exception ex) {
            //     var message = ex; // used instead serviceresponse
            // }
            // return user != null ? user : new User();
            return serviceResponse;
        }
    }
}