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
            try
            {
                List<User> usersList = userDataService.GetAll();
                return View(usersList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
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

       
    }
}
