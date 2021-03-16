using System;
using System.Linq;
using System.Timers;
using WebshopAPI.Database;

namespace WebshopAPI.Utils
{
    public static class Ping
    {
        public static string SetPingTimer(int userId)
        {
            var ping = string.Empty;

            using (var db = new EFContext())
            {
                var user = db.Users?.FirstOrDefault(u => u.Id == userId);
                if (user != null && user.IsAdmin == false && SessionTimer.CheckSessionTimer(user.SessionTimer) == true)
                {
                    ping = "Pong";
                    user.SessionTimer = DateTime.Now;
                }
            }
            return ping;
        }

        public static bool CheckPingTimer(DateTime setTime)
        {
            bool isSessionLimitReached = false;
            DateTime sessionLimit = setTime.AddMinutes(5);
            DateTime sessionCompare = DateTime.Now;

            var res = DateTime.Compare(sessionCompare, sessionLimit);

            if (res >= 0)
            {
                isSessionLimitReached = true;
            }
            return isSessionLimitReached;
            /*<0 − If date1 is earlier than date2
0 − If date1 is the same as date2
>0 − If date1 is later than date2*/
        }

    }
}