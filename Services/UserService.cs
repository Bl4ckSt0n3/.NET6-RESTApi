using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_test.Models;

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

        public UserService() // constructor
        {
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
        public List<User> Create(User newUser)
        {
            userList.Add(newUser);
            return userList;
        }

        public User Get(int id)
        {
            var usrObj = userList.FirstOrDefault(usr => usr.Id == id);
            return usrObj != null ? usrObj : new User(); // return null user
        }

        public List<User> GetAll()
        {
            return userList;
        }

        public List<User> Remove(int id)
        {
            var usrLst =  new List<User> (userList.Where<User>(usr => usr.Id != id));
            return  usrLst;
        }

        public List<User> Update(User updtUser)
        {
            var userObject = userList.FirstOrDefault(usr => usr.Id == updtUser.Id);
            if (userObject != null) 
            {
                userObject.Name = updtUser.Name;
                userObject.SecondName = updtUser.SecondName;
                userObject.Age = updtUser.Age;
                userObject.Username = updtUser.Username;
                
            }
            return userList;
        }
    }
}