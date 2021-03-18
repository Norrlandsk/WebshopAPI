﻿using System;
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
        /// <summary>
        /// Logins user, fails if user does not exist, if password does not match, if user.IsActive=false
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>int userId if successful, null int if unsuccessful</returns>
        public int? Login(string userName, string password)
        {
            using (var db = new EFContext())
            {
                var user = db.Users?.FirstOrDefault(u => u.Name == userName);
                if (user != null && user.Password == password && user.IsActive)
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
                            Ping(userId);
                            isPurchaseSuccessful = true;
                            user.SessionTimer = SessionTimer.SetSessionTimer(user.Id);
                        }
                    }
                }
            }

            return isPurchaseSuccessful;
        }

        public static string Ping(int userId)
        {
            var ping = string.Empty;

            using (var db = new EFContext())
            {
                var user = db.Users?.FirstOrDefault(u => u.Id == userId);
                if (user != null && SessionTimer.CheckSessionTimer(user.SessionTimer) == false)
                {
                    ping = "Pong";
                    user.SessionTimer = DateTime.Now;
                }
            }
            return ping;
        }

        public bool Register(string name, string password, string passwordverify)
        {
            bool isUserCreated = false;

            using (var db = new EFContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Name == name);
                if (user == null && password == passwordverify && password != "")
                {
                    user = new User { Name = name, Password = password, IsAdmin = false, IsActive = true };
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
                    var book = db.Books?.FirstOrDefault(i => i.Title == title);
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

        public bool DeleteBook(int adminId, int bookId, int amount = 0)
        {
            bool isBookDeleted = false;
            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    var book = db.Books?.FirstOrDefault(x => x.Id == bookId);
                    if (book != null)
                    {
                        if (amount == 0)
                        {
                            book.Amount--;
                            db.Update(book);
                        }
                        else if (amount > 0)
                        {
                            book.Amount -= amount;
                            db.Update(book);
                        }
                        if (book.Amount <= 0)
                        {
                            db.Remove(book);
                        }
                        db.SaveChanges();
                        SessionTimer.AdminSetSessionTimer(adminId);
                        isBookDeleted = true;
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
            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    var user = db.Users?.FirstOrDefault(u => u.Name == name);

                    if (user == null)
                    {
                        user = new User { Name = name, Password = password, IsAdmin = false, IsActive = true };

                        if (user.Password == "")
                        {
                            user.Password = "Codic2021";
                        }
                        SessionTimer.AdminSetSessionTimer(adminId);
                        db.Update(user);
                        db.SaveChanges();
                        isUserCreated = true;
                    }
                }
            }
            return isUserCreated;
        }

        #endregion ADMIN

        #region ADV ADMIN

        public List<SoldBook> SoldItems(int adminId)
        {
            List<SoldBook> bookList = new List<SoldBook>();
            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    SessionTimer.AdminSetSessionTimer(adminId);
                    return db.SoldBooks?.OrderBy(n => n.Title).ToList();
                }
            }
            else
            {
                return bookList;
            }
        }

        public int? MoneyEarned(int adminId)
        {
            int? earned = null;
            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    var books = db.SoldBooks?.ToList();
                    earned = books.Sum(b => b.Price);
                    SessionTimer.AdminSetSessionTimer(adminId);
                    return earned;
                }
            }
            else
            {
                return earned;
            }
        }

        public string BestCostumer(int adminId)
        {
            string bestCostumer = "";
            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    SessionTimer.AdminSetSessionTimer(adminId);
                    return bestCostumer;
                }
            }
            else
            {
                return bestCostumer;
            }
        }

        public bool Promote(int adminId, int userId)
        {
            bool isPromoted = false;
            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    var user = db.Users?.FirstOrDefault(u => u.Id == userId);

                    if (user != null)
                    {
                        user.IsAdmin = true;

                        SessionTimer.AdminSetSessionTimer(adminId);
                        db.Update(user);
                        db.SaveChanges();

                        isPromoted = true;
                    }
                }
            }

            return isPromoted;
        }

        public bool Demote(int adminId, int userId)
        {
            bool isDemoted = false;
            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    var user = db.Users?.FirstOrDefault(u => u.Id == userId);

                    if (user != null)
                    {
                        user.IsAdmin = false;

                        SessionTimer.AdminSetSessionTimer(adminId);
                        db.Update(user);
                        db.SaveChanges();

                        isDemoted = true;
                    }
                }
            }
            return isDemoted;
        }

        public bool ActivateUser(int adminId, int userId)
        {
            bool isActivated = false;
            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    var user = db.Users?.FirstOrDefault(u => u.Id == userId);

                    if (user != null)
                    {
                        user.IsActive = true;

                        SessionTimer.AdminSetSessionTimer(adminId);
                        db.Update(user);
                        db.SaveChanges();

                        isActivated = true;
                    }
                }
            }
            return isActivated;
        }

        public bool InactivateUser(int adminId, int userId)
        {
            bool isInactivated = false;
            if (Security.AdminCheck(adminId))
            {
                using (var db = new EFContext())
                {
                    var user = db.Users?.FirstOrDefault(u => u.Id == userId);

                    if (user != null)
                    {
                        user.IsActive = false;

                        SessionTimer.AdminSetSessionTimer(adminId);
                        db.Update(user);
                        db.SaveChanges();

                        isInactivated = true;
                    }
                }
            }
            return isInactivated;
        }

        #endregion ADV ADMIN
    }
}