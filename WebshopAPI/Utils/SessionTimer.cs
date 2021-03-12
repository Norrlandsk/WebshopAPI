using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;


namespace WebshopAPI.Utils
{
    public class SessionTimer
    {

        public void SetSessionTimer()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 5000;

            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;

            Console.WriteLine("Press the Enter key to exit anytime... ");
            Console.ReadLine();
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("Raised: {0}", e.SignalTime);
        }
    }
}

