using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    class ModelAdventurer : Model
    {
        SQLiteCommand cmd;

        public ModelAdventurer()
        {
            string sqlexp = "CREATE TABLE IF NOT EXISTS Adventurer (id integer primary key, " +
                "name string, " +
                "weapon integer, " +
                "helmet integer, " +
                "chest integer, " +
                "boot integer, " +
                "level integer, " +
                "FOREIGN KEY(weapon) REFERENCES Inventory(id)" +
                "FOREIGN KEY(chest) REFERENCES Inventory(id)" +
                "FOREIGN KEY(helmet) REFERENCES Inventory(id)" +
                "FOREIGN KEY(boot) REFERENCES Inventory(id) )";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Create an level 1 adventurer without equipment and adds the person to our ingame list 
        /// </summary>
        /// <param name="name">Name of the adventurer</param>
        public Adventurer CreateAdventurer(string name)
        {
            Adventurer a = null;
            cmd.CommandText = $"INSERT INTO Adventurer (id, name, level) VALUES (null, '{name}', 1)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT * FROM Adventurer ORDER BY id DESC LIMIT 1";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                 a = new Adventurer(Vector2.Zero, "bat", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(6), null, null, null, null);
            }
            reader.Close();
            return a;
        }

        /// <summary>
        /// Get the name of an adventurer
        /// </summary>
        /// <param name="id">The id of the adventurer</param>
        /// <returns>The name of the adventurer</returns>
        public string GetNameByID(int id)
        {
            string name = null;
            cmd.CommandText = "SELECT name FROM Adventurer WHERE id='" + id.ToString() + "'";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                name = reader.GetString(0);
            }
            reader.Close();
            return name;
        }

        /// <summary>
        /// Get the level of an adventurer
        /// </summary>
        /// <param name="id">The id of the adventurer</param>
        /// <returns>The adventurers level</returns>
        public int GetLevelByID(int id)
        {
            int level = 0;
            cmd.CommandText = "SELECT level FROM Adventurer WHERE id='" + id.ToString() + "'";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                level = reader.GetInt32(0);
            }
            reader.Close();
            return level;
        }

        /// <summary>
        /// Delete an adventurer with specific id
        /// </summary>
        /// <param name="id">The id of the adventurer</param>
        public void DeleteAdventurerByID(int id)
        {
            cmd.CommandText = $"DELETE FROM adventurer WHERE id='{id}'";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Get the amount of rows in the table
        /// </summary>
        /// <returns>the count</returns>
        public int GetLength()
        {
            int count = 0;
            cmd.CommandText = "SELECT COUNT(*) FROM Adventurer";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                count = reader.GetInt32(0);
            }
            reader.Close();
            return count;
        }

        /// <summary>
        /// Returns a List of all the adventurers in the table and adds them to the ingame list "adventurers".
        /// </summary>
        /// <returns></returns>
        public List<Adventurer> LoadAdventurers()
        {
            List<Adventurer> adventurers = new List<Adventurer>();
            cmd.CommandText = "SELECT * FROM adventurer";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                adventurers.Add(new Adventurer(Vector2.Zero, "bat", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(6), null, null, null, null));
            }
            reader.Close();
            return adventurers;
        }
    }
}
