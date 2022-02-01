﻿using Microsoft.AspNetCore.Mvc;
using Services.DataService;
using Services.EntityModels;

namespace OfficeDeskScheduler.Controllers
{
    public class ManagerController : Controller
    {

        private readonly UserDataService userDataService;
        private readonly TeamDataService teamDataService;
        private readonly DeskDataService deskDataService;
        private readonly DeskBookingDataService deskBookingDataService;
        const string SessionEmail = "_Email";
        const string SessionUserId = "_UserId";
        const string SessionUserRole = "_UserRole";
        public ManagerController(UserDataService _userDataService, TeamDataService _teamDataService, DeskDataService _deskDataService, DeskBookingDataService _deskBookingDataService)
        {
            userDataService = _userDataService;
            teamDataService = _teamDataService;
            deskDataService = _deskDataService;
            deskBookingDataService = _deskBookingDataService;
        }
        public IActionResult Index()
        {
            long userId = (long)HttpContext.Session.GetInt32(SessionUserId);
            List<Team> teamList = teamDataService.GetAllByManagerId(userId);
            return View(teamList);
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

        public async Task<IActionResult> AutoBookDesks(long Id)
        {
            List<Desk> desk = new List<Desk>();
            desk = teamDataService.AutoBookDesks(Id);
            return Json(desk);
        }
        [HttpGet]
        public async Task<IActionResult> InviteContributors(long Id)
        {
            OperationModel operationModel = new OperationModel();
            Team team = teamDataService.GetTeamByID(Id);
            ViewBag.TeamId = Id;
            ViewBag.TeamName = team.TeamName;
            List<User> users = userDataService.GetAllContributors();
            operationModel.User = users;
            return View(operationModel);
           
        }
        [HttpPost]
        public async Task<IActionResult> InviteContributors(TeamAndContributorMapper teamAndContributorMapper)
        {
            int userExistCount = deskBookingDataService.GetContributorAndTeamdataByTeamIdAndContributorId(teamAndContributorMapper);
            if(userExistCount<=0)
            {
                User contributorDetails = userDataService.GetUserByID(teamAndContributorMapper.ContributorId);
                long userId = (long)HttpContext.Session.GetInt32(SessionUserId);
                User managerDetails = userDataService.GetUserByID(userId);
                teamAndContributorMapper.ContributorId = contributorDetails.Id;
                teamAndContributorMapper.ContributorName = contributorDetails.LastName + " " + contributorDetails.LastName;
                teamAndContributorMapper.ManagerId = managerDetails.Id;
                teamAndContributorMapper.ManagerName = managerDetails.LastName + " " + managerDetails.LastName;
                teamAndContributorMapper.IsInvaitationAccept = false;
                teamAndContributorMapper.ChoosedDeskId = 0;
                teamAndContributorMapper.TeamId = teamAndContributorMapper.TeamId;
                deskBookingDataService.InviteContributors(teamAndContributorMapper);
                return RedirectToAction("Booking", "Manager");
            }

            return RedirectToAction("Booking", "Manager");
        }


        public async Task<IActionResult> Map()
        {
            return View();
        }

        public async Task<IActionResult> BookFromMap()
        {
            return View();
        }
        public async Task<IActionResult> Booking()
        {
            long userId = (long)HttpContext.Session.GetInt32(SessionUserId);
            List<DeskBooking> booking = deskBookingDataService.GetAll(userId);
            return View(booking);
        }
        [HttpGet]
        public async Task<IActionResult> BookingCreate( )
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BookingCreate(DeskBooking deskBooking)
        {
            deskBooking.BookedBy = (long)HttpContext.Session.GetInt32(SessionUserId);
            deskBookingDataService.CreateNewDeskBooking(deskBooking);
            return RedirectToAction("Booking", "Manager");
        }

        public async Task<IActionResult> GetBookingForm(string DeskNumber)
        {
            Desk desk = deskDataService.GetDeskByDeskNumber(DeskNumber);
            ViewBag.DeskNumber = DeskNumber;
            ViewBag.DeskId = desk.Id;
            ViewBag.EquipmentDetails = desk.EquipmentDetails;
            return PartialView("~/Views/Manager/BookingCreate.cshtml");
        }

        public async Task<IActionResult> GetAllBookedDeskList()
        {
            List<DeskBooking> deskBooking = deskDataService.GetallBookedDesk();
            List<Desk> bookedDeskList = new List<Desk>();

            foreach (var item in deskBooking)
            {
                Desk desk = deskDataService.GetDeskByID(item.DeskId);
                bookedDeskList.Add(desk);
            }
            return Json(bookedDeskList);
        }

    }
}
