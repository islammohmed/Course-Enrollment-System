using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseEnrollmentSystem.Application.Interfaces;
using CourseEnrollmentSystem.Domain.Entities;

namespace CourseEnrollmentSystem.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public IEnumerable<Student> GetAll()
        {
            return _studentRepository.GetAll();
        }

        public Student? GetById(int id)
        {
            return _studentRepository.GetById(id);
        }

        public bool Add(Student student, out string errorMessage)
        {
            errorMessage = string.Empty;
            
            if (_studentRepository.EmailExists(student.Email))
            {
                errorMessage = "A student with this email already exists. Please use a different email address.";
                return false;
            }

            _studentRepository.Add(student);
            _studentRepository.SaveChanges();
            return true;
        }

        public bool Update(Student student, out string errorMessage)
        {
            errorMessage = string.Empty;
            
            var existingStudent = _studentRepository.GetByEmail(student.Email);
            if (existingStudent != null && existingStudent.Id != student.Id)
            {
                errorMessage = "A student with this email already exists. Please use a different email address.";
                return false;
            }

            _studentRepository.Update(student);
            _studentRepository.SaveChanges();
            return true;
        }

        public void Delete(int id)
        {
            _studentRepository.Delete(id);
            _studentRepository.SaveChanges();
        }
    }
}
