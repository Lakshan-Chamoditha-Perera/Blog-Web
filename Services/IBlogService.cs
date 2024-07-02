using BlogApp.Entity;

namespace BlogApp.Service;

public interface IBlogService
{
    void CreateBlog(Blog blog);
    Blog GetBlogById(Guid id);
    IEnumerable<Blog> GetAll();
    void DeleteBlogById(Guid id);
    void UpdateBlogById(Blog blog);
}