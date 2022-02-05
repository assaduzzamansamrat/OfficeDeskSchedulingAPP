using Microsoft.AspNetCore.Mvc;

namespace OfficeDeskScheduler.HelperClasses
{
    public static class NotificationManager 
    {
        public  static string TeamCreateSuccessMessage = "Team created successfully.";
        public  static string UserCreateSuccessMessage = "User created successfully.";
        public static string UserEditSuccessMessage = "User edited successfully.";
        public static string  ContributorInviteSuccessMessage = "Invite sent successfully.";
        public static string DeleteSuccessMessage = "Delete successfull.";
        public static string  ContributorExistErrorMessage = "Already invited to this team.";
        public static string  ContributorSizeErrorMessage = "Team size exeed. Please increase team size.";
        public static string ContributorAssignSuccessMessage = "User assigned successfully.";
        public static string AutoDeskBookingSuccessMessage = "Auto desk booked successfully.";
        public static string AutoDeskBookingErrorMessage = "Desk alreay booked.";
        public static string DeleteErrorMessage = "Delete failed.";
        public static string EmailExistErrorMessage = "Email adready exist. Please try with another email address";
        public static string CommonErrorMessage = "Something wrong please try again.";
        public static string TeamEditSuccessMessage = "Team edited successfully.";
        public static string DeskCreateSuccessMessage = "Desk created successfully.";
        public static string DeskEditSuccessMessage = "Desk edited successfully.";

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

        public static string GetErrorNotificationMessage(Controller controller)
        {
            if (controller.TempData["errorMessage"] != null && controller.TempData["errorMessage"].ToString() != "")
            {
                string mesage = controller.TempData["errorMessage"].ToString();
                return mesage;
            }
            return string.Empty;
        }

        public static bool ResetNotificationMessage(Controller controller)
        {
            controller.TempData["successMessage"] = null;
            controller.TempData["errorMessage"] = null;
            return true;
        }
    }
}
