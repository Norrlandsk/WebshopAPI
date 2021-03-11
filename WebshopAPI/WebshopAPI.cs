using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopAPI
{
    class WebshopAPI
    {
        public int Login(string userName, string password)
        {
            int id = 0;

            return id;

            //User Id if success
            //Nothing if fail
            //Set SessionTime

        }
        public void Logout(int userId)
        {

            //Sets SessionTimer to
            //DateTime.Default

        }

        public void GetCategories()
        {

        }
        public void GetCategories(string keyword)
        {

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
        public bool Register(string name,string password, string passwordverify)
        {
            bool customercreated = true;
            //True if user is created
            //False is user already exist False is password != verify

            return customercreated;
        }
    }
}
