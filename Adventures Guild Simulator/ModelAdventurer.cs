﻿using System;
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
                "head integer, " +
                "chest integer, " +
                "boot integer, " +
                "level integer, " +
                "FOREIGN KEY(weapon) REFERENCES Inventory(id)" +
                "FOREIGN KEY(chest) REFERENCES Inventory(id)" +
                "FOREIGN KEY(head) REFERENCES Inventory(id)" +
                "FOREIGN KEY(boot) REFERENCES Inventory(id) )";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Create an level 1 adventurer without equipment
        /// </summary>
        /// <param name="name">Name of the adventurer</param>
        public void CreateAdventurer(string name)
        {
            cmd.CommandText = $"INSERT INTO Adventurer (id, name, level) VALUES (null, '{name}', 1)";
            cmd.ExecuteNonQuery();
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
    }
}
