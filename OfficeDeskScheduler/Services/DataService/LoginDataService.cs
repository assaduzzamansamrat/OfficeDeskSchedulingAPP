using Services.EntityModels;
using Services.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataService
{
    public class LoginDataService
    {
        private  AppDbContext context = new AppDbContext();
        
       
        public User AuthenticateAndGetUserByEmailAndPassword(string emailAddress, string password)
        {
            try
            {
                User user;
                string hasedPassword = Utilities.GetPasswordHash(password);
                user = context.Users.FirstOrDefault(d => d.EmailAddress == emailAddress && d.Password == hasedPassword);
                if (user != null)
                {
                    // Setting Is User Active 
                    UpdateIsUserLoggedInField(user.Id, true);
                }
                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void UpdateIsUserLoggedInField(long userId,bool isActive)
        {
            try
            {
                User user;
                user = context.Users.FirstOrDefault(d => d.Id == userId);
                if(user != null)
                {
                    user.IsUserLoggedIn = isActive;
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
