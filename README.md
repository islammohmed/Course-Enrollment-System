# Course Enrollment System



A comprehensive ASP.NET Core MVC application that manages students, courses, and enrollments using clean N-Tier architecture, Entity Framework Core (Code-First), and an in-memory database.

---

## Table of Contents

1. [Project Overview](#project-overview)
2. [Features Implemented](#features-implemented)
3. [Technical Architecture](#technical-architecture)
4. [Technologies Used](#technologies-used)
5. [Project Structure](#project-structure)
6. [Setup Instructions](#setup-instructions)
7. [Usage Guide](#usage-guide)
8. [Database Schema](#database-schema)
9. [Important Notes](#important-notes)

---

## Project Overview

This Course Enrollment System is built to demonstrate best practices in ASP.NET Core MVC development with a focus on:
- Clean N-Tier Architecture
- Separation of concerns with services handling all business logic
- Entity Framework Core with Code-First approach
- In-Memory database for easy testing and demonstration
- Responsive UI using Bootstrap
- Dynamic content loading with jQuery AJAX

---

## Features Implemented

### 1. Student Management
Complete CRUD operations for student management:
- **Add Student**: Create new students with full validation
- **Edit Student**: Update existing student information
- **Delete Student**: Remove students with confirmation
- **List Students**: View all students in a responsive table

**Student Properties:**
- Full Name (required)
- Email (unique, required)
- Birth Date (required)
- National ID (required, max length 14)
- Phone Number (optional, max length 11)

### 2. Course Management
Complete CRUD operations for course management:
- **Add Course**: Create new courses with title, description, and capacity
- **Edit Course**: Update course information
- **Delete Course**: Remove courses with confirmation dialog
- **List Courses**: View all courses with pagination (5 per page)

**Course Properties:**
- Title (required)
- Description (optional)
- Maximum Capacity (required)

### 3. Enrollment Management
Intelligent enrollment system with validations:
- **Enroll Students**: Assign students to courses
- **Capacity Validation**: Prevents enrollment when course is full
- **Duplicate Prevention**: Prevents duplicate enrollments
- **Dynamic Slots Display**: Shows available slots in real-time using jQuery AJAX

### 4. JavaScript Features (Bonus)
- **jQuery AJAX Implementation**: Dynamic display of available course slots
- Automatically updates when course selection changes
- Real-time slot availability feedback

### 5. Additional Features (Bonus)
- **Pagination**: Course list displays 5 courses per page with Previous/Next navigation
- **Partial Views**: Reusable `_CourseDetails` partial view for displaying course information
- **Bootstrap Styling**: Modern, responsive UI with Bootstrap 5
- **Form Validation**: Client-side and server-side validation on all forms

---

## Technical Architecture

### N-Tier Architecture Layers

```
CourseEnrollmentSystem (Solution)
│
├── CourseEnrollmentSystem.Domain (Entities Layer)
│   └── Entities/
│       ├── Student.cs
│       ├── Course.cs
│       └── Enrollment.cs
│
├── CourseEnrollmentSystem.Infrastructure (Data Layer)
│   └── Data/
│       └── ApplicationDbContext.cs (EF Core DbContext)
│
├── CourseEnrollmentSystem.Application (Business Layer)
│   ├── Interfaces/
│   │   ├── IStudentService.cs
│   │   ├── ICourseService.cs
│   │   └── IEnrollmentService.cs
│   └── Services/
│       ├── StudentService.cs
│       ├── CourseService.cs
│       └── EnrollmentService.cs
│
└── CourseEnrollmentSystem.Web (Presentation Layer)
    ├── Controllers/
    │   ├── StudentsController.cs
    │   ├── CoursesController.cs
    │   ├── EnrollmentsController.cs
    │   └── HomeController.cs
    ├── Views/
    │   ├── Students/
    │   ├── Courses/
    │   ├── Enrollments/
    │   ├── Home/
    │   └── Shared/
    └── wwwroot/ (Static files)
```

### Layer Responsibilities

- **Domain Layer**: Contains all entity models (Student, Course, Enrollment)
- **Infrastructure Layer**: Database context and Entity Framework Core configuration
- **Application Layer**: Business logic services with interfaces for dependency injection
- **Presentation Layer**: MVC controllers, views, and static assets

### Dependency Injection

All services are registered in `Program.cs` and injected into controllers:
```csharp
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
```

---

## Technologies Used

- **ASP.NET Core 8.0 MVC** - Web framework
- **Entity Framework Core 8.0** - ORM for data access
- **In-Memory Database** - For easy testing and demonstration
- **C# 12** - Programming language
- **Razor Views** - Server-side rendering
- **Bootstrap 5** - Responsive CSS framework
- **jQuery 3.x** - AJAX and DOM manipulation
- **HTML5 & CSS3** - Modern web standards
- **Git** - Version control

---

## Project Structure

### Domain Entities

**Student.cs**
```csharp
public class Student
{
    public int Id { get; set; }
    [Required] public string FullName { get; set; }
    [Required, EmailAddress] public string Email { get; set; }
    [Required] public DateTime BirthDate { get; set; }
    [Required, MaxLength(14)] public string NationalId { get; set; }
    [MaxLength(11)] public string PhoneNumber { get; set; }
}
```

**Course.cs**
```csharp
public class Course
{
    public int Id { get; set; }
    [Required] public string Title { get; set; }
    public string Description { get; set; }
    [Required] public int MaxCapacity { get; set; }
}
```

**Enrollment.cs**
```csharp
public class Enrollment
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public Student Student { get; set; }
    public Course Course { get; set; }
}
```

### Service Interfaces

All business logic is handled through service interfaces:
- `IStudentService`: Student CRUD operations
- `ICourseService`: Course CRUD operations with pagination
- `IEnrollmentService`: Enrollment operations with validation

---

## Setup Instructions

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 (or VS Code with C# extension)
- Git

### Installation Steps

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd CourseEnrollmentSystem
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Build the solution**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   cd CourseEnrollmentSystem.Web
   dotnet run
   ```

5. **Open in browser**
   - Navigate to: `https://localhost:5001` or `http://localhost:5000`
   - The application will automatically seed sample data

### Visual Studio

1. Open `CourseEnrollmentSystem.sln` in Visual Studio
2. Set `CourseEnrollmentSystem.Web` as the startup project
3. Press `F5` to run with debugging or `Ctrl+F5` without debugging

---

## Usage Guide

### Managing Students

1. Navigate to **Students** from the main menu
2. Click **Add New Student** to create a student
3. Fill in all required fields (Full Name, Email, Birth Date, National ID)
4. Phone Number is optional
5. Use **Edit** to modify student details
6. Use **Delete** to remove students (with confirmation)

### Managing Courses

1. Navigate to **Courses** from the main menu
2. Click **Add New Course** to create a course
3. Enter Title (required), Description (optional), and Max Capacity (required)
4. View courses with pagination (5 per page)
5. Use **Edit** to modify course details
6. Use **Delete** to remove courses (with confirmation)

### Enrolling Students

1. Navigate to **Enrollments** from the main menu
2. Click **New Enrollment**
3. Select a student from the dropdown
4. Select a course from the dropdown
5. **Available slots will be displayed dynamically** using AJAX
6. Click **Enroll Student** to complete enrollment
7. System will prevent:
   - Enrolling when course is full
   - Duplicate enrollments for the same student and course

---

## Database Schema

### In-Memory Database

The application uses Entity Framework Core's In-Memory database provider for demonstration purposes.

**Database Configuration:**
```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("CourseEnrollmentDb"));
```

**Sample Data Seeding:**
- 5 Students with complete information
- 8 Courses with varying capacities
- Pre-existing enrollments to demonstrate functionality

### Entity Relationships

- **Student** ↔ **Enrollment**: One-to-Many
- **Course** ↔ **Enrollment**: One-to-Many
- **Enrollment**: Junction table connecting Students and Courses

---

## Important Notes

### Data Persistence

The application uses an **In-Memory Database**. Data will be reset when the application restarts. For production use, configure a persistent database (SQL Server, PostgreSQL, etc.) in `Program.cs`.

### Validation

- **Server-side validation**: All entities use Data Annotations
- **Client-side validation**: jQuery validation on all forms
- **Business rules**: Enforced in service layer (capacity limits, duplicates)

### Best Practices Implemented

1. **Separation of Concerns**: Logic separated into distinct layers
2. **Dependency Injection**: All services injected via interfaces
3. **Repository Pattern**: DbContext acts as repository
4. **Strongly Typed Views**: All views use proper models
5. **Partial Views**: Reusable UI components
6. **Responsive Design**: Bootstrap 5 for mobile-friendly UI
7. **AJAX**: Dynamic content loading without page refresh
8. **Clean Code**: Consistent naming, commenting, and structure

### Security Considerations

- Anti-forgery tokens on all forms
- Email validation and uniqueness
- Input validation on all fields
- SQL injection prevention through EF Core parameterization

---

## Project Compliance with Requirements

### CodeZone LLC Requirements Checklist

#### Student Management
- ✅ Add, edit, delete, and list students
- ✅ Full name (required)
- ✅ Email (unique, required)
- ✅ Birthdate (required)
- ✅ National ID (required, max length 14)
- ✅ Phone number (optional, max length 11)

#### Course Management
- ✅ Add, edit, delete, and list courses
- ✅ Title (required)
- ✅ Description (optional)
- ✅ Maximum capacity (required)

#### Enrollment Management
- ✅ Enroll students in courses
- ✅ Prevent enrollment if course is full
- ✅ Prevent duplicate enrollments

#### JavaScript Feature
- ✅ Display available slots dynamically using jQuery AJAX

#### Architecture
- ✅ N-Tier architecture (Presentation, Business, Data layers)
- ✅ Business logic in services (IEnrollmentService, etc.)
- ✅ Dependency injection for decoupling

#### Database
- ✅ Entity Framework Core with In-Memory Database
- ✅ Sample data seeding

#### UI/UX Features
- ✅ jQuery for dynamic slot display (Bonus)
- ✅ Pagination for Course List (Bonus)
- ✅ Partial Views for course/student details (Bonus)

#### Code Quality
- ✅ Clean code with logic in services
- ✅ Reusable components
- ✅ Multiple commits showing progress
