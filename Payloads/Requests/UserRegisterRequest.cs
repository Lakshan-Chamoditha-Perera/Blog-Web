using BlogApp.Entities;

namespace BlogApp.Payloads.Requests;

public class UserRegisterRequest
{
    public UserRegisterRequest(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public UserRegisterRequest()
    {
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public override string ToString()
    {
        return $"UserRegisterRequest | Name: {Name}, Email: {Email}, Password: {Password}";
    }

    public User ToUser()
    {
        return new User
        {
            Name = Name,
            Email = Email,
            Password = Password
        };
    }
}