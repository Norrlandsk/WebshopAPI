using WebshopAPI.Utils;

namespace WebshopAPI
{
    /// <summary>
    /// Class for handling objects for starting application
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// Sets up Seeder for use in Main()
        /// </summary>
        public static void InitialiseSeed()
        {
            Seed.Seeder();
        }

        /// <summary>
        /// Sets up DatabaseCreator for use in Main()
        /// </summary>
        public static void InitialiseDatabase()
        {
            Database.DatabaseCreator.Create();
        }
    }
}