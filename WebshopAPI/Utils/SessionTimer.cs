using System;
using System.Linq;
using WebshopAPI.Database;

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
            }
            return isSessionLimitReached;
        }

        public static void AdminSetSessionTimer(int adminId)
        {
            using (var db = new EFContext())
            {
                var admin = db.Users.FirstOrDefault(i => i.Id == adminId);
                admin.SessionTimer = SetSessionTimer(admin.Id);
            }
        }
    }
}