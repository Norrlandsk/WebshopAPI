using System;
using System.Collections.Generic;
using System.Text;
using WebshopAPI.Database;
using System.Linq;

namespace WebshopAPI.Utils
{
    public  class Seed
    {
        public static void Seeder()
        {
            using (var db = new EFContext())
            {
                db.Users.Add(new Models.User
                {
                    Name = "Administrator",
                    Password = "Codicrulez",
                    IsAdmin = true
                });
                db.Users.Add(new Models.User
                {
                    Name = "TestCostumer",
                    Password = "Codic2021"
                });
                db.BookCategories.Add(new Models.BookCategory
                {
                    Name = "Horror"
                }) ;
                db.BookCategories.Add(new Models.BookCategory
                {
                    Name = "Humor"
                });
                db.BookCategories.Add(new Models.BookCategory
                {
                    Name = "Sci-Fi"
                });
                db.BookCategories.Add(new Models.BookCategory
                {
                    Name = "Romance"
                });
                var bookCategory = db.BookCategories.FirstOrDefault(c => c.Name == "Horror");
                db.Books.Add(new Models.Book
                {
                    Title = "Cabal (Nightbreed)",
                    Author = "Clive Barker",
                    Price = 250,
                    Amount = 3,
                    Category=bookCategory
                });
                db.SaveChanges();
            }
        }

    }
}
