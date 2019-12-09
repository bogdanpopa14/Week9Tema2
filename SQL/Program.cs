using System;
using System.Collections.Generic;
using System.Linq;
using DB_Access;
using DB_Entities;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Threading.Tasks;

namespace SQL
{
    class Program
    {
        static void Main(string[] args)
        {
            PublisherRepo repo = new PublisherRepo();
            BookRepo bookrep = new BookRepo();


            List<Book> books = new List<Book>();
            books = bookrep.GetTopNBooks(10);

            List<Publisher> pubs = new List<Publisher>();
            pubs = repo.GetTopNPublisher(10);

            string output = JsonConvert.SerializeObject(books);
            string path = @"C:\Users\Bogdan\Desktop\Programare\Week9Tema2\w9tema2\json.txt";
            File.WriteAllText(path, output);
            XmlSerializer ser = new XmlSerializer(typeof(List<Publisher>));
            var fs = new FileStream(@" C:\Users\Bogdan\Desktop\Programare\Week9Tema2\w9tema2\ser.xml", FileMode.Create);
            ser.Serialize(fs, pubs);

            string output2 = JsonConvert.SerializeObject(pubs);
            string path2 = @"C:\Users\Bogdan\Desktop\Programare\Week9Tema2\w9tema2\json2.txt";
            File.WriteAllText(path2, output2);

            Console.ReadLine();

        }
    }
}
