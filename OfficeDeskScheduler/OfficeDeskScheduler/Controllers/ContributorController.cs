using Microsoft.AspNetCore.Mvc;
using Services.DataService;
using Services.EntityModels;

namespace OfficeDeskScheduler.Controllers
{
    public class ContributorController : Controller
    {
        private readonly UserDataService userDataService;
        private readonly TeamDataService teamDataService;
        private readonly DeskDataService deskDataService;
        private readonly DeskBookingDataService deskBookingDataService;
        const string SessionEmail = "_Email";
        const string SessionUserId = "_UserId";
        const string SessionUserRole = "_UserRole";
        public ContributorController(UserDataService _userDataService, TeamDataService _teamDataService, DeskDataService _deskDataService, DeskBookingDataService _deskBookingDataService)
        {
            userDataService = _userDataService;
            teamDataService = _teamDataService;
            deskDataService = _deskDataService;
            deskBookingDataService = _deskBookingDataService;
        }
        public IActionResult Index()
        {
            long userId = (long)HttpContext.Session.GetInt32(SessionUserId);
            List<TeamAndContributorMapper> invitationsList =  deskBookingDataService.GetAllInvitationsOfContributorBycontributorId(userId);
            return View(invitationsList);
        }
    }
}
