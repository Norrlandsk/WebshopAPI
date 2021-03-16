using System.Linq;
using WebshopAPI.Database;

namespace WebshopAPI.Utils
{
    public static class Security
    {
        public static bool AdminCheck(int userId)
        {
            bool isUserAdmin = false;
            using (var db = new EFContext())
            {
                var user = db.Users?.FirstOrDefault(u => u.Id == userId);
                if (user != null && user.IsAdmin == true)
                {
                    isUserAdmin = true;
                }
                return isUserAdmin;
            }
        }
    }
}