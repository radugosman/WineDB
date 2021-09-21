using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WineDB
{
    class DB_Connection
    {

        private static string CreateConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "20.68.58.80";
            builder.UserID = "dbuser";
            builder.Password = "TestPass1234";
            builder.InitialCatalog = "winedb";
            return builder.ConnectionString;
        }

        private SqlConnection connection = new SqlConnection(CreateConnectionString());

        public SqlConnection getConnection
        {
            get
            {
                return connection;
            }
        }
    }
}
