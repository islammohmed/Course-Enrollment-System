using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseEnrollmentSystem.Application.Interfaces;
using CourseEnrollmentSystem.Domain.Entities;
using CourseEnrollmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseEnrollmentSystem.Application.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Enrollment> GetAll()
        {
            return _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToList();
        }

        public void EnrollStudent(int studentId, int courseId)
        {
            // Duplicate enrollment check
            bool alreadyEnrolled = _context.Enrollments
                .Any(e => e.StudentId == studentId && e.CourseId == courseId);

            if (alreadyEnrolled)
                throw new Exception("Student already enrolled in this course");

            var course = _context.Courses.Find(courseId);
            if (course == null)
                throw new Exception("Course not found");

            int currentCount = _context.Enrollments
                .Count(e => e.CourseId == courseId);

            if (currentCount >= course.MaxCapacity)
                throw new Exception("Course is full");

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId
            };

            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();
        }

        public int GetAvailableSlots(int courseId)
        {
            var course = _context.Courses.Find(courseId);
            if (course == null) return 0;

            int enrolledCount = _context.Enrollments
                .Count(e => e.CourseId == courseId);

            return course.MaxCapacity - enrolledCount;
        }
    }
}
