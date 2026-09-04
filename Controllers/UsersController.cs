using Microsoft.AspNetCore.Mvc;
using JesonApi.Models;

namespace JesonApi.Controllers;



[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
private readonly List<UserResource>  users = new List<UserResource>
        {
            new UserResource { Id = 1, Name = "Jan", Email = "jan@seznam.cz" },
            new UserResource { Id = 2, Name = "Petr", Email = "" }
        };

    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(users);
    }


    [HttpGet("{id}")]
    public IActionResult GetUserDetail(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null) {
            return NotFound();
        }
        return Ok(user);
    }
}
