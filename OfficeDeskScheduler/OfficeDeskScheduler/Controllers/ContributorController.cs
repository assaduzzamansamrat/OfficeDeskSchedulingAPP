using Microsoft.AspNetCore.Mvc;
using OfficeDeskScheduler.HelperClasses;
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
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32(SessionUserId) != null)
            {
                ViewBag.SuccessMessage = NotificationManager.GetSuccessNotificationMessage(this);
                ViewBag.ErrorMessage = NotificationManager.GetErrorNotificationMessage(this);
                NotificationManager.ResetNotificationMessage(this);
                long userId = (long)HttpContext.Session.GetInt32(SessionUserId);
                List<TeamAndContributorMapper> invitationsList = deskBookingDataService.GetAllInvitationsOfContributorBycontributorId(userId);
                return View(invitationsList);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
           
        }

        public async Task<IActionResult> AcceptInvitation(long Id)
        {
            if (HttpContext.Session.GetInt32(SessionUserId) != null)
            {
                deskBookingDataService.AcceptOrRejectInvitaions(true, Id);
                NotificationManager.SetSuccessNotificationMessage(this, NotificationManager.AcceptSuccessMessage);
                return RedirectToAction("Index", "Contributor");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

           
        }
        public async Task<IActionResult> RejectInvitation(long Id)
        {
            if (HttpContext.Session.GetInt32(SessionUserId) != null)
            {
                deskBookingDataService.AcceptOrRejectInvitaions(false, Id);
                NotificationManager.SetSuccessNotificationMessage(this, NotificationManager.RejectSuccessMessage);
                return RedirectToAction("Index", "Contributor");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
          
        }

        public async Task<IActionResult> logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        public async Task<ActionResult> ChooseDesk()
        {
            if (HttpContext.Session.GetInt32(SessionUserId) != null)
            {
                long userId = (long)HttpContext.Session.GetInt32(SessionUserId);
                List<DeskBooking> deskBookingList = deskBookingDataService.GetAvailabeDesksForChoosing(userId);
                return View(deskBookingList);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }

        public async Task<ActionResult> ChooseThisDesk(long Id)
        {

            long userId = (long)HttpContext.Session.GetInt32(SessionUserId);
            bool result = deskBookingDataService.ChooseThisDesk(Id, userId);
            return RedirectToAction("Index", "Contributor");
        }
    }
}
