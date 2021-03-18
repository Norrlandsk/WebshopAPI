using WebshopAPI.Utils;

namespace WebshopAPI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Startup.InitialiseDatabase();
            Startup.InitialiseSeed();
            DevTools.Menu();
        }
    }
}