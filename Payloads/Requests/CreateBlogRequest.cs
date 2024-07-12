using BlogApp.Controllers;

namespace BlogApp.Dtos;

public record CreateBlogRequest
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Category { get; set; }
    public IFormFile Image { get; set; }
    public Guid UserId { get; set; }

    public CreateBlogRequest()
    {
    }

    public CreateBlogRequest(string title, string content, string category, IFormFile image, Guid userId)
    {
        Title = title;
        Content = content;
        Category = category;
        Image = image;
        UserId = userId;
    }
}