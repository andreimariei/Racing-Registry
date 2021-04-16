using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curse.Repository.dbUtils
{
    public class SqliteConnectionFactory : ConnectionFactory
    {
        public override IDbConnection createConnection()
        {
            // Windows Sqlite Connection, fisierul .db ar trebuie sa fie in directorul debug/bin
            //var props = ConfigurationManager.ConnectionStrings["db-mpp"].ConnectionString;
            String props = "Data Source=C:/Users/junki/OneDrive/Desktop/CurseCS/Persistance/bin/CurseDB.db;Version=3;";
            return new SQLiteConnection(props);
        }
    }
}
