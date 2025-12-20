using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseEnrollmentSystem.Application.Interfaces;
using CourseEnrollmentSystem.Domain.Entities;
using CourseEnrollmentSystem.Infrastructure.Data;

namespace CourseEnrollmentSystem.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public Student? GetById(int id)
        {
            return _context.Students.Find(id);
        }

        public void Add(Student entity)
        {
            _context.Students.Add(entity);
        }

        public void Update(Student entity)
        {
            _context.Students.Update(entity);
        }

        public void Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool EmailExists(string email)
        {
            return _context.Students.Any(s => s.Email == email);
        }

        public Student? GetByEmail(string email)
        {
            return _context.Students.FirstOrDefault(s => s.Email == email);
        }
    }
}
