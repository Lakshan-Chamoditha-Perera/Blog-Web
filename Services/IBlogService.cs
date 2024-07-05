using BlogApp.Entities;

namespace BlogApp.Service;

public interface IBlogService
{
    Blog CreateBlog(Blog blog);
    Blog GetBlogById(Guid id);
    IEnumerable<Blog> GetAll();
    bool DeleteBlogById(Guid id);
    Blog UpdateBlogById(Blog blog);
}