using System;
using System.Collections.Generic;
using System.Text;
using WebshopAPI.Utils;

namespace WebshopAPI
{

    public static class Startup
    {
        public static void InitialiseSeed()
        {
            Seed.Seeder();
        }
       public static void InitialiseDatabase()
        {
            Database.DatabaseCreator.Create();
        }
        
    }
}
