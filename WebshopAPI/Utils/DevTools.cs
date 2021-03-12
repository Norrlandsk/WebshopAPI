using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopAPI.Utils
{
    public class DevTools
    {
        WebshopAPI api = new WebshopAPI();
        public void Menu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("NO LOGIN REQUIRED\n\n");
                Console.WriteLine("[1] List categories");
                Console.WriteLine("[2] Search categories");
                Console.WriteLine("[3] List books in category");
                Console.WriteLine("[4] Get available books");
                Console.WriteLine("[5] Info about book");
                Console.WriteLine("[6] Search for book");
                Console.WriteLine("[7] Search for author\n\n");

                Console.WriteLine("LOGIN REGUIRED");
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
                var choice1 = Convert.ToInt32(Console.ReadLine());

                switch (choice1)
                {
                    case 1:

                        api.GetCategories();
                        break;
                    case 2:
                        var choice21 = Console.ReadLine();
                        api.GetCategories(choice21);
                        break;
                    case 3:
                        var choice2 = Convert.ToInt32(Console.ReadLine());
                        api.GetCategory(choice2);
                        break;
                    case 4:
                        var choice3 = Convert.ToInt32(Console.ReadLine());
                        api.GetAvailableBooks(choice3);
                        break;
                    case 5:
                        var choice4 = Convert.ToInt32(Console.ReadLine());
                        api.GetBook(choice4);
                        break;
                    case 6:
                        var choice5 = Console.ReadLine();
                        api.GetBooks(choice5);
                        break;
                    case 7:
                        var choice6 = Console.ReadLine();
                        api.GetAuthors(choice6);
                        break;
                    case 8:
                        var choice7 = Console.ReadLine();
                        var choice8 = Console.ReadLine();
                        api.Login(choice7, choice8);
                        break;
                    case 9:
                        var choice9 = Convert.ToInt32(Console.ReadLine());
                        api.Logout(choice9);
                        break;
                    case 10:
                        var choice10 = Convert.ToInt32(Console.ReadLine());
                        var choice11 = Convert.ToInt32(Console.ReadLine());
                        api.BuyBook(choice10, choice11);
                        break;
                    case 11:
                        var choice12 = Console.ReadLine();
                        var choice13 = Console.ReadLine();
                        var choice14 = Console.ReadLine();

                        api.Register(choice12, choice13, choice14);
                        break;
                    case 12:

                        break;
                    case 13:
                        break;
                    case 14:
                        break;
                    case 15:
                        break;
                    case 16:
                        break;
                    case 17:
                        break;
                }
            }



        }
        public void ListReader(List<object> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
