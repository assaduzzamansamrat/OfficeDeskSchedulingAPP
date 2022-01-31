using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DataService;
using Services.EntityModels;

namespace OfficeDeskScheduler.Controllers
{
    public class LoginController : Controller
    {
        LoginDataService loginDomainService;
        const string SessionEmail = "_Email";
        const string SessionUserId = "_UserId";
        const string SessionUserRole = "_UserRole";
        public LoginController(LoginDataService _LoginDomainService)
        {
            loginDomainService = _LoginDomainService;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
           
                return View();
           

        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel login)
        {
            try
            {
                User user;
                login.RememberMe = true;
                user = loginDomainService.AuthenticateAndGetUserByEmailAndPassword(login.UserName, login.Password);
                if (user != null)
                {

                    HttpContext.Session.SetString(SessionEmail, user.EmailAddress);
                    HttpContext.Session.SetInt32(SessionUserId,(int)user.Id);
                    HttpContext.Session.SetString(SessionUserRole, user.Role);
                    
                    if (user.Role == "Admin")
                   {
                        return RedirectToAction("Index", "Admin");
                   }
                   else if (user.Role == "Manager")
                   {
                        return RedirectToAction("Index", "Manager");
                   }

                   else if (user.Role == "Contributor")
                    {
                        return RedirectToAction("Index", "Contributor");
                    }
                    else
                    {
                        return View("Index");
                    }
                }
                else
                {
                    return View("Index");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public void SetRoleForAuthorization(User user, LoginModel login)
        //{
        //    try
        //    {
        //        string userIdAndRoleKeyValuePair = user.Id.ToString() + ";" + user.Role;
        //        var authTicket = new FormsAuthenticationTicket(
        //                       1,                             // version
        //                       user.EmailAddress,             // user name
        //                       DateTime.Now,                  // created
        //                       DateTime.Now.AddDays(365),     // expires
        //                       login.RememberMe,              // persistent?
        //                       userIdAndRoleKeyValuePair      // can be used to store roles
        //                       );

        //        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

        //        var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
        //        System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}

        //public ActionResult LogOut()
        //{
        //    long userId = (long)Session["UserId"];
        //    loginDomainService.UpdateIsUserLoggedInField(userId, false);
        //    Session["UserName"] = null;
        //    Session["UserId"] = null;
        //    Session["Role"] = null;
        //    Session["WallPaper"] = null;
        //    Session["UserProfileImage"] = null;
        //    Session["UserFullName"] = null;
        //    return RedirectToAction("Index", "Login");
        //}
    }
}
