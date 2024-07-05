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
        ILogger<BlogController> logger, IBlogService blogService)
    {
        _logger = logger;
        _blogService = blogService;
    }

    [HttpPost("/blogs")]
    public IActionResult CreateBlog([FromBody] Blog blog)
    {
        _logger.LogInformation("API : Method CreateBlog {}:", blog);
        if (blog == null) return BadRequest("Blog object cannot be null");
        var createdBlog = _blogService.CreateBlog(blog);
        return Ok(new StandardResponse<Blog>(true, "Blog created successfully", createdBlog));
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
}