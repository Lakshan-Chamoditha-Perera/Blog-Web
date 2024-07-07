using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// Added for attributes

namespace BlogApp.Entities;

public class Blog
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)] // Example max length, adjust as needed
    public string Title { get; set; }

    [Required] public string Content { get; set; }

    [Required] public DateTime PublishedDate { get; set; }

    [Required] public Guid UserId { get; set; } // Foreign key for User
    public byte[] Image { get; set; }

    [ForeignKey("UserId")] // Specify the foreign key name
    public User User { get; set; }

    public override string ToString()
    {
        return $"Blog | Id: {Id}, Title: {Title}, Content: {Content}, PublishedDate: {PublishedDate}";
    }
}