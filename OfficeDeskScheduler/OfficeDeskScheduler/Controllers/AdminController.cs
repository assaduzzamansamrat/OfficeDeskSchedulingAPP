using Microsoft.AspNetCore.Mvc;
using Services.DataService;
using Services.EntityModels;

namespace OfficeDeskScheduler.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserDataService userDataService;
        public AdminController(UserDataService _userDataService)
        {
            userDataService = _userDataService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                if(user != null)
                {
                    userDataService.CreateNewUser(user);
                }
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IActionResult GetAll()
        {
            try
            {
                return View(userDataService.GetAll());
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
