using BlogApp.Data;
using BlogApp.Entities;

namespace BlogApp.Service.Impl;

public class BlogServiceImpl : IBlogService
{
    private readonly DatabaseContext _context;
    private readonly ILogger<BlogServiceImpl> _logger;
    private readonly IUserService _userService;

    public BlogServiceImpl(ILogger<BlogServiceImpl> logger,
        DatabaseContext context,
        IUserService userService
    )
    {
        _logger = logger;
        _context = context;
        _userService = userService;
    }

    /**
     * Create blog
     */
    public Blog CreateBlog(Blog blog)
    {
        _logger.LogInformation("Method CreateBlog called with blog: {Blog}", blog);
        try
        {
            blog.Id = Guid.NewGuid();
            blog.PublishedDate = DateTime.UtcNow;

            var user = _userService.GetUserById(blog.UserId);
            if (user == null) throw new Exception($"User with ID {blog.UserId} not found.");
            blog.User = user;

            _context.Blogs.AddAsync(blog);
            _context.SaveChangesAsync();

            _logger.LogInformation("Blog with ID: {Id} created successfully.", blog.Id);
            return blog;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the blog with ID: {Id}", blog.Id);
            throw;
        }
    }

    /**
     * Get blog by id
     */
    public Blog GetBlogById(Guid id)
    {
        _logger.LogInformation("Fetching blog with ID: {Id}", id);
        try
        {
            var blog = _context.Blogs.Find(id);
            if (blog != null)
            {
                _logger.LogInformation("Blog with ID: {Id} found.", id);
                return blog;
            }

            _logger.LogWarning("Blog with ID: {Id} not found.", id);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching the blog with ID: {Id}", id);
            throw;
        }
    }

    /**
     * Get all blogs
     */
    public IEnumerable<Blog> GetAll()
    {
        _logger.LogInformation("Method GetAllBlogs");
        try
        {
            return _context.Blogs.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching all blogs.");
            throw;
        }
    }

    /**
     * Delete blog by id
     */
    public bool DeleteBlogById(Guid id)
    {
        _logger.LogInformation("Method DeleteBlogById with ID: {Id}", id);
        try
        {
            if (GetBlogById(id) == null)
            {
                _logger.LogWarning("Blog with ID: {Id} not found. Cannot delete.", id);
                return false;
            }

            _context.Blogs.Remove(GetBlogById(id));
            _context.SaveChanges();

            _logger.LogInformation("Blog with ID: {Id} deleted successfully.", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the blog with ID: {Id}", id);
            throw;
        }
    }

    /**
     * Update blog by id
     */
    public Blog UpdateBlogById(Blog blog)
    {
        _logger.LogInformation("Method UpdateBlogById with ID: {Id}", blog.Id);
        try
        {
            var existingBlog = _context.Blogs.Find(blog.Id);
            if (existingBlog == null) throw new Exception("Blog with ID: " + blog.Id + " not found.");

            existingBlog.Title = blog.Title;
            existingBlog.Content = blog.Content;
            existingBlog.PublishedDate = DateTime.Now;

            _context.SaveChanges();

            _logger.LogInformation("Blog with ID: {Id} updated successfully.", blog.Id);
            return existingBlog;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the blog with ID: {Id}", blog.Id);
            throw;
        }
    }
}