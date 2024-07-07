using BlogApp.Entities;
using BlogApp.Exceptions;
using BlogApp.Payloads;
using BlogApp.Payloads.Requests;
using BlogApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpPost("/users/login")]
    public IActionResult Login([FromBody] UserLoginRequest user)
    {
        _logger.LogInformation("API : Login attempt with email: {Email}", user.email);

        try
        {
            var authenticatedUser = _userService.Login(user);
            return Ok(new StandardResponse<User>(true, "User authenticated successfully", authenticatedUser));
        }
        catch (UserNotFoundException ex)
        {
            _logger.LogWarning(ex.Message);
            return NotFound(new StandardResponse<User>(false, ex.Message, null));
        }
        catch (AuthenticationException ex)
        {
            _logger.LogWarning(ex.Message);
            return Unauthorized(new StandardResponse<User>(false, ex.Message, null));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during login for email {Email}", user.email);
            return StatusCode(500, new StandardResponse<User>(false, "An unexpected error occurred", null));
        }
    }
}