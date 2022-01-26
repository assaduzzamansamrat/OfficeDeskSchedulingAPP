using Microsoft.AspNetCore.Mvc;

namespace OfficeDeskScheduler.Controllers
{
    public class ContributorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
