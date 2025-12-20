using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseEnrollmentSystem.Domain.Entities;

namespace CourseEnrollmentSystem.Application.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        bool EmailExists(string email);
        Student? GetByEmail(string email);
    }
}
