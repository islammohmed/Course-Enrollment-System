using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseEnrollmentSystem.Domain.Entities;

namespace CourseEnrollmentSystem.Application.Interfaces
{
    public interface ICourseRepository : IRepository<Course>
    {
    }
}
