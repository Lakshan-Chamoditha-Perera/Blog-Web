using Microsoft.EntityFrameworkCore;
using BlogApp.Entity;

namespace BlogApp.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }

    public DbSet<Blog> Blogs { get; set; }
}