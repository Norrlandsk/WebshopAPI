using System;
using System.Linq;
using System.Timers;
using WebshopAPI.Database;

namespace WebshopAPI.Utils
{
    public static class PingHandler
    {
        public static string Ping(int userId)
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

        public static void CallCounter(int userId)
        {
            int callCount=0;
            callCount++;
            if (callCount == 10)
            {
                Ping(userId);
                callCount = 0;
            }

        }

    }
}