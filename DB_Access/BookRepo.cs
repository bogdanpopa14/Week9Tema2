using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB_Entities;
using System.Data.SqlClient;
using Connection;
using System.Threading.Tasks;

namespace DB_Access
{
    public class BookRepo
    {
        public List<Book> GetTopNBooks(int N)
        {
            List<Book> books = new List<Book>();
            try
            {
                var querry = $"select top {N} * from [Book]";
                var connection = ConnectionManager.GetConnection();
                SqlCommand comand = new SqlCommand(querry, connection);
                SqlDataReader dataReader = comand.ExecuteReader();
                while (dataReader.Read())
                {
                    var curentRow = dataReader;
                    Book book = new Book();
                    book.BookID = (int)curentRow["BookId"];
                    book.Title = curentRow["Title"].ToString();
                    book.PublisherID = curentRow["PublisherId"] as int? ?? 0;
                    book.Year = curentRow["Year"] as int? ?? 0;
                    book.Price = curentRow["Decimal"] as decimal? ?? 0;
                    books.Add(book);
                }
            }
            catch(SqlException e)
            { Console.WriteLine(e.Message); }
            return books;
        }


        public List<Book> AllBooksFromYear(int year)
        {
            SqlParameter yearParam = new SqlParameter("@IdParam", year);
            List<Book> books = new List<Book>();
            try
            {
                var querry = "select * from Book where Year=@IdParam ";
                var connection = ConnectionManager.GetConnection();
                SqlCommand command = new SqlCommand(querry, connection);
                command.Parameters.Add(yearParam);
                
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    var curentRow = dataReader;
                    Book book = new Book();
                    book.BookID = (int)curentRow["BookId"];
                    book.Title = curentRow["Title"].ToString();
                    book.PublisherID = curentRow["PublisherId"] as int? ?? 0;
                    book.Year = curentRow["Year"] as int? ?? 0;
                    book.Price = curentRow["Decimal"] as decimal? ?? 0;
                    books.Add(book);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return books;
        }


        public List<Book> BooksFromMaxYear()
        {
            List<Book> books = new List<Book>();
            try
            {
                var querry = "select * from Book where [Year]=(select max([Year]) from Book)";
                var connection = ConnectionManager.GetConnection();
                SqlCommand command = new SqlCommand(querry, connection);
                
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    var curentRow = dataReader;
                    Book book = new Book();
                    book.BookID = (int)curentRow["BookId"];
                    book.Title = curentRow["Title"].ToString();
                    book.PublisherID = curentRow["PublisherId"] as int? ?? 0;
                    book.Year = curentRow["Year"] as int? ?? 0;
                    book.Price = curentRow["Decimal"] as decimal? ?? 0;
                    books.Add(book);
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return books;
        }
    }
}
