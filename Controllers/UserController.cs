using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

[ApiController]
public class UserController
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }
}