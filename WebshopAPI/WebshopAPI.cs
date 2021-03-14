using System;
using System.Collections.Generic;
using System.Linq;
using WebshopAPI.Database;
using WebshopAPI.Models;
using WebshopAPI.Utils;

namespace WebshopAPI
{
    internal class WebshopAPI
    {
        #region USER
        
        public int? Login(string userName, string password)
        {
            
            using (var db = new EFContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Name == userName);
                if (user.Password == password)
                {
                    user.SessionTimer=SessionTimer.SetSessionTimer();
                    user.LastLogin = user.SessionTimer;
                    db.Update(user);
                    db.SaveChanges();
                    return user.Id;
                }
                else return null;
            }
            //TODO: Add sessiontimer to Login method
        }

        public void Logout(int userId)
        {
            
            //Sets SessionTimer to
            //DateTime.Default
            //TODO: Create Logout method
        }


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

        public List<Book> GetCategory(int categoryId)
        {
            using (var db = new EFContext())
            {
                return db.Books?.Where(b => b.Category.Id == categoryId).OrderBy(n => n.Title).ToList();
            }
        }

        public List<Book> GetAvailableBooks(int categoryId)
        {
            using (var db = new EFContext())
            {
                return db.Books?.Where(b => b.Category.Id == categoryId).Where(a => a.Amount > 0).OrderBy(n => n.Title).ToList();
            }
        }

        public List<Book> GetBook(int bookId)
        {
            using (var db = new EFContext())
            {
                return db.Books?.Where(b => b.Id == bookId).ToList();
            }
        }

        public List<Book> GetBooks(string keyword)
        {
            using (var db = new EFContext())
            {
                return db.Books?.Where(x => x.Title.Contains(keyword)).OrderBy(n => n.Title).ToList();
            }
        }

        public List<Book> GetAuthors(string keyword)
        {
            using (var db = new EFContext())
            {
                return db.Books?.Where(x => x.Author.Contains(keyword)).OrderBy(n => n.Title).ToList();
            }
        }

        public bool BuyBook(int userId, int bookId)
        {
            bool purchasecomplete = true;
            //TODO: Create BuyBook method
            return purchasecomplete;
            //Check Session Timer
            //Fail om user inte finns
            //Kopierar bokdata till soldbooks, fyller på med datum och
            //användarId
        }

        public string Ping(int userId)
        {
            //TODO:Create Ping method
            //“Pong” if customer is online string.empty is customer is offline
            string ping = "";
            return ping;
        }

        public bool Register(string name, string password, string passwordverify)
        {
            bool userCreated = false;

            using (var db = new EFContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Name == name);
                if (user == null && password == passwordverify)
                {
                    user = new User { Name = name, Password = password };
                }
                if (user != null)
                {
                    db.Update(user);
                    db.SaveChanges();
                    userCreated = true;

                }
            }
            return userCreated;
        }

        #endregion USER
        #region ADMIN
        public bool AddBook(int adminId, int id, string title, string author, int price, int amount)
        {
            bool isBookAdded = false;
            return isBookAdded;

            /*Öka book.amount om boken redan  finns, annars sätt book.amount till  antal som skickades in 
adminId är den inloggade  
användarens Id
*/
        }
        public void SetAmount(int adminId, int bookId)
        {

        }
        public List<User> ListUsers(int adminId)
        {
            List<User> userList = new List<User>();
            return userList;
        }
        public List<User> FindUser(int adminId, string keyword)
        {
            List<User> userList = new List<User>();
            return userList;
        }
        public bool UpdateBook(int adminId, int id, string title, string author, int price)
        {
            bool isBookUpdated = false;
            return isBookUpdated;
        }
        public bool DeleteBook(int adminId, int bookId)
        {
            bool isBookDeleted = false;
            return isBookDeleted;
        }
        public bool AddCategory(int adminId, string name)
        {
            bool isCategoryCreated = false;
            return isCategoryCreated;
        }
        public bool AddBookToCategory(int adminId, int bookId, int categoryId)
        {
            bool isBookAddedToCategory = false;
            return isBookAddedToCategory;
        }
        public bool UpdateCategory(int adminId, int categoryId, string name)
        {
            bool isCategoryUpdated = false;
            return isCategoryUpdated;
        }
        public bool DeleteCategory(int adminId, int categoryId)
        {
            bool isCategoryDeleted = false;
            return isCategoryDeleted;
            //Fails om kategorin inte är tom
        }
        public bool AddUser(int adminId, string name, string password)
        {
            bool isUserCreated = false;
            return isUserCreated;
            //Fails om user redan finns 
            //Fails om lösenord är tom

        }
        #endregion ADMIN
    }
}