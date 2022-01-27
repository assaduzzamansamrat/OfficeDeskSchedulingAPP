using Microsoft.AspNetCore.Mvc;

namespace OfficeDeskScheduler.Controllers
{
    public class Contributor : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
