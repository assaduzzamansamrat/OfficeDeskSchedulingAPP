using Microsoft.AspNetCore.Mvc;

namespace OfficeDeskScheduler.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
