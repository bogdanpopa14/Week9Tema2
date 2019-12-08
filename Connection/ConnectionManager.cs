using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public class ConnectionManager
    {
        private static SqlConnection connection = null;
        public static SqlConnection GetConnection()
        {
            if(connection==null)
            {
                string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            return connection;
        }
        public static void CloseConnection()
        {
            connection.Close();
        }

    }
}
