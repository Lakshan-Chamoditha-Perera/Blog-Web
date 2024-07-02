using BlogApp.Entity;
using System.Collections.Concurrent;
using BlogApp.Data;

namespace BlogApp.Service.Impl;

public class BlogServiceImpl : IBlogService
{
    private readonly DatabaseContext _context;
    private readonly ILogger<BlogServiceImpl> _logger;

    public BlogServiceImpl(ILogger<BlogServiceImpl> logger,
        DatabaseContext context)
    {
        _logger = logger;
        _context = context;
    }

    /**
     * Create blog
     */
    public void CreateBlog(Blog blog)
    {
        _logger.LogInformation("Method CreateBlog");
        try
        {
            blog.Id = Guid.NewGuid();
            blog.PublishedDate = DateTime.Now;

            _context.Blogs.Add(blog);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the blog with ID: {Id}", blog.Id);
            throw;
        }
    }

    /**
     *  Get blog by id
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
    public void DeleteBlogById(Guid id)
    {
        _logger.LogInformation("Method DeleteBlogById with ID: {Id}", id);
        try
        {
            if (GetBlogById(id) == null)
            {
                _logger.LogWarning("Blog with ID: {Id} not found. Cannot delete.", id);
                return;
            }

            _context.Blogs.Remove(GetBlogById(id));
            _context.SaveChanges();
            _logger.LogInformation("Blog with ID: {Id} deleted successfully.", id);
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
    public void UpdateBlogById(Blog blog)
    {
        _logger.LogInformation("Method UpdateBlogById with ID: {Id}", blog.Id);
        try
        {
            if (GetBlogById(blog.Id) == null)
            {
                _logger.LogWarning("Blog with ID: {Id} not found. Cannot update.", blog.Id);
                return;
            }

            _context.Blogs.Update(blog);
            _context.SaveChanges();
            _logger.LogInformation("Blog with ID: {Id} updated successfully.", blog.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the blog with ID: {Id}", blog.Id);
            throw;
        }
    }
}