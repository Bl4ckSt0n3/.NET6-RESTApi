using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnet_test.Models;
using dotnet_test.Services;
using dotnet_test.Models.Dtos;

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
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        // implementing interfaces
        _logger = logger;
        _userService = userService;

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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll() 
    {

        // calling the method from _userService interface  ---- https://exceptionnotfound.net/dependency-injection-in-dotnet-6-service-lifetimes/
        return Ok(await _userService.GetAll());

    }

    [HttpGet("get/{id}")]
    public ActionResult<User> Get(int id)
    {
        
        // var usrObj = userList.FirstOrDefault(usr => usr.Id == id);
        return Ok(_userService.Get(id));
    }

    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove (int id)
    {
        return _userService.Remove(id) == null ? BadRequest() : Ok(await _userService.Remove(id));
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateUser))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create (CreateUser newUser) // sync Action
    {
        return  _userService.Create(newUser) == null ? BadRequest() : Ok(await _userService.Create(newUser));
    }

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUser))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update (UpdateUser updateUser) // async call
    {
        return _userService.Update(updateUser) == null ? BadRequest() : Ok(await _userService.Update(updateUser));
    }
}
