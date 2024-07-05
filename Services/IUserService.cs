using BlogApp.Entities;

namespace BlogApp.Service;

public interface IUserService
{
    User GetUserByEmail(string email);
    User CreateUser(User user);

}