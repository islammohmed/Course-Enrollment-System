using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseEnrollmentSystem.Domain.Entities;

namespace CourseEnrollmentSystem.Application.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAll();
        Course GetById(int id);
        void Add(Course course);
        void Update(Course course);
        void Delete(int id);
    }
}
