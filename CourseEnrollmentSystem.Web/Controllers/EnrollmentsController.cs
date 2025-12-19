using CourseEnrollmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseEnrollmentSystem.Web.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;

        public EnrollmentsController(
            IEnrollmentService enrollmentService,
            IStudentService studentService,
            ICourseService courseService)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            var enrollments = _enrollmentService.GetAll();
            return View(enrollments);
        }

        public IActionResult Create()
        {
            ViewBag.Students = new SelectList(_studentService.GetAll(), "Id", "FullName");
            ViewBag.Courses = new SelectList(_courseService.GetAll(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int studentId, int courseId)
        {
            try
            {
                _enrollmentService.EnrollStudent(studentId, courseId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                ViewBag.Students = new SelectList(_studentService.GetAll(), "Id", "FullName");
                ViewBag.Courses = new SelectList(_courseService.GetAll(), "Id", "Title");

                return View();
            }
        }

        [HttpGet]
        public IActionResult GetAvailableSlots(int courseId)
        {
            var slots = _enrollmentService.GetAvailableSlots(courseId);
            return Json(slots);
        }

    }
}
