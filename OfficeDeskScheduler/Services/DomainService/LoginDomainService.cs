using Services.DataService;
using Services.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainService
{
    public class LoginDomainService 
    {
        LoginDataService loginDataService;

        public LoginDomainService(LoginDataService _loginDataService)
        {
            loginDataService = _loginDataService;
        }
        public User AuthenticateAndGetUserByEmailAndPassword(string emailAddress, string password)
        {
            try
            {
                return loginDataService.AuthenticateAndGetUserByEmailAndPassword(emailAddress, password);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void UpdateIsUserLoggedInField(long userId, bool isActive)
        {
            try
            {
                 loginDataService.UpdateIsUserLoggedInField(userId, isActive);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
