using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Timers;


namespace WebshopAPI.Utils
{
    public class PingTimer
    {
        private static Timer timer;

        public static void PingTimerTest()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 5000;

            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;

            Console.WriteLine("Press the Enter key to exit anytime... ");
            Console.ReadLine();
        }

        private static void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Hej", e.SignalTime);
        }
    }
}