using System;
using System.Collections.Generic;
using System.Text;
using WebshopAPI.Database;
using System.Linq;

namespace WebshopAPI.Utils
{
    public class Seed
    {
        public static void Seeder()
        {
            
            using (var db = new EFContext())
            {
                if (!db.Users.Any())
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
                    db.SaveChanges();
                }
                if (!db.BookCategories.Any())
                {
                    db.BookCategories.Add(new Models.BookCategory
                    {
                        Name = "Horror"
                    });
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
                    db.SaveChanges();
                }
                if (!db.Books.Any())
                {
                    var getBookCategory1 = db.BookCategories.FirstOrDefault(c => c.Name == "Horror");
                    db.Books.Add(new Models.Book
                    {
                        Title = "Cabal (Nightbreed)",
                        Author = "Clive Barker",
                        Price = 250,
                        Amount = 3,
                        Category = getBookCategory1
                    });
                    var getBookCategory2 = db.BookCategories.FirstOrDefault(c => c.Name == "Horror");
                    db.Books.Add(new Models.Book
                    {
                        Title = "The Shining",
                        Author = "Stephen King",
                        Price = 200,
                        Amount = 2,
                        Category = getBookCategory2
                    });
                    var getBookCategory3 = db.BookCategories.FirstOrDefault(c => c.Name == "Horror");
                    db.Books.Add(new Models.Book
                    {
                        Title = "Doctor Sleep",
                        Author = "Stephen King",
                        Price = 200,
                        Amount = 1,
                        Category = getBookCategory3
                    });
                    var getBookCategory4 = db.BookCategories.FirstOrDefault(c => c.Name == "Sci-Fi");
                    db.Books.Add(new Models.Book
                    {
                        Title = "I, Robot",
                        Author = "Isaac Asimov",
                        Price = 150,
                        Amount = 4,
                        Category = getBookCategory4
                    });
                db.SaveChanges();
                }
            }
        }

    }
}
