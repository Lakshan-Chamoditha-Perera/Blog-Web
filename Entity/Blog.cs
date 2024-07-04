using System;

namespace BlogApp.Entity;

public class Blog
{
    public Blog(Guid id, string title, string content, DateTime publishedDate)
    {
        Id = id;
        Content = content;
        Title = title;
        PublishedDate = publishedDate;
    }

    private Guid Id { get; set; }
    private string Title { get; set; } = "Default Title";
    private string Content { get; set; } = "Default Content";
    private DateTime PublishedDate { get; set; } = DateTime.Now;

    public override string ToString()
    {
        return $"Blog | Id: {Id}, Title: {Title}, Content: {Content}, PublishedDate: {PublishedDate}";
    }
}