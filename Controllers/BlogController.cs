using BlogApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Entity;
using BlogApp.Service;

namespace BlogApp.Controllers;

[ApiController]
public class BlogController : ControllerBase
{
    private readonly ILogger<BlogController> _logger;
    private readonly IBlogService _blogService;

    public BlogController(
        ILogger<BlogController> logger, IBlogService blogService)
    {
        _logger = logger;
        _blogService = blogService;
    }

    [HttpPost("/blogs")]
    public IActionResult CreateBlog([FromBody] Blog blog)
    {
       _logger.LogInformation("CreateBlog {}:", blog);

        _blogService.CreateBlog(blog);
        var response = new BlogResponse
        {
            Id = blog.Id,
            Title = blog.Title,
            Content = blog.Content,
            PublishedDate = blog.PublishedDate
        };

        return CreatedAtAction(nameof(GetBlogById), new { id = blog.Id }, response);
    }


    [HttpGet("/blogs/{id:guid}")]
    public IActionResult GetBlogById(Guid id)
    {
       _logger.LogInformation("GetBlogById {}:", id);

       var blog = _blogService.GetBlogById(id);
       if (blog == null) return NotFound();

       return Ok();
    }

    [HttpGet("/blogs")]
    public IActionResult GetAll(Guid id)
    {
        _logger.LogInformation("GetAllBlogs {}:");

        var blogs = _blogService.GetAll();
        return Ok( blogs);
    }

    [HttpDelete("/blogs/{id:guid}")]
    public IActionResult DeleteBlogById(Guid id)
    {
        _logger.LogInformation("DeleteBlogById {}:", id);

        _blogService.DeleteBlogById(id);
        return Ok();
    }

    [HttpPatch("/blogs/{id:guid}")]
    public IActionResult UpdateBlogById([FromBody] Blog blog)
    {
        _logger.LogInformation("UpdateBlogById {}:", blog);

        _blogService.UpdateBlogById(blog);
        return Ok();
    }
}