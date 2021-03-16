using System;
using System.Linq;
using WebshopAPI.Database;
using WebshopAPI.Models;

namespace WebshopAPI.Utils
{
    public static class SessionTimer
    {
        public static DateTime SetSessionTimer(int id)
        {
            DateTime setTime;
            setTime = DateTime.MaxValue;
            using (var db = new EFContext())
            {
                var user = db.Users?.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    user.SessionTimer = DateTime.Now;
                    db.Update(user);
                    db.SaveChanges();
                    setTime = user.SessionTimer;
                }
            }
            return setTime;
        }

        public static bool CheckSessionTimer(DateTime setTime)
        {
            bool isSessionLimitReached = false;
            DateTime sessionLimit = setTime.AddMinutes(15);
            DateTime sessionCompare = DateTime.Now;

            var res = DateTime.Compare(sessionCompare, sessionLimit);

            if (res >= 0)
            {
                isSessionLimitReached = true;
                Console.WriteLine("Time is up!");
            }
            return isSessionLimitReached;
            /*<0 − If date1 is earlier than date2
0 − If date1 is the same as date2
>0 − If date1 is later than date2*/
        }
        public static void AdminSetSessionTimer(int adminId)
        {
            using (var db = new EFContext())
            {
                var admin = db.Users.FirstOrDefault(i => i.Id == adminId);
                admin.SessionTimer = SessionTimer.SetSessionTimer(admin.Id);

            }
        }
    }
}