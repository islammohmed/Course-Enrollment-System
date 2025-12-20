using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseEnrollmentSystem.Application.Interfaces;
using CourseEnrollmentSystem.Domain.Entities;

namespace CourseEnrollmentSystem.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public IEnumerable<Course> GetAll()
        {
            return _courseRepository.GetAll();
        }

        public Course? GetById(int id)
        {
            return _courseRepository.GetById(id);
        }

        public void Add(Course course)
        {
            _courseRepository.Add(course);
            _courseRepository.SaveChanges();
        }

        public void Update(Course course)
        {
            _courseRepository.Update(course);
            _courseRepository.SaveChanges();
        }

        public void Delete(int id)
        {
            _courseRepository.Delete(id);
            _courseRepository.SaveChanges();
        }
    }
}
