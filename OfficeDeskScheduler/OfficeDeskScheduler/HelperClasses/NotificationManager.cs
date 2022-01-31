using Microsoft.AspNetCore.Mvc;

namespace OfficeDeskScheduler.HelperClasses
{
    public static class NotificationManager 
    {
        private readonly static string TeamCreateSuccessMessage = "Team created successfully.";

        public static string  GetNotificationMessageByActionName(string actionName)
        {
            if(actionName == "TeamCreate")
            {
                return TeamCreateSuccessMessage;
            }
            return string.Empty;
        }
    }
}
