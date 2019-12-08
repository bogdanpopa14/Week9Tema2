using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Connection;
using DB_Entities;
using System.Threading.Tasks;

namespace DB_Access
{
    public class PublisherRepo
    {

        public int NrOfRows()
        {

            int cnt = 0;
            try
            {
                string commandQuery = "select count(PublisherId) from Publisher";
                var connection = ConnectionManager.GetConnection();
                SqlCommand countCommand = new SqlCommand(commandQuery, connection);
                var count = countCommand.ExecuteScalar();
                Console.WriteLine(count);
                cnt = Convert.ToInt32(count);
            }

            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return cnt;
        }

        public List<Publisher> GetTopNPublisher(int N)
        {
            List<Publisher> publishers = new List<Publisher>();
            try
            {
                var querry = $"select top {N} * from [Publisher]";
                var connection = ConnectionManager.GetConnection();
                SqlCommand comand = new SqlCommand(querry, connection);
                SqlDataReader dataReader = comand.ExecuteReader();
                while (dataReader.Read())
                {
                    var curentRow = dataReader;
                    Publisher publisher = new Publisher();
                    publisher.Name = curentRow["Name"].ToString();
                    publisher.PublisherID = curentRow["PublisherId"] as int? ?? 0;
                    publishers.Add(publisher);
                }
            }
            catch (SqlException e)
            { Console.WriteLine(e.Message); }
            return publishers;
        }

        public List<NumberOfBooksPerPublisher> GetNumberOfBooksPerPublishers()
        {
            List<NumberOfBooksPerPublisher> pubBooks = new List<NumberOfBooksPerPublisher>();
            try
            {
                var querry = "select [Name] ,(select count(BookId) from Book where Publisher.PublisherId=Book.PublisherId) as Nr from Publisher";
                var connection = ConnectionManager.GetConnection();
                SqlCommand command = new SqlCommand(querry, connection);

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    var currentRow = dataReader;
                    NumberOfBooksPerPublisher publisher = new NumberOfBooksPerPublisher();
                    publisher.publisher.Name = currentRow["Name"].ToString();
                    publisher.nrBooks = currentRow["Nr"] as int? ?? 0;
                    pubBooks.Add(publisher);
                   
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return pubBooks;
        }

        public void TotalBooksPrice(string ColumnName)
        {
            SqlParameter nameParam = new SqlParameter("@NameParam", ColumnName);
            try
            {
                var querry = "select [Name] ,(select sum(Price) from Book) as Nr from Publisher where [Name] = @NameParam  ";
                var connection = ConnectionManager.GetConnection();
                SqlCommand comand = new SqlCommand(querry, connection);
                comand.Parameters.Add(ColumnName);
                comand.ExecuteNonQuery();

                SqlDataReader dataReader = comand.ExecuteReader();
                while (dataReader.Read())
                {
                    var currentRow = dataReader;
                    var name = currentRow["Name"];
                    var id = currentRow["Nr"];


                    Console.WriteLine($"Total books price for {name} - {id}");
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
