using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    class Model
    {
        const string CONNECTIONSTRING = @"Data Source=AGS.db;version=3;New=true;Compress=true";
        protected SQLiteConnection connection = new SQLiteConnection(CONNECTIONSTRING);

        public Model()
        {
            connection.Open();
        }
    }
}
