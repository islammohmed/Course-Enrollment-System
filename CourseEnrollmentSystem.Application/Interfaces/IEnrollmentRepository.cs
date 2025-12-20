using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseEnrollmentSystem.Domain.Entities;

namespace CourseEnrollmentSystem.Application.Interfaces
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        IEnumerable<Enrollment> GetByStudentId(int studentId);
        IEnumerable<Enrollment> GetByCourseId(int courseId);
        bool IsStudentEnrolledInCourse(int studentId, int courseId);
    }
}
