using System.Collections.Generic;
using System.Linq;
using WebshopAPI.Database;
using WebshopAPI.Models;

namespace WebshopAPI
{
    internal class WebshopAPI
    {
        public int? Login(string userName, string password)
        {
            using (var db = new EFContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Name == userName);
                if (user.Password == password)
                {
                    return user.Id;
                }
                else return null;
            }
        }

        public void Logout(int userId)
        {
            //Sets SessionTimer to
            //DateTime.Default
        }

        //var categoryList = db.BookCategories;

        //if (categoryList != null)
        //{
        //    categoryList.OrderBy(n => n.Name);
        //}

        public List<BookCategory> GetCategories()
        {
            using (var db = new EFContext())
            {
                return (db.BookCategories?.OrderBy(n => n.Name)).ToList();
            }
        }

        public List<BookCategory> GetCategories(string keyword)
        {
            using (var db = new EFContext())
            {
                return db.BookCategories?.Where(x => x.Name.Contains(keyword)).OrderBy(n => n.Name).ToList();
            }
        }

        public void GetCategory(int CategoryId)
        {
        }

        public void GetAvailableBooks(int CategoryId)
        {
            //List of books with amount > 0
        }

        public void GetBook(int bookId)
        {
            //Info about book
        }

        public void GetBooks(string keyword)
        {
            //return matching books
        }

        public void GetAuthors(string keyword)
        {
            //return matching authors
        }

        public bool BuyBook(int userId, int bookId)
        {
            bool purchasecomplete = true;

            return purchasecomplete;
            //Check Session Timer
            //Fail om user inte finns
            //Kopierar bokdata till soldbooks, fyller på med datum och
            //användarId
        }

        public string Ping(int userId)
        {
            //“Pong” if customer is online string.empty is customer is offline
            string ping = "";
            return ping;
        }

        public bool Register(string name, string password, string passwordverify)
        {
            bool customercreated = true;
            //True if user is created
            //False is user already exist False is password != verify

            return customercreated;
        }
    }
}