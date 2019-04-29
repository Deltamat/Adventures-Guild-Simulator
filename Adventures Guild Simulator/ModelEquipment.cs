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
        private SQLiteCommand cmd;

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
                "skillRating integer," +
                "isEquipped bool)";
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
        public Equipment CreateEquipment(string name, string spriteName, string type, string rarity, int goldCost, int skillRating, bool isEquipped)
        {
            Equipment temp = null;
            cmd.CommandText = $"INSERT INTO Equipment (id, name, spriteName, type, rarity, goldCost, skillRating, isEquipped) VALUES (null, '{name}', '{spriteName}', '{type}', '{rarity}', {goldCost}, {skillRating},{isEquipped})";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT * FROM Equipment ORDER BY id desc limit 1";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                temp = new Equipment(Vector2.Zero, reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetBoolean(7));
            }
            reader.Close();
            return temp;
        }

        //Removes the item with the ID from the database equipment
        public void SellEquipment(int ID)
        {
            cmd.CommandText = $"DELETE FROM equipment WHERE id = {ID}";
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// Adds all equipment from the database to a dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Equipment> LoadEquipment()
        {
            Dictionary<int, Equipment> equipmentItems = new Dictionary<int, Equipment>();
            cmd.CommandText = "SELECT * FROM Equipment";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                equipmentItems.Add(reader.GetInt32(0), new Equipment(new Vector2(1000), reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetBoolean(7)));
            }
            reader.Close();
            return equipmentItems;
        }
    }
}
