using System;

namespace WebshopAPI.Utils
{
    public static class SessionTimer
    {
        public static DateTime SetSessionTimer()
        {
            DateTime setTime = DateTime.Now;
            Console.WriteLine("Login = {0:dd} {0:y}, {0:hh}:{0:mm}:{0:ss} ", setTime);
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
    }
}