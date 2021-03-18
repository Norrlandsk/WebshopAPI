using System;
using System.Collections.Generic;
using WebshopAPI.Models;

namespace WebshopAPI.Utils
{
    public static class DevTools
    {
        #region MENU

        private static WebshopAPI api = new WebshopAPI();

        public static void Menu()
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

                Console.WriteLine("ADVANCED ADMIN FUNCTIONS\n\n");
                Console.WriteLine("[23] List sold books");
                Console.WriteLine("[24] Money earned");
                Console.WriteLine("[25] Best customer");
                Console.WriteLine("[26] Promote");
                Console.WriteLine("[27] Demote");
                Console.WriteLine("[28] Activate user");
                Console.WriteLine("[29] Inactivate user");

                int.TryParse(Console.ReadLine(), out var choice0);

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
                        int.TryParse(Console.ReadLine(), out var choice2);
                        ListReader(api.GetCategory(choice2));
                        break;

                    case 4:
                        int.TryParse(Console.ReadLine(), out var choice3);
                        ListReader(api.GetAvailableBooks(choice3));
                        break;

                    case 5:
                        int.TryParse(Console.ReadLine(), out var choice4);
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
                        int.TryParse(Console.ReadLine(), out var choice9);
                        api.Logout(choice9);
                        break;

                    case 10:
                        int.TryParse(Console.ReadLine(), out var choice10);
                        int.TryParse(Console.ReadLine(), out var choice11);
                        BoolCheck(api.BuyBook(choice10, choice11));
                        break;

                    case 11:
                        var choice12 = Console.ReadLine();
                        var choice13 = Console.ReadLine();
                        var choice14 = Console.ReadLine();

                        BoolCheck(api.Register(choice12, choice13, choice14));
                        break;

                    case 12:
                        int.TryParse(Console.ReadLine(), out var choice15);
                        int.TryParse(Console.ReadLine(), out var choice16);
                        var choice17 = Console.ReadLine();
                        var choice18 = Console.ReadLine();
                        int.TryParse(Console.ReadLine(), out var choice19);
                        int.TryParse(Console.ReadLine(), out var choice20);
                        BoolCheck(api.AddBook(choice15, choice16, choice17, choice18, choice19, choice20));
                        break;

                    case 13:
                        int.TryParse(Console.ReadLine(), out var choice21);
                        int.TryParse(Console.ReadLine(), out var choice22);
                        var amount = Convert.ToInt32(Console.ReadLine());
                        BoolCheck(api.SetAmount(choice21, choice22, amount));
                        break;

                    case 14:
                        int.TryParse(Console.ReadLine(), out var choice23);

                        ListReader(api.ListUsers(choice23));
                        break;

                    case 15:
                        int.TryParse(Console.ReadLine(), out var choice24);
                        var choice25 = Console.ReadLine();
                        ListReader(api.FindUser(choice24, choice25));
                        break;

                    case 16:
                        int.TryParse(Console.ReadLine(), out var choice26);
                        int.TryParse(Console.ReadLine(), out var choice27);
                        var choice28 = Console.ReadLine();
                        var choice29 = Console.ReadLine();
                        int.TryParse(Console.ReadLine(), out var choice30);
                        BoolCheck(api.UpdateBook(choice26, choice27, choice28, choice29, choice30));
                        break;

                    case 17:
                        int.TryParse(Console.ReadLine(), out var choice31);
                        int.TryParse(Console.ReadLine(), out var choice32);
                        int.TryParse(Console.ReadLine(), out var amountToDelete);
                        BoolCheck(api.DeleteBook(choice31, choice32,amountToDelete));
                        break;

                    case 18:
                        int.TryParse(Console.ReadLine(), out var choice33);
                        var choice34 = Console.ReadLine();
                        BoolCheck(api.AddCategory(choice33, choice34));
                        break;

                    case 19:
                        int.TryParse(Console.ReadLine(), out var choice35);
                        int.TryParse(Console.ReadLine(), out var choice36);
                        int.TryParse(Console.ReadLine(), out var choice37);
                        BoolCheck(api.AddBookToCategory(choice35, choice36, choice37));
                        break;

                    case 20:
                        int.TryParse(Console.ReadLine(), out var choice38);
                        int.TryParse(Console.ReadLine(), out var choice39);
                        var choice40 = Console.ReadLine();
                        BoolCheck(api.UpdateCategory(choice38, choice39, choice40));
                        break;

                    case 21:
                        int.TryParse(Console.ReadLine(), out var choice41);
                        int.TryParse(Console.ReadLine(), out var choice42);
                        BoolCheck(api.DeleteCategory(choice41, choice42));
                        break;

                    case 22:
                        int.TryParse(Console.ReadLine(), out var choice43);
                        var choice44 = Console.ReadLine();
                        var choice45 = Console.ReadLine();
                        BoolCheck(api.AddUser(choice43, choice44, choice45));
                        break;

                    case 23:
                        int.TryParse(Console.ReadLine(), out var choice46);
                        ListReader(api.SoldItems(choice46));
                        break;

                    case 24:
                        int.TryParse(Console.ReadLine(), out var choice47);
                        Reader(api.MoneyEarned(choice47));
                        break;

                    case 25:
                        int.TryParse(Console.ReadLine(), out var choice48);
                        Reader(api.BestCostumer(choice48));
                        break;

                    case 26:
                        int.TryParse(Console.ReadLine(), out var choice49);
                        int.TryParse(Console.ReadLine(), out var choice50);
                        BoolCheck(api.Promote(choice49,choice50));
                        break;

                    case 27:
                        int.TryParse(Console.ReadLine(), out var choice51);
                        int.TryParse(Console.ReadLine(), out var choice52);
                        BoolCheck(api.Demote(choice51, choice52));
                        break;

                    case 28:
                        int.TryParse(Console.ReadLine(), out var choice53);
                        int.TryParse(Console.ReadLine(), out var choice54);
                        BoolCheck(api.ActivateUser(choice53, choice54));
                        break;

                    case 29:
                        int.TryParse(Console.ReadLine(), out var choice55);
                        int.TryParse(Console.ReadLine(), out var choice56);
                        BoolCheck(api.InactivateUser(choice55, choice56));
                        break;
                }
            }
        }

        #endregion MENU

        #region READER

        private static void BoolCheck(bool boolcheck)
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

        private static void LoginCheck(int? userId)
        {
            Console.WriteLine(userId);
        }

        private static void ListReader(List<BookCategory> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item.Name);
            }
        }
        private static void ListReader(List<SoldBook> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item.Title);
            }
        }
        private static void ListReader(List<User> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item.Name);
            }
        }

        private static void ListReader(List<Book> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item.Title);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="book"></param>
        private static void BookReader(List<Book> book)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private static void Reader(int? value)
        {
            Console.WriteLine(value);
        }
        private static void Reader(string value)
        {
            Console.WriteLine(value);
        }

        #endregion READER
    }
}