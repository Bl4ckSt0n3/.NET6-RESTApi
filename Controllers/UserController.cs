using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnet_test.Models;

namespace dotnet_test.Controllers;

/************** CRUD API CONTROLLER ***************/

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{

    private static readonly string[] Names = new[]
    {
        "Sam", "Jhon", "Alp", "Dany", "Enes", "Tonny", "Jack"
    };

    private static readonly string[] secondNames = new[]
    {
        "Do", "Alias", "Gergin", "Woo", "Bedevi", "Wrackler", "Taylor"
    };

    /************************* OR **************************/
    private static List<User> userList = new List<User>();
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;

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
                    Username = "NewUser",
                    CreateDate = DateTime.Now,
                }
            );
        }
    }

    [HttpGet("all")]
    public ActionResult<IEnumerable<User>> GetAll() 
    {
        // creating a random list and return it.
        // return Enumerable.Range(1, 9).Select(index => new User
        // {
        //     Id = index,
        //     Name = Names[Random.Shared.Next(Names.Length)],
        //     SecondName = secondNames[Random.Shared.Next(Names.Length)],
        //     Age = Random.Shared.Next(18, 30),
        //     Username = "NewUser",
        //     CreateDate = DateTime.Now.AddDays(index),
        // }).ToArray();
        return userList;

    }

    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
        
        var usrObj = userList.FirstOrDefault(usr => usr.Id == id);
        // Console.WriteLine(usrObj);
        if (usrObj != null) 
        {
            return Ok(usrObj);
        }
        return BadRequest();
    }

    [HttpDelete("delete")]
    public ActionResult<User> Remove (int id)
    {
        var usrLst = userList.Where<User>(usr => usr.Id != id);
        return Ok(usrLst);
    }

    [HttpPost("create")]
    public ActionResult<User> Create (User newUser) 
    {
        userList.Add(newUser);
        return Ok(userList);
    }

    [HttpPut("update")]
    public ActionResult<User> Update (User uptdUser)
    {
        var userObject = userList.FirstOrDefault(usr => usr.Id == uptdUser.Id);
        if (userObject != null) 
        {
            userObject.Name = uptdUser.Name;
            userObject.SecondName = uptdUser.SecondName;
            userObject.Age = uptdUser.Age;
            userObject.Username = uptdUser.Username;
            return Ok(userList);
        }
        return BadRequest();
    }
}
