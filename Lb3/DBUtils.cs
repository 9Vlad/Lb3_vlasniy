using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Lab3_Primer
{
    class DBUtils
    {

        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "mydb";
            string username = "Vladyslav";
            string password = "vladyslav";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
    }
}
