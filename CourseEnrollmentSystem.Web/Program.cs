using CourseEnrollmentSystem.Domain.Entities;
using CourseEnrollmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("CourseEnrollmentDb"));


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
            Title = "c#",
            Description = "Intro to c#",
            MaxCapacity = 10
        },
        new Course
        {
            Title = "SQL",
            Description = "Intro to SQL",
            MaxCapacity = 15
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
