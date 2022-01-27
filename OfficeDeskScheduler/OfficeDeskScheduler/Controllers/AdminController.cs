using Microsoft.AspNetCore.Mvc;
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
            return View();
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
                Team team = teamDataService.GetTeamByID(Id);
                return View(team);
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


    }
}
