using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseEnrollmentSystem.Application.Interfaces;
using CourseEnrollmentSystem.Domain.Entities;

namespace CourseEnrollmentSystem.Application.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICourseRepository _courseRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository, ICourseRepository courseRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _courseRepository = courseRepository;
        }

        public IEnumerable<Enrollment> GetAll()
        {
            return _enrollmentRepository.GetAll();
        }

        public void EnrollStudent(int studentId, int courseId)
        {
            // Duplicate enrollment check
            if (_enrollmentRepository.IsStudentEnrolledInCourse(studentId, courseId))
            {
                throw new Exception("Student already enrolled in this course");
            }

            var course = _courseRepository.GetById(courseId);
            if (course == null)
                throw new Exception("Course not found");

            int currentCount = _enrollmentRepository.GetByCourseId(courseId).Count();

            if (currentCount >= course.MaxCapacity)
                throw new Exception("Course is full");

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId
            };

            _enrollmentRepository.Add(enrollment);
            _enrollmentRepository.SaveChanges();
        }

        public int GetAvailableSlots(int courseId)
        {
            var course = _courseRepository.GetById(courseId);
            if (course == null) return 0;

            int enrolledCount = _enrollmentRepository.GetByCourseId(courseId).Count();

            return course.MaxCapacity - enrolledCount;
        }
    }
}
