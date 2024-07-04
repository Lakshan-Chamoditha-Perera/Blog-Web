namespace BlogApp.Entity;

public class User
{
    private Guid Id { get; set; }
    private string Name { get; set; }
    private string Email { get; set; }
    private string Password { get; set; }

    public User(string name, string email, string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
    }

    public override string ToString()
    {
        return $"User | Id: {Id}, Name: {Name}, Email: {Email}, Password: {Password}";
    }
}