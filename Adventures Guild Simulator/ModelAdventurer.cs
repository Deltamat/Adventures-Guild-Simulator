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

        Vector2 adventurerPosition = new Vector2(650, 200);

        public ModelAdventurer()
        {
            string sqlexp = "CREATE TABLE IF NOT EXISTS Adventurer (id integer primary key, " +
                "name string, " +
                "weapon integer, " +
                "helmet integer, " +
                "chest integer, " +
                "boot integer, " +
                "level integer, " +
                "spriteName string," +
                "consumable integer," +
                "FOREIGN KEY(weapon) REFERENCES Equipment(id)," +
                "FOREIGN KEY(chest) REFERENCES Equipment(id)," +
                "FOREIGN KEY(helmet) REFERENCES Equipment(id)," +
                "FOREIGN KEY(boot) REFERENCES Equipment(id)," +
                "FOREIGN KEY(consumable) REFERENCES Consumable(id))";
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
            cmd.CommandText = $"INSERT INTO Adventurer (id, name, level, spriteName) VALUES (null, '{name}', 1, 'defaultSprite')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT * FROM Adventurer ORDER BY id DESC LIMIT 1";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                 a = new Adventurer(new Vector2(650, 200), $"Adventures/{GameWorld.Instance.GenerateRandom(0,58)}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(6), null, null, null, null, null);
            }
            reader.Close();
            return a;
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
        public Dictionary<int, Adventurer> LoadAdventurers()
        {
            Dictionary<int, Adventurer> adventurers = new Dictionary<int, Adventurer>();
            cmd.CommandText = "SELECT * FROM adventurer";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Equipment helmet, weapon, chest, boot;
                Consumable consumable;
                #region TryCatch
                try
                {
                    helmet = GameWorld.Instance.equipmentDic[reader.GetInt32(3)];
                }
                catch (Exception)
                {
                    helmet = null;
                }
                try
                {
                    weapon = GameWorld.Instance.equipmentDic[reader.GetInt32(2)];
                }
                catch (Exception)
                {
                    weapon = null;
                }
                try
                {
                    chest = GameWorld.Instance.equipmentDic[reader.GetInt32(4)];
                }
                catch (Exception)
                {
                    chest = null;
                }
                try
                {
                    boot = GameWorld.Instance.equipmentDic[reader.GetInt32(5)];
                }
                catch (Exception)
                {
                    boot = null;
                }
                try
                {
                    consumable = GameWorld.Instance.consumableDic[reader.GetInt32(8)];
                }
                catch (Exception)
                {
                    consumable = null;
                }
                #endregion

                adventurers.Add(reader.GetInt32(0), new Adventurer(adventurerPosition, reader.GetString(7), reader.GetInt32(0), reader.GetString(1), reader.GetInt32(6), weapon, chest, helmet, boot, consumable));
            }
            reader.Close();
            return adventurers;
        }

        public void SetLevel(int id, int level)
        {
            cmd.CommandText = $"UPDATE adventurer SET level = {level} WHERE id = '{id.ToString()}'";
            cmd.ExecuteNonQuery();
        }

        public void UpdateWeapon(int weaponId, int id)
        {
            cmd.CommandText = $"UPDATE adventurer SET weapon={weaponId} WHERE id={id}";
            cmd.ExecuteNonQuery();
        }
        public void UpdateHelmet(int helmetId, int id)
        {
            cmd.CommandText = $"UPDATE adventurer SET helmet={helmetId} WHERE id={id}";
            cmd.ExecuteNonQuery();
        }
        public void UpdateChest(int chestId, int id)
        {
            cmd.CommandText = $"UPDATE adventurer SET chest={chestId} WHERE id={id}";
            cmd.ExecuteNonQuery();
        }
        public void UpdateBoot(int bootId, int id)
        {
            cmd.CommandText = $"UPDATE adventurer SET boot={bootId} WHERE id={id}";
            cmd.ExecuteNonQuery();
        }
        public void UpdateConsumable(int consumableId, int id)
        {
            cmd.CommandText = $"UPDATE adventurer SET consumable={consumableId} WHERE id={id}";
            cmd.ExecuteNonQuery();
        }

        public void Reset()
        {
            cmd.CommandText = "DELETE FROM Adventurer";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Adventurer (id, name, level, spriteName) VALUES (null, 'Carolus Rex', 1, 'defaultSprite')";
            cmd.ExecuteNonQuery();
        }
    }
}
