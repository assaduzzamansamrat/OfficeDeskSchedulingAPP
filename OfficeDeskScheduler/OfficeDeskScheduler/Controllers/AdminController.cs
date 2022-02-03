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
                ViewBag.SuccessMessage = NotificationManager.GetSuccessNotificationMessage(this);
                ViewBag.ErrorMessage = NotificationManager.GetErrorNotificationMessage(this);
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
                userDataService.Delete(Id);
                NotificationManager.SetSuccessNotificationMessage(this, NotificationManager.DeleteSuccessMessage);
                return RedirectToAction("Index", "Admin");
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
                List<Team> teamList = teamDataService.GetAll();
                return View(teamList);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        public async Task<IActionResult> TeamCreate()
        {
            List<User> managers = new List<User>();
            managers = userDataService.GetAllManagers();
            return View(managers);
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
                List<User> managers = new List<User>();
                managers = userDataService.GetAllManagers();
                Team team = teamDataService.GetTeamByID(Id);
                OperationModel opratonModel = new OperationModel();
                opratonModel.User = managers;
                opratonModel.Team = team;
                return View(opratonModel);
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
                Team team = teamDataService.GetTeamByID(Id);
                return View(team);
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
                teamDataService.Delete(Id);
                return RedirectToAction("Team", "Admin");
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
                List<Desk> deskList = deskDataService.GetAll();
                return View(deskList);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IActionResult> DeskCreate()
        {
            return View();
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
                Desk team = deskDataService.GetDeskByID(Id);
                return View(team);
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
                Desk desk = deskDataService.GetDeskByID(Id);
                return View(desk);
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
                deskDataService.Delete(Id);
                return RedirectToAction("Desk", "Admin");
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

    }
}
