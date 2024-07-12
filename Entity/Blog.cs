using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using BlogApp.Controllers;

namespace BlogApp.Entities;

public class Blog
{
    [Key] public Guid Id { get; set; }

    [Required] [MaxLength(100)] public string Title { get; set; }

    [Required] public string Content { get; set; }
    [Required] public DateTime PublishedDate { get; set; }
    [Required] public Guid UserId { get; set; }

    [ForeignKey("UserId")] public User User { get; set; }
    public  string Category { get; set; }
    public byte[] Image { get; set; }

    public override string ToString()
    {
        return $"Blog | Id: {Id}, Title: {Title}, Content: {Content}, PublishedDate: {PublishedDate}";
    }
}