using System.Net;
using BlogApp.Entities;
using BlogApp.Payloads;
using BlogApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

[ApiController]
[Route("[controller]")]
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
    public async Task<IActionResult> SaveBlog()
    {
        try
        {
            var form = await Request.ReadFormAsync();
            if (form == null) return BadRequest(new { success = false, message = "Form data is null" });

            // Get form data
            var title = form["title"].FirstOrDefault();
            var content = form["content"].FirstOrDefault();
            var category = form["category"].FirstOrDefault();
            var userId = form["userId"].FirstOrDefault();

            // Get image from form
            var imageFile = form.Files.GetFile("image");
            byte[] imageBytes = null;

            if (imageFile != null && imageFile.Length > 0)
            {
                Console.WriteLine("Image file name: " + imageFile.FileName);
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    imageBytes = memoryStream.ToArray();
                }
            }


            // Create Blog entity
            var blog = new Blog
            {
                Title = title,
                Content = content,
                Category = Enum.Parse<Category>(category, true),
                Image = imageBytes,
                UserId = Guid.Parse(userId),
                PublishedDate = DateTime.UtcNow
            };

            // Save blog to database
            var createdBlog = _blogService.CreateBlog(blog);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while saving the blog");
            return StatusCode((int)HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
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
}