using System;
using System.Collections.Generic;
using System.Text;
using WebshopAPI.Utils;

namespace WebshopAPI
{
    public class Startup
    {
        public void InitialiseSeed()
        {
            Seed.Seeder();
        }
       public void InitialiseDatabase()
        {
            Database.DatabaseCreator.Create();
        }
        
    }
}
