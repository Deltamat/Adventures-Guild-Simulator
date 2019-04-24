using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    class ModelConsumable : Model
    {
        SQLiteCommand cmd;

        public ModelConsumable()
        {
            string sqlexp = "CREATE TABLE IF NOT EXISTS Equipment (id integer primary key, " +
                "name string, " +
                "spriteName string, " +                
                "type string, " +
                "rarity string, " +
                "goldCost integer, " +
                "skillRating integer, " +
                "uses integer )";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }

        public void CreateEquipment(string name, string spriteName, string type, string rarity, int goldCost, int skillRating, int uses)
        {
            cmd.CommandText = $"INSERT INTO Equipment (id, name, spriteName, type, rarity, goldCost, skillRating, uses) VALUES (null, '{name}', '{spriteName}', '{type}', '{rarity}', '{goldCost}', '{skillRating}', '{uses}')";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Adds all equipment from the database to a list
        /// </summary>
        /// <returns></returns>
        public List<Consumable> LoadConsumable()
        {
            List<Consumable> consumables = new List<Consumable>();
            cmd.CommandText = "SELECT * FROM Equipment";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                consumables.Add(new Consumable(Vector2.Zero, reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7)));
            }
            reader.Close();
            return consumables;
        }
    }
}
