using Microsoft.AspNetCore.Mvc;
using Services.DataService;
using Services.EntityModels;

namespace OfficeDeskScheduler.Controllers
{
    public class ManagerController : Controller
    {

        private readonly UserDataService userDataService;
        private readonly TeamDataService teamDataService;
        private readonly DeskDataService deskDataService;
        public ManagerController(UserDataService _userDataService, TeamDataService _teamDataService, DeskDataService _deskDataService)
        {
            userDataService = _userDataService;
            teamDataService = _teamDataService;
            deskDataService = _deskDataService;
        }
        public IActionResult Index()
        {
            long managerId = 2;
            List<Team> teamList = teamDataService.GetAllByManagerId(managerId);
            return View(teamList);
        }

      

    }
}
