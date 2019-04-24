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

        /// <summary>
        /// Creates the columns for the table, unless the table with the specified name "Equipment" already exists.
        /// </summary>
        public ModelEquipment()
        {
            string sqlexp = "CREATE TABLE IF NOT EXISTS Equipment (id integer primary key, " +
                "name string, " +
                "spriteName string, " +
                "type string, " +
                "rarity string, " +
                "goldCost integer, " +
                "skillRating integer)";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Specifies which attributes to insert values on and then adds said equipment to the list by returning it as an object.
        /// </summary>
        /// <param name="name">Name of the equipment</param>
        /// <param name="spriteName">Name of the sprite</param>
        /// <param name="type">Type of equipment(Chest, Helmet, Boot, Weapon)</param>
        /// <param name="rarity">Common, Uncommon, Rare, Epic, Legendary</param>
        /// <param name="goldCost">base cost of the item</param>
        /// <param name="skillRating">how much power stat it gives to an adventurer when you equip it on said one</param>
        /// <returns>an object of the type Equipment with the specified attributes</returns>
        public Equipment CreateEquipment(string name, string spriteName, string type, string rarity, int goldCost, int skillRating)
        {
            Equipment temp = null;
            cmd.CommandText = $"INSERT INTO Equipment (id, name, spriteName, type, rarity, goldCost, skillRating) VALUES (null, '{name}', '{spriteName}', '{type}', '{rarity}', '{goldCost}', '{skillRating}')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT * FROM Equipment ORDER BY id desc limit 1";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                temp = new Equipment(Vector2.Zero, reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6));
            }
            reader.Close();
            return temp;
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
                equipmentItems.Add(new Equipment(Vector2.Zero, reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6)));
            }
            reader.Close();
            return equipmentItems;
        }
    }
}
