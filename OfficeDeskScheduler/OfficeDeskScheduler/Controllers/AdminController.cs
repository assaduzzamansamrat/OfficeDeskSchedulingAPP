using Microsoft.AspNetCore.Mvc;

namespace OfficeDeskScheduler.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateNewUser()
        {
            return View();
        }
    }
}
