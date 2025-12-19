using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseEnrollmentSystem.Infrastructure.Data;
using CourseEnrollmentSystem.Application.Interfaces;
using CourseEnrollmentSystem.Domain.Entities;



namespace CourseEnrollmentSystem.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public Student GetById(int id)
        {
            return _context.Students.Find(id);
        }

        public void Add(Student student)
        {
            if (_context.Students.Any(s => s.Email == student.Email))
                throw new Exception("Email already exists");

            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return;

            _context.Students.Remove(student);
            _context.SaveChanges();
        }
    }
}
