using Microsoft.AspNetCore.Mvc;

namespace CourseEnrollmentSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
