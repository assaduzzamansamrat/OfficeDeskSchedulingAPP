using Microsoft.AspNetCore.Mvc;

namespace OfficeDeskScheduler.HelperClasses
{
    public static class NotificationManager 
    {
        public  static string TeamCreateSuccessMessage = "Team created successfully.";
        public  static string UserCreateSuccessMessage = "User created successfully.";
        public static string  ContributorInviteSuccessMessage = "Invite sent successfully.";
        public static string  ContributorExistErrorMessage = "Already invited to this team.";
        public static string  ContributorSizeErrorMessage = "Team size exeed. Please increase team size.";


        public static string  SetSuccessNotificationMessage(Controller controller, string message)
        {
            if(message != "" && message != null) {
               controller.TempData["successMessage"] = message;
               return message;
            }
            return string.Empty;
        }
        public static string SetErrorNotificationMessage(Controller controller, string message)
        {
            if (message != "" && message != null)
            {
                controller.TempData["errorMessage"] = message;
                return message;
            }
            return string.Empty;
        }

        public static string GetSuccessNotificationMessage(Controller controller)
        {
            if ( controller.TempData["successMessage"] != null && controller.TempData["successMessage"].ToString() != "")
            {
                string mesage = controller.TempData["successMessage"].ToString();
                return mesage;
            }
            return string.Empty;
        }
    }
}
