using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DataService;
using Services.EntityModels;

namespace OfficeDeskScheduler.Controllers
{
    public class LoginController : Controller
    {
        LoginDataService loginDomainService;
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
                    //HttpContext.Session.SetString("UserName") = user.EmailAddress;
                    //HttpContext.Session.SetString("UserId") = user.Id;
                    //HttpContext.Session.SetString("Role")["Role"] = user.Role;
                    //HttpContext.Session.SetString("WallPaper")["WallPaper"] = user.Wallpaper;
                    //HttpContext.Session.SetString("UserProfileImage")["UserProfileImage"] = user.ImagePath;
                    //HttpContext.Session.SetString("UserFullName")["UserFullName"] = user.FirstName + " " + user.LastName;
                    // Set Role For Authorization
                   // SetRoleForAuthorization(user, login);
                    return RedirectToAction("Index", "Admin");
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
