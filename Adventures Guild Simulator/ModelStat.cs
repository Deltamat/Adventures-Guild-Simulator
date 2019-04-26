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
            string sqlexp = "CREATE TABLE IF NOT EXISTS Stat (id integer primary key, gold integer, deaths integer, completedQuests integer)";
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
            cmd.CommandText = $"REPLACE INTO Stat (id, gold, deaths, completedQuests) VALUES ({1}, {gold}, deaths, completedQuests)";
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

        /// <summary>
        /// Updates the total death count in the database
        /// </summary>
        /// <param name="deaths">The new total deaths value</param>
        public void SetDeaths(int deaths)
        {
            cmd.CommandText = $"REPLACE INTO Stat (id, gold, deaths, completedQuests) VALUES ({1}, gold, {deaths}, completedQuests)";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Gets the total death count and returns it
        /// </summary>
        /// <returns></returns>
        public int LoadDeaths()
        {
            int deaths = 0;
            cmd.CommandText = "SELECT deaths FROM Stat";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                deaths = reader.GetInt32(0);
            }
            reader.Close();
            return deaths;
        }

        /// <summary>
        /// Updates the gold stored in the database
        /// </summary>
        /// <param name="completedQuests">The new gold value</param>
        public void SetCompletedQuests(int completedQuests)
        {
            cmd.CommandText = $"REPLACE INTO Stat (id, gold, deaths, completedQuests) VALUES ({1}, gold, deaths, {completedQuests})";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Gets the gold stored and returns it
        /// </summary>
        /// <returns></returns>
        public int LoadCompletedQuests()
        {
            int completedQuests = 0;
            cmd.CommandText = "SELECT completedQuests FROM Stat";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                completedQuests = reader.GetInt32(0);
            }
            reader.Close();
            return completedQuests;
        }
    }
}
