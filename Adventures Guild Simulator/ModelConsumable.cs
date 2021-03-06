﻿using Microsoft.Xna.Framework;
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
            /// <summary>
            /// Creates the columns for the table, unless the table with the specified name "Consumable" already exists.
            /// </summary>
            string sqlexp = "CREATE TABLE IF NOT EXISTS Consumable (id integer primary key, " +
                "name string, " +
                "spriteName string, " +                
                "type string, " +
                "rarity string, " +
                "goldCost integer, " +
                "skillRating integer, " +
                "isEquipped bool, " +
                "uses integer )";
            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Specifies which attributes to insert values on and then adds said Consumable to the list by returning it as an object.
        /// </summary>
        /// <param name="name">Name of the consumable</param>
        /// <param name="spriteName">Name of the sprite</param>
        /// <param name="type">Type of consumable(only "potion" so far, but ready for "expansion")</param>
        /// <param name="rarity">Common, Uncommon, Rare, Epic, Legendary</param>
        /// <param name="goldCost">Base cost of the item</param>
        /// <param name="skillRating">How much power stat it gives to an adventurer when you equip it on said one</param>
        /// <param name="uses">The amount times you can roll for better result before it's "useless"</param>
        /// <returns>an object of the type Consumable with the specified attributes</returns>
        public Consumable CreateConsumable(string name, string spriteName, string type, string rarity, int goldCost, int skillRating, bool isEquipped, int uses)
        {
            Consumable temp = null;
            cmd.CommandText = $"INSERT INTO Consumable (id, name, spriteName, type, rarity, goldCost, skillRating, isEquipped, uses) VALUES (null, '{name}', '{spriteName}', '{type}', '{rarity}', {goldCost}, {skillRating}, {isEquipped}, {uses})";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "select * from Consumable order by id desc limit 1";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                temp = new Consumable(Vector2.Zero, reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetBoolean(7), reader.GetInt32(8));
            }
            reader.Close();
            return temp;
        }

        //Removes the item with the ID from the database equipment
        public void SellConsumable(int ID)
        {
            cmd.CommandText = $"DELETE FROM consumable WHERE id = {ID}";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Adds all consumables from the database to a dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Consumable> LoadConsumable()
        {
            Dictionary<int, Consumable> consumables = new Dictionary<int, Consumable>();
            cmd.CommandText = "SELECT * FROM Consumable";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Consumable c = new Consumable(new Vector2(650, 200), reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6), false, reader.GetInt32(8));
                if (reader.GetInt16(7) == 1)
                {
                    c.IsEquipped = true;
                }
                consumables.Add(c.Id, c);
            }
            reader.Close();
            return consumables;
        }

        public void UpdateUses(int id, int newUses)
        {
            cmd.CommandText = $"UPDATE consumable SET uses={newUses} WHERE id={id}";
            cmd.ExecuteNonQuery();
        }

        public void Reset()
        {
            cmd.CommandText = "DELETE FROM consumable";
            cmd.ExecuteNonQuery();
        }

        public void EquipConsumable(int id)
        {
            cmd.CommandText = $"UPDATE consumable SET isEquipped={true} WHERE id={id}";
            cmd.ExecuteNonQuery();
        }

        public void UnequipConsumable(int id)
        {
            cmd.CommandText = $"UPDATE consumable SET isEquipped={false} WHERE id={id}";
            cmd.ExecuteNonQuery();
        }
    }
}
