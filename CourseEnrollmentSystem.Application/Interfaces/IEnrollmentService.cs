using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseEnrollmentSystem.Domain.Entities;

namespace CourseEnrollmentSystem.Application.Interfaces
{
    public interface IEnrollmentService
    {
        void EnrollStudent(int studentId, int courseId);
        IEnumerable<Enrollment> GetAll();
    }
}
