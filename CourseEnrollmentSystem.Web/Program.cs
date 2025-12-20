using CourseEnrollmentSystem.Application.Interfaces;
using CourseEnrollmentSystem.Application.Services;
using CourseEnrollmentSystem.Domain.Entities;
using CourseEnrollmentSystem.Infrastructure.Data;
using CourseEnrollmentSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("CourseEnrollmentDb"));

// Register Repositories
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

// Register Services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Seed Students
    context.Students.AddRange(
        new Student
        {
            FullName = "islam",
            Email = "islam@gmail.com",
            BirthDate = new DateTime(2000, 5, 10),
            NationalId = "30202050202751",
            PhoneNumber = "01204877915"
        },
        new Student
        {
            FullName = "Mohamed",
            Email = "Mohamed@gmail.com",
            BirthDate = new DateTime(1999, 8, 20),
            NationalId = "30202050202752",
            PhoneNumber = "01204877915"
        }
    );

    // Seed Courses
    context.Courses.AddRange(
        new Course
        {
            Title = "C# Fundamentals",
            Description = "Learn the basics of C# programming, syntax, and object-oriented principles.",
            MaxCapacity = 20
        },
        new Course
        {
            Title = "ASP.NET Core MVC",
            Description = "Build modern web applications using ASP.NET Core MVC and Razor views.",
            MaxCapacity = 25
        },
        new Course
        {
            Title = "Entity Framework Core",
            Description = "Work with databases using EF Core, LINQ, and migrations.",
            MaxCapacity = 18
        },
        new Course
        {
            Title = "SQL Fundamentals",
            Description = "Understand relational databases, SQL queries, joins, and constraints.",
            MaxCapacity = 5
        },
        new Course
        {
            Title = "Advanced SQL",
            Description = "Deep dive into stored procedures, indexes, performance tuning, and views.",
            MaxCapacity = 15
        },
        new Course
        {
            Title = "HTML & CSS",
            Description = "Create responsive web pages using modern HTML5 and CSS3 techniques.",
            MaxCapacity = 35
        },
        new Course
        {
            Title = "JavaScript Basics",
            Description = "Learn JavaScript fundamentals including variables, functions, and DOM manipulation.",
            MaxCapacity = 30
        },
        new Course
        {
            Title = "RESTful APIs",
            Description = "Design and consume REST APIs with proper HTTP methods and status codes.",
            MaxCapacity = 20
        },
        new Course
        {
            Title = "Git & GitHub",
            Description = "Version control fundamentals using Git and collaboration with GitHub.",
            MaxCapacity = 40
        }
    );

    context.SaveChanges();
} 


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
