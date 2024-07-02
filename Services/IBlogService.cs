using BlogApp.Entity;

namespace BlogApp.Service;

public interface IBlogService
{
    Blog CreateBlog(Blog blog);
    Blog GetBlogById(Guid id);
    IEnumerable<Blog> GetAll();
    Boolean DeleteBlogById(Guid id);
    Blog UpdateBlogById(Blog blog);
}