using BlogApp.Dtos;
using BlogApp.Entities;
using BlogApp.Payloads;
using BlogApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

[ApiController]
public class BlogController : ControllerBase
{
    private readonly IBlogService _blogService;
    private readonly ILogger<BlogController> _logger;

    public BlogController(
        ILogger<BlogController> logger,
        IBlogService blogService)
    {
        _logger = logger;
        _blogService = blogService;
    }

    [HttpPost("/blogs")]
    public async Task<IActionResult> SaveBlog([FromForm] CreateBlogRequest createBlogRequest)
    {
        _logger.LogInformation("SaveBlog {}:", createBlogRequest);
        try
        {
            var blog = new Blog
            {
                Title = createBlogRequest.Title,
                PublishedDate = DateTime.Now,
                Category = createBlogRequest.Category,
                Content = createBlogRequest.Content,
                UserId = createBlogRequest.UserId
            };

            if (createBlogRequest.Image != null && createBlogRequest.Image.Length > 0)
            {
                _logger.LogInformation("Image exists.");
                using var memoryStream = new MemoryStream();
                await createBlogRequest.Image.CopyToAsync(memoryStream);
                blog.Image = memoryStream.ToArray();
                _logger.LogInformation("Image size: " + blog.Image.Length);
            }

            var createdBlog = _blogService.CreateBlog(blog);
            return Ok(new { success = true, message = "Blog created successfully", data = createdBlog });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the blog");
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred while creating the blog",
                details = ex.Message
            });
        }
    }


    [HttpGet("/blogs/{id:guid}")]
    public IActionResult GetBlogById(Guid id)
    {
        _logger.LogInformation("GetBlogById {}:", id);

        var blog = _blogService.GetBlogById(id);
        return Ok(new StandardResponse<Blog>(true, "Blog fetched successfully", blog));
    }

    [HttpGet("/blogs")]
    public IActionResult GetAll(Guid id)
    {
        _logger.LogInformation("GetAllBlogs {}:");

        var blogs = _blogService.GetAll();
        return Ok(new StandardResponse<IEnumerable<Blog>>(true, "All blogs fetched successfully", blogs));
    }

    [HttpDelete("/blogs/{id:guid}")]
    public IActionResult DeleteBlogById(Guid id)
    {
        _logger.LogInformation("DeleteBlogById {}:", id);

        var isDeleted = _blogService.DeleteBlogById(id);
        return Ok(new StandardResponse<bool>(true, isDeleted ? "Blog deleted successfully" : "Blog not found",
            isDeleted));
    }

    [HttpPatch("/blogs/{id:guid}")]
    public IActionResult UpdateBlogById([FromBody] Blog blog)
    {
        _logger.LogInformation("UpdateBlogById {}:", blog);

        var updatedBlog = _blogService.UpdateBlogById(blog);
        return Ok(new StandardResponse<Blog>(true, "Blog updated successfully", updatedBlog));
    }

    [HttpGet("/abc")]
    public IActionResult get()
    {
        _logger.LogInformation("get()-----");
        return Ok();
    }
}