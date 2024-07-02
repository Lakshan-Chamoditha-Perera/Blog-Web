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

    public Guid Id { get; set; }
    public string Title { get; set; } = "Default Title";
    public string Content { get; set; } = "Default Content";
    public DateTime PublishedDate { get; set; } = DateTime.Now;

    public override string ToString()
    {
        return $"Id: {Id}, Title: {Title}, Content: {Content}, PublishedDate: {PublishedDate}";
    }
}