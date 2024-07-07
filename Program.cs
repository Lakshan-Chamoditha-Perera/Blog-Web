using BlogApp.Data;
using BlogApp.Service;
using BlogApp.Service.Impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36))));

builder.Services.AddScoped<IBlogService, BlogServiceImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddControllers();

var app = builder.Build();


app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseRouting(); // mapping routes to controllers
app.MapControllers(); // map controllers to routes
app.Run(); // run the app