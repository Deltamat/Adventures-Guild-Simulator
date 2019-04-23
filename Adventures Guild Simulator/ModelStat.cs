using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    class ModelStat : Model
    {
        SQLiteCommand cmd;

        public ModelStat()
        {
            string sqlexp = "CREATE TABLE IF NOT EXISTS Stat (id integer primary key, gold interger)";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Updates the gold stored in the database
        /// </summary>
        /// <param name="gold">The new gold value</param>
        public void SetGold(int gold)
        {
            cmd.CommandText = $"REPLACE INTO Stat (id, gold) VALUES ({1}, {gold})";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Gets the gold stored and returns it
        /// </summary>
        /// <returns></returns>
        public int LoadGold()
        {
            int gold = 0;
            cmd.CommandText = "SELECT gold FROM Stat";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                gold = reader.GetInt32(0);
            }
            reader.Close();
            return gold;
        }
    }
}
