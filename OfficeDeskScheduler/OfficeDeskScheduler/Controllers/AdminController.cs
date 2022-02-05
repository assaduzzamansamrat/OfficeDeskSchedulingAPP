using Microsoft.AspNetCore.Mvc;
using OfficeDeskScheduler.HelperClasses;
using Services.DataService;
using Services.EntityModels;

namespace OfficeDeskScheduler.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserDataService userDataService;
        private readonly TeamDataService teamDataService;
        private readonly DeskDataService deskDataService;
        const string SessionEmail = "_Email";
        const string SessionUserId = "_UserId";
        const string SessionUserRole = "_UserRole";
        public AdminController(UserDataService _userDataService, TeamDataService _teamDataService, DeskDataService _deskDataService)
        {
            userDataService = _userDataService;
            teamDataService = _teamDataService;
            deskDataService = _deskDataService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                if (HttpContext.Session.GetInt32(SessionUserId)!= null)
                {
                    ViewBag.SuccessMessage = NotificationManager.GetSuccessNotificationMessage(this);
                    ViewBag.ErrorMessage = NotificationManager.GetErrorNotificationMessage(this);
                    NotificationManager.ResetNotificationMessage(this);
                    List<User> usersList = userDataService.GetAll();
                    return View(usersList);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (HttpContext.Session.GetInt32(SessionUserId) != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
          
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            try
            {

                if (user != null)
                {
                    bool isUserExist = userDataService.CheckUserExistOrnotByEmailAddress(user.EmailAddress, 0);
                    if (isUserExist == false)
                    {
                        userDataService.CreateNewUser(user);
                        NotificationManager.SetSuccessNotificationMessage(this, NotificationManager.UserCreateSuccessMessage);
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        NotificationManager.SetErrorNotificationMessage(this, NotificationManager.EmailExistErrorMessage);
                        return RedirectToAction("Index", "Admin");
                    }
                }
                NotificationManager.SetErrorNotificationMessage(this, NotificationManager.CommonErrorMessage);
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IActionResult> CheckEmailExistOrNot(string email, long Id)
        {
            bool isUserExist = userDataService.CheckUserExistOrnotByEmailAddress(email, Id);
            return Json(isUserExist);
        }
        public async Task<IActionResult> Details(long Id)
        {
            try
            {
                if (HttpContext.Session.GetInt32(SessionUserId) != null)
                {
                    User user = userDataService.GetUserByID(Id);
                    return View(user);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
               
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
                if (HttpContext.Session.GetInt32(SessionUserId) != null)
                {
                    User user = userDataService.GetUserByID(Id);
                    return View(user);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
                
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
                if (user != null)
                {
                    bool isUserExist = userDataService.CheckUserExistOrnotByEmailAddress(user.EmailAddress, user.Id);
                    if (isUserExist == false)
                    {
                        bool result = userDataService.UpdateUser(user);
                        NotificationManager.SetSuccessNotificationMessage(this, NotificationManager.UserEditSuccessMessage);
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        NotificationManager.SetErrorNotificationMessage(this, NotificationManager.EmailExistErrorMessage);
                        return RedirectToAction("Index", "Admin");
                    }
                }
                NotificationManager.SetErrorNotificationMessage(this, NotificationManager.CommonErrorMessage);
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
                if (HttpContext.Session.GetInt32(SessionUserId) != null)
                {
                    userDataService.Delete(Id);
                    NotificationManager.SetSuccessNotificationMessage(this, NotificationManager.DeleteSuccessMessage);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
              
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> Team()
        {
            try
            {

                if (HttpContext.Session.GetInt32(SessionUserId) != null)
                {
                    ViewBag.SuccessMessage = NotificationManager.GetSuccessNotificationMessage(this);
                    ViewBag.ErrorMessage = NotificationManager.GetErrorNotificationMessage(this);
                    NotificationManager.ResetNotificationMessage(this);
                    List<Team> teamList = teamDataService.GetAll();
                    return View(teamList);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }

                
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        public async Task<IActionResult> TeamCreate()
        {
            if (HttpContext.Session.GetInt32(SessionUserId) != null)
            {
                List<User> managers = new List<User>();
                managers = userDataService.GetAllManagers();
                return View(managers);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }


        }

        [HttpPost]
        public async Task<IActionResult> TeamCreate(Team team)
        {
            try
            {
                if (team != null)
                {
                    teamDataService.CreateNewTeam(team);
                }
                NotificationManager.SetSuccessNotificationMessage(this, NotificationManager.TeamCreateSuccessMessage);

                return RedirectToAction("Team", "Admin");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> TeamEdit(long Id)
        {
            try
            {
                if (HttpContext.Session.GetInt32(SessionUserId) != null)
                {
                    List<User> managers = new List<User>();
                    managers = userDataService.GetAllManagers();
                    Team team = teamDataService.GetTeamByID(Id);
                    OperationModel opratonModel = new OperationModel();
                    opratonModel.User = managers;
                    opratonModel.Team = team;
                    return View(opratonModel);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
              
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> TeamEdit(Team team)
        {
            try
            {
                bool result = teamDataService.UpdateTeam(team);
                if (result == true)
                {
                    NotificationManager.SetSuccessNotificationMessage(this, NotificationManager.TeamEditSuccessMessage);
                    return RedirectToAction("Team", "Admin");
                }
                return RedirectToAction("Team", "Admin");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IActionResult> TeamDetails(long Id)
        {
            try
            {
                if (HttpContext.Session.GetInt32(SessionUserId) != null)
                {
                    Team team = teamDataService.GetTeamByID(Id);
                    return View(team);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
               
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IActionResult> TeamDelete(long Id)
        {
            try
            {
                if (HttpContext.Session.GetInt32(SessionUserId) != null)
                {
                    teamDataService.Delete(Id);
                    NotificationManager.SetSuccessNotificationMessage(this, NotificationManager.DeleteSuccessMessage);
                    return RedirectToAction("Team", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }

               
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> Desk()
        {
            try
            {
                if (HttpContext.Session.GetInt32(SessionUserId) != null)
                {
                    ViewBag.SuccessMessage = NotificationManager.GetSuccessNotificationMessage(this);
                    ViewBag.ErrorMessage = NotificationManager.GetErrorNotificationMessage(this);
                    NotificationManager.ResetNotificationMessage(this);
                    List<Desk> deskList = deskDataService.GetAll();
                    return View(deskList);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }

              
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IActionResult> DeskCreate()
        {
            if (HttpContext.Session.GetInt32(SessionUserId) != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> DeskCreate(Desk desk)
        {
            try
            {
                if (desk != null)
                {
                    deskDataService.CreateNewDesk(desk);
                }
                NotificationManager.SetSuccessNotificationMessage(this, NotificationManager.DeskCreateSuccessMessage);
                return RedirectToAction("Desk", "Admin");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeskEdit(long Id)
        {
            try
            {
                if (HttpContext.Session.GetInt32(SessionUserId) != null)
                {
                    Desk team = deskDataService.GetDeskByID(Id);
                    return View(team);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeskEdit(Desk desk)
        {
            try
            {
                bool result = deskDataService.UpdateDesk(desk);
                if (result == true)
                {
                    NotificationManager.SetSuccessNotificationMessage(this, NotificationManager.DeskEditSuccessMessage);
                    return RedirectToAction("Desk", "Admin");
                }
                return RedirectToAction("Desk", "Admin");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IActionResult> DeskDetails(long Id)
        {
            try
            {
                if (HttpContext.Session.GetInt32(SessionUserId) != null)
                {
                    Desk desk = deskDataService.GetDeskByID(Id);
                    return View(desk);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
              
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IActionResult> DeskDelete(long Id)
        {
            try
            {
                if (HttpContext.Session.GetInt32(SessionUserId) != null)
                {
                    deskDataService.Delete(Id);
                    NotificationManager.SetSuccessNotificationMessage(this, NotificationManager.DeleteSuccessMessage);
                    return RedirectToAction("Desk", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
               
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> Map()
        {
            return View();
        }

        public async Task<IActionResult> logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }


    }
}
