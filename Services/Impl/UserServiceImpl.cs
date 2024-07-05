using BlogApp.Data;
using BlogApp.Entities;

namespace BlogApp.Service.Impl;

public class UserServiceImpl : IUserService

{
    private readonly DatabaseContext _context;
    private readonly ILogger<UserServiceImpl> _logger;

    public UserServiceImpl(ILogger<UserServiceImpl> logger, DatabaseContext context)
    {
        _logger = logger;
        _context = context;
    }

    public User GetUserByEmail(string email)
    {
        _logger.LogInformation("Getting user by email: {Email}", email);
        try
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred while getting user by email: {ex.Message}");
            throw;
        }
    }

    public User CreateUser(User user)
    {
        _logger.LogInformation("Creating user: {User}", user);
        try
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred while creating user: {ex.Message}");
            throw;
        }
    }
}