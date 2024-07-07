using BlogApp.Entities;
using BlogApp.Payloads.Requests;

namespace BlogApp.Service;

public interface IUserService
{
    User GetUserByEmail(string email);
    User CreateUser(User user);
    IEnumerable<User> GetAll();
    User Login(UserLoginRequest user);
}