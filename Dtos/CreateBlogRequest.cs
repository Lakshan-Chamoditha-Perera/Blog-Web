namespace BlogApp.Dtos;

public record CreateBlogRequest
{
    public string Content;
    public DateTime PublishedDate;
    public string Title;
}