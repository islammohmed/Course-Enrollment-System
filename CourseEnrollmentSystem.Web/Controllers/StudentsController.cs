using CourseEnrollmentSystem.Application.Interfaces;
using CourseEnrollmentSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CourseEnrollmentSystem.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            var students = _studentService.GetAll();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
                return View(student);

            _studentService.Add(student);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var student = _studentService.GetById(id);
            if (student == null) return NotFound();

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            if (!ModelState.IsValid)
                return View(student);

            _studentService.Update(student);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var student = _studentService.GetById(id);
            if (student == null) return NotFound();

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _studentService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
