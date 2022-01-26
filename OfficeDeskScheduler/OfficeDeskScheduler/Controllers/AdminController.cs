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
        public async Task<IActionResult> Index()
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
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
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


        public async Task<IActionResult> Details(long Id)
        {
            try
            {
                User user = userDataService.GetUserByID(Id);
                return View(user);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long Id)
        {
            try
            {
                User user = userDataService.GetUserByID(Id);
                return View(user);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            try
            {
                bool result =  userDataService.UpdateUser(user);
                if(result == true)
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IActionResult> Delete(long Id)
        {
            try
            {
                userDataService.Delete(Id);
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
