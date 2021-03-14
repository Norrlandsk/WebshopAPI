using System;
using WebshopAPI.Database;
using WebshopAPI.Utils;

namespace WebshopAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            DevTools menu = new DevTools();
            Startup startup = new Startup();
            
            startup.InitialiseDatabase();
            startup.InitialiseSeed();
            var db = new EFContext();
            db.SaveChanges();
            
            
            menu.Menu();
        }
    }
}
