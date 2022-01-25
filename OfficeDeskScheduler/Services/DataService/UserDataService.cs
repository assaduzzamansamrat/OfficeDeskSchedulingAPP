using Services.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.HelperClasses;

namespace Services.DataService
{
    public class UserDataService
    {
        AppDbContext context = new AppDbContext();

       
        public List<User> GetAll()
        {
            try
            {
               
                return  context.Users.ToList();
                
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
                return context.Users.Where(d => d.Id != id).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

      
        public User GetUserByID(long id)
        {
            try
            {
                return context.Users.SingleOrDefault(d => d.Id == id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public bool UpdateUserWallPaperByUserId(long userid,string wallpaperName)
        {
            try
            {
                User user = new User();
                user =  context.Users.SingleOrDefault(d => d.Id == userid);
                if(user != null)
                {
                    user.Wallpaper = wallpaperName;
                    context.SaveChanges();
                    return true;
                }
                return false;
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
                User user = new User();
                user = context.Users.SingleOrDefault(d => d.Id == userid);
                if (user != null)
                {
                    user.ImagePath = profileImageName;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CreateNewUser(User user)
        {
            bool isUserAddTrue = false;
            user.CreatedBy = -1;
            user.CreatedDate = DateTime.Now;
            user.Password = Utilities.GetPasswordHash(user.Password);
            user.Role = "Student";
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
                isUserAddTrue =true;
                return isUserAddTrue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
