using Microsoft.AspNetCore.Mvc;
using OfficeDeskScheduler.HelperClasses;
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
            if (HttpContext.Session.GetInt32(SessionUserId) != null)
            {
                List<TeamDetails> UpdatetdTeamList = new List<TeamDetails>();
                long userId = (long)HttpContext.Session.GetInt32(SessionUserId);
                List<Team> teamList = teamDataService.GetAllByManagerId(userId);
                foreach (var item in teamList)
                {
                    User user = new User();
                    TeamDetails teamDetails = new TeamDetails();
                    user = userDataService.GetUserByID(item.ManagerId);
                    if (user != null)
                    {
                        teamDetails.Id = item.Id;
                        teamDetails.ManagerId = item.ManagerId;
                        teamDetails.ManagerName = user.FirstName + " " + user.LastName;
                        teamDetails.CreatedBy = item.CreatedBy;
                        teamDetails.CreatedDate = item.CreatedDate;
                        teamDetails.EditedBy = item.EditedBy;
                        teamDetails.EditedDate = item.EditedDate;
                        teamDetails.EquipmentDetails = item.EquipmentDetails;
                        teamDetails.TeamName = item.TeamName;
                        teamDetails.TeamSize = item.TeamSize;
                        UpdatetdTeamList.Add(teamDetails);
                    }
                }
                return View(UpdatetdTeamList);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
           
        }

        public async Task<IActionResult> TeamDetails(long Id)
        {
            try
            {
                if (HttpContext.Session.GetInt32(SessionUserId) != null)
                {
                    TeamDetails UpdatetdTeamDetails = new TeamDetails();
                    Team team = teamDataService.GetTeamByID(Id);
                    if (team != null)
                    {
                        User user = new User();
                        user = userDataService.GetUserByID(team.ManagerId);
                        if (user != null)
                        {
                            UpdatetdTeamDetails.Id = team.Id;
                            UpdatetdTeamDetails.ManagerId = team.ManagerId;
                            UpdatetdTeamDetails.ManagerName = user.FirstName + " " + user.LastName;
                            UpdatetdTeamDetails.CreatedBy = team.CreatedBy;
                            UpdatetdTeamDetails.CreatedDate = team.CreatedDate;
                            UpdatetdTeamDetails.EditedBy = team.EditedBy;
                            UpdatetdTeamDetails.EditedDate = team.EditedDate;
                            UpdatetdTeamDetails.EquipmentDetails = team.EquipmentDetails;
                            UpdatetdTeamDetails.TeamName = team.TeamName;
                            UpdatetdTeamDetails.TeamSize = team.TeamSize;
                        }

                    }
                    return View(UpdatetdTeamDetails);
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

        public async Task<IActionResult> AutoBookDesks(long Id)
        {

            List<Desk> desk = new List<Desk>();
            desk = teamDataService.AutoBookDesks(Id);
            int countBookedesks = deskBookingDataService.GetDeskBookingCountByTeamId(Id);
            if (desk != null && countBookedesks <= 0)
            {
                foreach (var item in desk)
                {
                    DeskBooking deskBooking = new DeskBooking();
                    deskBooking.DeskId = item.Id;
                    deskBooking.Location = "Canada";
                    deskBooking.TeamId = Id;
                    deskBooking.StartDateTime = DateTime.Now;
                    deskBooking.EndDateTime = DateTime.Now.AddDays(1);
                    deskBooking.BookedBy = (long)HttpContext.Session.GetInt32(SessionUserId);
                    deskBookingDataService.CreateNewDeskBooking(deskBooking);
                   
                }
                NotificationManager.SetSuccessNotificationMessage(this, NotificationManager.AutoDeskBookingSuccessMessage);
                return RedirectToAction("Booking", "Manager");
            }
            NotificationManager.SetErrorNotificationMessage(this, NotificationManager.AutoDeskBookingErrorMessage);

            return RedirectToAction("Booking", "Manager");

        }
        [HttpGet]
        public async Task<IActionResult> InviteContributors(long Id)
        {
            if (HttpContext.Session.GetInt32(SessionUserId) != null)
            {
                OperationModel operationModel = new OperationModel();
                Team team = teamDataService.GetTeamByID(Id);
                ViewBag.TeamId = Id;
                ViewBag.TeamName = team.TeamName;
                List<User> users = userDataService.GetAllContributors();
                operationModel.User = users;
                return View(operationModel);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            

        }
        [HttpPost]
        public async Task<IActionResult> InviteContributors(TeamAndContributorMapper teamAndContributorMapper)
        {
            int userExistCount = deskBookingDataService.GetContributorAndTeamdataByTeamIdAndContributorId(teamAndContributorMapper);
            int currentUsercount = deskBookingDataService.GetContributorCountinMapperTableByTeamId(teamAndContributorMapper.TeamId);
            Team team = teamDataService.GetTeamByID(teamAndContributorMapper.TeamId);

            if (userExistCount <= 0 && currentUsercount < team.TeamSize)
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
                teamAndContributorMapper.TeamName = team.TeamName;
                deskBookingDataService.InviteContributors(teamAndContributorMapper);
                
                NotificationManager.SetSuccessNotificationMessage(this, NotificationManager.ContributorInviteSuccessMessage);
                return RedirectToAction("Booking", "Manager");
            }
            else if (userExistCount > 0)
            {
                NotificationManager.SetErrorNotificationMessage(this, NotificationManager.ContributorExistErrorMessage);
                return RedirectToAction("Booking", "Manager");
            }
            else if (currentUsercount >= team.TeamSize)
            {
                NotificationManager.SetErrorNotificationMessage(this, NotificationManager.ContributorSizeErrorMessage);
                return RedirectToAction("Booking", "Manager");

            }
            return RedirectToAction("Booking", "Manager");

        }

        public async Task<IActionResult> DeskBookingDelete(long Id)
        {
            bool isDelete = false;
            isDelete = deskBookingDataService.DeskBookingDelete(Id);
            if (isDelete == true)
            {
                NotificationManager.SetSuccessNotificationMessage(this, NotificationManager.DeleteSuccessMessage);
                return RedirectToAction("Booking", "Manager");
            }
            else
            {
                NotificationManager.SetErrorNotificationMessage(this, NotificationManager.DeleteErrorMessage);
                return RedirectToAction("Booking", "Manager");
            }
        }

        public async Task<IActionResult> AllInvitedContributorList()
        {
            if (HttpContext.Session.GetInt32(SessionUserId) != null)
            {
                long userId = (long)HttpContext.Session.GetInt32(SessionUserId);
                List<TeamAndContributorMapper> teamAndContributorMappers = new List<TeamAndContributorMapper>();
                teamAndContributorMappers = teamDataService.GetAllInvitedContributorList(userId);
                return View(teamAndContributorMappers);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
          
        }
        public async Task<IActionResult> Map()
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

        public async Task<IActionResult> BookFromMap()
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
        public async Task<IActionResult> Booking()
        {
            if (HttpContext.Session.GetInt32(SessionUserId) != null)
            {
                List<DeskBookingDetails> deskBookingDetails = new List<DeskBookingDetails>();
                ViewBag.SuccessMessage = NotificationManager.GetSuccessNotificationMessage(this);
                ViewBag.ErrorMessage = NotificationManager.GetErrorNotificationMessage(this);
                NotificationManager.ResetNotificationMessage(this);
                long userId = (long)HttpContext.Session.GetInt32(SessionUserId);
                List<DeskBooking> booking = deskBookingDataService.GetAll(userId);
                if(booking != null)
                {
                    foreach (var item in booking)
                    {
                        DeskBookingDetails deskBookings = new DeskBookingDetails();
                        Desk desk = deskDataService.GetDeskByID(item.DeskId);
                        if (item.AssignedContributor > 0)
                        {
                            User user = userDataService.GetUserByID(item.AssignedContributor);
                            if (desk != null && user != null)
                            {
                                deskBookings.Id = item.Id;
                                deskBookings.TeamId = item.TeamId;
                                deskBookings.Location = item.Location;
                                deskBookings.Map = item.Map;
                                deskBookings.StartDateTime = item.StartDateTime;
                                deskBookings.EndDateTime = item.EndDateTime;
                                deskBookings.BookedBy = item.BookedBy;
                                deskBookings.DeskId = item.DeskId;
                                deskBookings.DeskNumber = desk.DeskNumber;
                                deskBookings.AssignedContributorId = item.AssignedContributor;
                                deskBookings.AssignedContributorName = user.FirstName + " " + user.LastName;
                                deskBookingDetails.Add(deskBookings);
                            }
                        }
                        else
                        {
                          if (desk != null)
                            {
                                deskBookings.Id = item.Id;
                                deskBookings.TeamId = item.TeamId;
                                deskBookings.Location = item.Location;
                                deskBookings.Map = item.Map;
                                deskBookings.StartDateTime = item.StartDateTime;
                                deskBookings.EndDateTime = item.EndDateTime;
                                deskBookings.BookedBy = item.BookedBy;
                                deskBookings.DeskId = item.DeskId;
                                deskBookings.DeskNumber = desk.DeskNumber;
                                deskBookings.AssignedContributorId = item.AssignedContributor;
                                deskBookings.AssignedContributorName = "N/A";
                                deskBookingDetails.Add(deskBookings);
                            }
                        }
                       
                    }
                }
                return View(deskBookingDetails);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

           
        }
        [HttpGet]
        public async Task<IActionResult> BookingCreate()
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
        public async Task<IActionResult> BookingCreate(DeskBooking deskBooking)
        {
            deskBooking.BookedBy = (long)HttpContext.Session.GetInt32(SessionUserId);
            deskBookingDataService.CreateNewDeskBooking(deskBooking);
            return RedirectToAction("Booking", "Manager");
        }


        [HttpGet]
        public async Task<IActionResult> BookingEdit(long Id)
        {
            if (HttpContext.Session.GetInt32(SessionUserId) != null)
            {
                OperationModel operationModel = new OperationModel();
                DeskBooking deskBooking = deskBookingDataService.GetDeskByID(Id);
                List<User> users = userDataService.GetAllContributors();
                operationModel.User = users;
                operationModel.DeskBooking = deskBooking;
                return View(operationModel);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> BookingEdit(DeskBooking deskBooking)
        {

            deskBookingDataService.UpdateDesk(deskBooking);
            NotificationManager.SetSuccessNotificationMessage(this, NotificationManager.ContributorAssignSuccessMessage);
            return RedirectToAction("Booking", "Manager");
        }

        public async Task<IActionResult> GetBookingForm(string DeskNumber)
        {
            if (HttpContext.Session.GetInt32(SessionUserId) != null)
            {
                Desk desk = deskDataService.GetDeskByDeskNumber(DeskNumber);
                ViewBag.DeskNumber = DeskNumber;
                ViewBag.DeskId = desk.Id;
                ViewBag.EquipmentDetails = desk.EquipmentDetails;
                return PartialView("~/Views/Manager/BookingCreate.cshtml");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
           
        }

        public async Task<IActionResult> GetAllBookedDeskList()
        {
            if (HttpContext.Session.GetInt32(SessionUserId) != null)
            {
                List<DeskBooking> deskBooking = deskDataService.GetallBookedDesk();
                List<Desk> bookedDeskList = new List<Desk>();

                foreach (var item in deskBooking)
                {
                    Desk desk = deskDataService.GetDeskByID(item.DeskId);
                    if (desk != null)
                    {
                        bookedDeskList.Add(desk);
                    }

                }
                return Json(bookedDeskList);
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
    }
}
