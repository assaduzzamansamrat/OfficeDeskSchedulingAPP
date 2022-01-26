using Services.DataService;
using Services.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainService
{
    public class UserDomainService
    {
        UserDataService userDataService;

        public UserDomainService(UserDataService _userDataService)
        {
            userDataService = _userDataService;
        }
        public List<User> GetAllUsersData()
        {
            try
            { 
                
                return userDataService.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

       
        public List<User> GetAllActiveUsersByID(long id)
        {
            try
            {
                return userDataService.GetAllActiveUsersByID(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public User GetUserByID(long id)
        {
            return userDataService.GetUserByID(id);
        }
        public bool UpdateUserWallPaperByUserId(long userid, string wallpaperName)
        {
            try
            {
                return userDataService.UpdateUserWallPaperByUserId(userid, wallpaperName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UpdateUserProfilePicture(long userid, string profileImageName)
        {
            try
            {
                return userDataService.UpdateUserProfilePicture(userid, profileImageName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public bool CreateNewUser(User user)
        {
            try
            {
                return userDataService.CreateNewUser(user);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
