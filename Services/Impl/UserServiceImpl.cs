using BlogApp.Data;
using BlogApp.Entities;
using BlogApp.Exceptions;
using BlogApp.Payloads.Requests;

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
            var users = _context.Users.Where(u => u.Email == email).ToList();
            if (!users.Any())
            {
                _logger.LogWarning("No users found with email: {Email}", email);
                return null;
            }

            var user = users.FirstOrDefault();
            _logger.LogInformation("User found: {User}", user);

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
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                _logger.LogWarning("User already exists with email: {Email}", user.Email);
                throw new Exception($"User already exists with email: {user.Email}");
            }
            user.Id = Guid.NewGuid();

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

    public IEnumerable<User> GetAll()
    {
        _logger.LogInformation("Getting all users");
        try
        {
            var users = _context.Users.ToList();
            return users;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred while getting all users: {ex.Message}");
            throw;
        }
    }

    public User Login(UserLoginRequest user)
    {
        _logger.LogInformation("SERVICE : Method Login with email: {Email}", user.email);
        try
        {
            var user1 = GetUserByEmail(user.email);
            if (user1 == null)
            {
                _logger.LogWarning("No user found with email: {Email}", user.email);
                throw new UserNotFoundException($"No user found with email: {user.email}");
            }

            if (user.password == user1.Password)
            {
                _logger.LogInformation("User authenticated successfully: {Email}", user.email);
                return user1;
            }

            _logger.LogWarning("Authentication failed for user: {Email}", user.email);
            throw new AuthenticationException("Invalid email or password");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while logging in user: {Email}", user.email);
            throw ex;
        }
    }

    public User GetUserById(Guid id)
    {

        _logger.LogInformation("Getting user by ID: {Id}", id);
        try
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                _logger.LogWarning("No user found with ID: {Id}", id);
                throw new UserNotFoundException($"No user found with ID: {id}");
            }
            _logger.LogInformation("User found: {User}", user);
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred while getting user by ID: {ex.Message}");
            throw;
        }
    }
}