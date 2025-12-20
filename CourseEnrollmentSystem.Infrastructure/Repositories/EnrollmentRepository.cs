using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseEnrollmentSystem.Application.Interfaces;
using CourseEnrollmentSystem.Domain.Entities;
using CourseEnrollmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseEnrollmentSystem.Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentRepository(ApplicationDbContext context)
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

        public Enrollment? GetById(int id)
        {
            return _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefault(e => e.Id == id);
        }

        public void Add(Enrollment entity)
        {
            _context.Enrollments.Add(entity);
        }

        public void Update(Enrollment entity)
        {
            _context.Enrollments.Update(entity);
        }

        public void Delete(int id)
        {
            var enrollment = _context.Enrollments.Find(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Enrollment> GetByStudentId(int studentId)
        {
            return _context.Enrollments
                .Include(e => e.Course)
                .Where(e => e.StudentId == studentId)
                .ToList();
        }

        public IEnumerable<Enrollment> GetByCourseId(int courseId)
        {
            return _context.Enrollments
                .Include(e => e.Student)
                .Where(e => e.CourseId == courseId)
                .ToList();
        }

        public bool IsStudentEnrolledInCourse(int studentId, int courseId)
        {
            return _context.Enrollments.Any(e => e.StudentId == studentId && e.CourseId == courseId);
        }
    }
}
