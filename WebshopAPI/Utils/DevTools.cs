using System;
using System.Collections.Generic;
using WebshopAPI.Models;

namespace WebshopAPI.Utils
{
    public class DevTools
    {
        #region MENU

        
        private WebshopAPI api = new WebshopAPI();

        public void Menu()
        {
            
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("NO LOGIN REQUIRED\n\n");
                Console.WriteLine("[1] List categories");
                Console.WriteLine("[2] Search categories");
                Console.WriteLine("[3] List books in category");
                Console.WriteLine("[4] Get available books in category");
                Console.WriteLine("[5] Info about book");
                Console.WriteLine("[6] Search for book");
                Console.WriteLine("[7] Search for author\n\n");

                Console.WriteLine("LOGIN REQUIRED");
                Console.WriteLine("[8] Login");
                Console.WriteLine("[9] Logout");
                Console.WriteLine("[10] Buy book");
                Console.WriteLine("[11] Register\n\n");


                Console.WriteLine("ADMIN FUNCTIONS\n\n");
                Console.WriteLine("[12] Add book");
                Console.WriteLine("[13] Set amount of books");
                Console.WriteLine("[14] List users");
                Console.WriteLine("[15] Find user");
                Console.WriteLine("[16] Update book");
                Console.WriteLine("[17] Delete book");
                Console.WriteLine("[18] Add category");
                Console.WriteLine("[19] Add book to category");
                Console.WriteLine("[20] Update category");
                Console.WriteLine("[21] Delete category");
                Console.WriteLine("[22] Add user");
                Console.WriteLine("[23] Check sessionTimer");

                var choice0 = Convert.ToInt32(Console.ReadLine());

                switch (choice0)
                {
                    case 1:
                        ListReader(api.GetCategories());

                        break;

                    case 2:
                        var choice1 = Console.ReadLine();

                        ListReader(api.GetCategories(choice1));
                        break;

                    case 3:
                        var choice2 = Convert.ToInt32(Console.ReadLine());
                        ListReader(api.GetCategory(choice2));
                        break;

                    case 4:
                        var choice3 = Convert.ToInt32(Console.ReadLine());
                        ListReader(api.GetAvailableBooks(choice3));
                        break;

                    case 5:
                        var choice4 = Convert.ToInt32(Console.ReadLine());
                        BookReader(api.GetBook(choice4));
                        break;

                    case 6:
                        var choice5 = Console.ReadLine();
                        BookReader(api.GetBooks(choice5));
                        break;

                    case 7:
                        var choice6 = Console.ReadLine();
                        BookReader(api.GetAuthors(choice6));
                        break;

                    case 8:
                        var choice7 = Console.ReadLine();
                        var choice8 = Console.ReadLine();
                        LoginCheck(api.Login(choice7, choice8));
                        break;

                    case 9:
                        var choice9 = Convert.ToInt32(Console.ReadLine());
                        api.Logout(choice9);
                        break;

                    case 10:
                        var choice10 = Convert.ToInt32(Console.ReadLine());
                        var choice11 = Convert.ToInt32(Console.ReadLine());
                        BoolCheck(api.BuyBook(choice10, choice11));
                        break;

                    case 11:
                        var choice12 = Console.ReadLine();
                        var choice13 = Console.ReadLine();
                        var choice14 = Console.ReadLine();

                        BoolCheck(api.Register(choice12, choice13, choice14));
                        break;

                    case 12:
                        var choice15 = Convert.ToInt32(Console.ReadLine());
                        int.TryParse(Console.ReadLine(), out var choice16);
                        var choice17 = Console.ReadLine();
                        var choice18 = Console.ReadLine();
                        var choice19 = Convert.ToInt32(Console.ReadLine());
                        var choice20 = Convert.ToInt32(Console.ReadLine());
                        BoolCheck(api.AddBook(choice15, choice16, choice17, choice18, choice19, choice20));
                        break;

                    case 13:
                        var choice21 = Convert.ToInt32(Console.ReadLine());
                        var choice22 = Convert.ToInt32(Console.ReadLine());
                        var amount = Convert.ToInt32(Console.ReadLine());
                        api.SetAmount(choice21, choice22,amount);
                        break;

                    case 14:
                        var choice23 = Convert.ToInt32(Console.ReadLine());
                        ListReader(api.ListUsers(choice23));
                        break;

                    case 15:
                        var choice24 = Convert.ToInt32(Console.ReadLine());
                        var choice25 = Console.ReadLine();
                        ListReader(api.FindUser(choice24, choice25));
                        break;

                    case 16:
                        var choice26 = Convert.ToInt32(Console.ReadLine());
                        var choice27 = Convert.ToInt32(Console.ReadLine());
                        var choice28 = Console.ReadLine();
                        var choice29 = Console.ReadLine();
                        var choice30 = Convert.ToInt32(Console.ReadLine());
                        BoolCheck(api.UpdateBook(choice26, choice27, choice28, choice29, choice30));
                        break;

                    case 17:
                        var choice31 = Convert.ToInt32(Console.ReadLine());
                        var choice32 = Convert.ToInt32(Console.ReadLine());
                        BoolCheck(api.DeleteBook(choice31, choice32));
                        break;

                    case 18:
                        var choice33 = Convert.ToInt32(Console.ReadLine());
                        var choice34 = Console.ReadLine();
                        BoolCheck(api.AddCategory(choice33, choice34));
                        break;

                    case 19:
                        var choice35 = Convert.ToInt32(Console.ReadLine());
                        var choice36 = Convert.ToInt32(Console.ReadLine());
                        var choice37 = Convert.ToInt32(Console.ReadLine());
                        BoolCheck(api.AddBookToCategory(choice35, choice36, choice37));
                        break;

                    case 20:
                        var choice38 = Convert.ToInt32(Console.ReadLine());
                        var choice39 = Convert.ToInt32(Console.ReadLine());
                        var choice40 = Console.ReadLine();
                        BoolCheck(api.UpdateCategory(choice38, choice39, choice40));
                        break;

                    case 21:
                        var choice41 = Convert.ToInt32(Console.ReadLine());
                        var choice42 = Convert.ToInt32(Console.ReadLine());
                        BoolCheck(api.DeleteCategory(choice41, choice42));
                        break;

                    case 22:
                        var choice43 = Convert.ToInt32(Console.ReadLine());
                        var choice44 = Console.ReadLine();
                        var choice45 = Console.ReadLine();
                        BoolCheck(api.AddUser(choice43, choice44, choice45));
                        break;
                        //case 23:
                        //    sessionTimer.CheckSessionTimer(login);
                        //    break;
                }
            }
        }

        #endregion MENU

        #region READER

        public void BoolCheck(bool boolcheck)
        {
            if (boolcheck == true)
            {
                Console.WriteLine("True");
            }
            else
            {
                Console.WriteLine("False");
            }
        }

        public void LoginCheck(int? userId)
        {
            Console.WriteLine(userId);
        }

        public void ListReader(List<BookCategory> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item.Name);
            }
        }

        public void ListReader(List<User> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item.Name);
            }
        }

        public void ListReader(List<Book> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item.Title);
            }
        }

        public void BookReader(List<Book> book)
        {
            foreach (var item in book)
            {
                Console.WriteLine($"Title: {item.Title}");
                Console.WriteLine($"Author: {item.Author}");
                Console.WriteLine($"Price: {item.Price}");
                Console.WriteLine($"Amount: {item.Amount}");
                Console.WriteLine($"Category: {item.CategoryId}");
                //TODO: Fix Category issue
            }
        }

        #endregion READER
    }
}