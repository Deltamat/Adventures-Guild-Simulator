using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    class ModelEquipment : Model
    {
        SQLiteCommand cmd;

        public ModelEquipment()
        {
            string sqlexp = "CREATE TABLE IF NOT EXISTS Equipment (id integer primary key, " +
                "name string, " +
                "spriteName string, " +
                "value integer, " +
                "type string, " +               
                "skillRating integer, ";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }

        public void CreateEquipment(string name, string type, int skillRating)
        {
            cmd.CommandText = $"INSERT INTO Equipment (id, name, type, skillRating) VALUES (null, '{name}', '{type}', '{skillRating}')";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Adds all equipment from the database to a list
        /// </summary>
        /// <returns></returns>
        public List<Equipment> LoadEquipment()
        {
            List<Equipment> equipmentItems = new List<Equipment>();
            cmd.CommandText = "SELECT * FROM Equipment";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //equipmentItems.Add(new Equipment(Vector2.Zero, reader.GetString(2), reader.GetInt32(0), reader.GetInt32(5), reader.GetString(4), reader.GetString(1)));
            }
            reader.Close();
            return equipmentItems;
        }
    }
}
