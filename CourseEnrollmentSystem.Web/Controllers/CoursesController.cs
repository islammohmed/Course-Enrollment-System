using CourseEnrollmentSystem.Application.Interfaces;
using CourseEnrollmentSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseEnrollmentSystem.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 5;

            var courses = _courseService.GetAll().ToList();

            var pagedCourses = courses
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(courses.Count / (double)pageSize);

            return View(pagedCourses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            if (!ModelState.IsValid)
                return View(course);

            _courseService.Add(course);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var course = _courseService.GetById(id);
            if (course == null) return NotFound();

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course course)
        {
            if (!ModelState.IsValid)
                return View(course);

            _courseService.Update(course);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var course = _courseService.GetById(id);
            if (course == null) return NotFound();

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _courseService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
