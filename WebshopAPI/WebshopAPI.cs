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
                var user = db.Users?.FirstOrDefault(u => u.Name == userName);
                if (user != null && user.Password == password)
                {
                    user.SessionTimer = SessionTimer.SetSessionTimer(user.Id);
                    user.LastLogin = user.SessionTimer;
                    db.Update(user);
                    db.SaveChanges();

                    return user.Id;
                }
                else return null;
            }
        }

        public void Logout(int userId)
        {
            using (var db = new EFContext())
            {
                var user = db.Users?.FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    user.SessionTimer = DateTime.MinValue;
                    db.Update(user);
                    db.SaveChanges();
                }
            }
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
                return db.Books?.Where(b => b.CategoryId == categoryId).OrderBy(n => n.Title).ToList();
            }
        }

        public List<Book> GetAvailableBooks(int categoryId)
        {
            using (var db = new EFContext())
            {
                return db.Books?.Where(b => b.CategoryId == categoryId).Where(a => a.Amount > 0).OrderBy(n => n.Title).ToList();
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
            bool isPurchaseSuccessful = false;
            using (var db = new EFContext())
            {
                var user = db.Users?.FirstOrDefault(x => x.Id == userId);

                if (user != null)
                {
                    if (SessionTimer.CheckSessionTimer(user.SessionTimer) == false)
                    {
                        var book = db.Books?.FirstOrDefault(x => x.Id == bookId);

                        if (book != null && book.Amount > 0)
                        {
                            SoldBook soldBook = new SoldBook();
                            soldBook.Title = book.Title;
                            soldBook.Author = book.Author;
                            soldBook.CategoryId = book.CategoryId;
                            soldBook.Price = book.Price;
                            soldBook.PurchaseDate = DateTime.Now;
                            soldBook.UserId = user.Id;

                            book.Amount--;

                            db.Update(book);
                            db.Update(soldBook);
                            db.SaveChanges();
                            isPurchaseSuccessful = true;
                            user.SessionTimer = SessionTimer.SetSessionTimer(user.Id);
                        }
                    }
                }
            }

            return isPurchaseSuccessful;
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
            bool isUserCreated = false;

            using (var db = new EFContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Name == name);
                if (user == null && password == passwordverify)
                {
                    user = new User { Name = name, Password = password };
                    db.Update(user);
                    db.SaveChanges();
                    isUserCreated = true;
                }
            }
            return isUserCreated;
        }

        #endregion USER

        #region ADMIN

        public bool AddBook(int adminId, int id, string title, string author, int price, int amount)
        {
            bool isBookAdded = false;
            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    var book = db.Books?.FirstOrDefault(i => i.Id == id);
                    if (book == null)
                    {
                        book = new Book();
                        book.Title = title;
                        book.Author = author;
                        book.Price = price;
                        book.Amount = amount;
                    }
                    else if (book != null)
                    {
                        book.Amount += amount;
                    }
                    SessionTimer.AdminSetSessionTimer(adminId);
                    db.Update(book);
                    db.SaveChanges();
                    isBookAdded = true;
                }
            }

            return isBookAdded;
        }

        public bool SetAmount(int adminId, int bookId, int amount)
        {
            bool isAmountSet = false;
            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    var book = db.Books?.FirstOrDefault(i => i.Id == bookId);
                    if (book != null)
                    {
                        book.Amount = amount;
                        SessionTimer.AdminSetSessionTimer(adminId);
                        db.Update(book);
                        db.SaveChanges();
                        isAmountSet = true;
                    }
                }
            }
            return isAmountSet;
        }

        public List<User> ListUsers(int adminId)
        {
            List<User> userList = new List<User>();
            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    SessionTimer.AdminSetSessionTimer(adminId);
                    return db.Users?.OrderBy(n => n.Name).ToList();
                }
            }
            else
            {
                return userList;
            }
        }

        public List<User> FindUser(int adminId, string keyword)
        {
            List<User> userList = new List<User>();
            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    SessionTimer.AdminSetSessionTimer(adminId);
                    return db.Users?.Where(x => x.Name.Contains(keyword)).OrderBy(n => n.Name).ToList();
                }
            }
            else
            {
                return userList;
            }
        }

        public bool UpdateBook(int adminId, int id, string title, string author, int price)
        {
            bool isBookUpdated = false;
            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    var book = db.Books?.FirstOrDefault(x => x.Id == id);
                    if (book != null)
                    {
                        book.Title = title;
                        book.Author = author;
                        book.Price = price;
                        SessionTimer.AdminSetSessionTimer(adminId);
                        db.Update(book);
                        db.SaveChanges();
                        isBookUpdated = true;
                    }
                }
            }

            return isBookUpdated;
        }

        public bool DeleteBook(int adminId, int bookId)
        {
            bool isBookDeleted = false;
            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    var book = db.Books?.FirstOrDefault(x => x.Id == bookId);
                    if (book != null)
                    {
                    }
                }
            }
            return isBookDeleted;
        }

        public bool AddCategory(int adminId, string name)
        {
            bool isCategoryCreated = false;
            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    var category = db.BookCategories?.FirstOrDefault(x => x.Name == name);

                    if (category == null)
                    {
                        category = new BookCategory();
                        category.Name = name;
                        SessionTimer.AdminSetSessionTimer(adminId);
                        db.Update(category);
                        db.SaveChanges();
                        isCategoryCreated = true;
                    }
                }
            }
            return isCategoryCreated;
        }

        public bool AddBookToCategory(int adminId, int bookId, int categoryId)
        {
            bool isBookAddedToCategory = false;
            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    var book = db.Books?.FirstOrDefault(b => b.Id == bookId);

                    if (book != null)
                    {
                        var category = db.BookCategories?.FirstOrDefault(c => c.Id == categoryId);

                        if (category != null)
                        {
                            book.CategoryId = category.Id;
                            SessionTimer.AdminSetSessionTimer(adminId);
                            db.Update(category);
                            db.SaveChanges();
                            isBookAddedToCategory = true;
                        }
                    }
                }
            }
            return isBookAddedToCategory;
        }

        public bool UpdateCategory(int adminId, int categoryId, string name)
        {
            bool isCategoryUpdated = false;

            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    var category = db.BookCategories?.FirstOrDefault(c => c.Id == categoryId);

                    if (category != null)
                    {
                        category.Name = name;
                        SessionTimer.AdminSetSessionTimer(adminId);
                        db.Update(category);
                        db.SaveChanges();
                        isCategoryUpdated = true;
                    }
                }
            }

                    return isCategoryUpdated;
        }

        public bool DeleteCategory(int adminId, int categoryId)
        {
            bool isCategoryDeleted = false;
            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    var category = db.BookCategories?.FirstOrDefault(c => c.Id == categoryId);

                    if (category != null)
                    {
                        var book = db.Books?.FirstOrDefault(c => c.CategoryId == categoryId);

                        if (book == null)
                        {
                            SessionTimer.AdminSetSessionTimer(adminId);
                            db.BookCategories.Remove(category);
                            db.SaveChanges();
                            isCategoryDeleted = true;
                        }
                    }
                }
            }
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