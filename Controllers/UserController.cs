using BlogApp.Entities;
using BlogApp.Payloads;
using BlogApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(
        ILogger<UserController> logger,
        IUserService userService
    )
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost("/users")]
    public IActionResult CreateUser([FromBody] User user)
    {
        _logger.LogInformation("API : Method CreateBlog {}:", user);
        if (user == null) return BadRequest("User object cannot be null");
        var createdUser = _userService.CreateUser(user);
        return Ok(new StandardResponse<User>(true, "User created successfully", createdUser));
    }

    [HttpGet("/users/{email}")]
    public IActionResult GetUserById(string email)
    {
        _logger.LogInformation("API : Method GetUserById {}:", email);
        var user = _userService.GetUserByEmail(email);
        return Ok(new StandardResponse<User>(true, "User fetched successfully", user));
    }
}