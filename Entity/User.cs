using System.ComponentModel.DataAnnotations;
// Added for ICollection<T>

// Added for attributes

namespace BlogApp.Entities;

public class User
{
    public User()
    {
        Id = Guid.NewGuid();
        Blogs = new List<Blog>(); // Initialize the collection to avoid null reference
    }

    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)] // Example max length, adjust as needed
    public string Name { get; set; }

    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string Password { get; set; }

    public ICollection<Blog> Blogs { get; set; }

    public override string ToString()
    {
        return $"User | Id: {Id}, Name: {Name}, Email: {Email}, Password: {Password}";
    }
}