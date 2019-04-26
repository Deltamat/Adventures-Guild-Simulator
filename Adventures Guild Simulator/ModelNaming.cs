using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    class ModelNaming : Model
    {
        static SQLiteCommand cmd;
        static SQLiteDataReader sqlite_datareader;

        public ModelNaming()
        {
            string sqlexp = "CREATE TABLE IF NOT EXISTS naming (id integer primary key, " +
                "name string, " +
                "type string)";

            cmd = connection.CreateCommand();
            cmd.CommandText = sqlexp;
            cmd.ExecuteNonQuery();
        }

        //Adds all of the suffixes and prefixes, should only be done once
        public static void CreateNames()
        {
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Stamina', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'The Bear', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'The Champion', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Bedrock', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Strength', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Intellect', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Agility', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Dexterity', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Constitution', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Wisdom', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'The Owl', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Storms', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Nimbleness', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'The Monkey', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'The Sorcerer', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'The Seer', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'The Bandit', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Marksmanship', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Brawling', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Sneaking', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'The Soldier', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'The Mercenary', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Galeburst', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'The Beast', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Landsliding', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'The Prophet', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'The Deathknight', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'The Warrior', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Undeads', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'The Harpy Queen', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'The Necromancer', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Blazing', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Windflurry', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Illusions', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Enchanting', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'The Zephyr', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'The Squire', 'suffix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Acclaimed', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Almighty', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Arching', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Astonishing', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Bold', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Cunning', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Daring', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Dashing', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Defensive', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Dense', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Dignified', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Divine', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Eccentric', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Elite', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Essential', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Fabled', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Forgotten', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Gleaming', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Glimmering', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Hulking', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Immaculate', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Incorporeal', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Infused', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Loyal', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Majestic', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Merciless', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Meticulous', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Mystic', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Noble', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Novel', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Obedient', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Organic', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Pristine', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Prized', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Ravaging', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Repellant', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Resistant', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Ruthless', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Sacred', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Holy', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Unholy', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Freezing', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Flaming', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Burning', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Blazing', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Cursed', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Hollow', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Lucky', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Creepy', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Bloody', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Filthy', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Crazy', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Precise', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Broken', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Demonic', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Angelic', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Beastial', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Haunted', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Blessed', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Arcane', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Chromatic', 'prefix')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"INSERT INTO naming (id, name, type) VALUES (null, 'Diamond', 'prefix')";
            cmd.ExecuteNonQuery();

        }
        
        //Generates a prefix or suffix for an item
        public static string SelectPrefix(int id)
        {

            cmd.CommandText = $"SELECT name FROM naming WHERE id = {id}";
            sqlite_datareader = cmd.ExecuteReader();

            string Value = "cake";
            while (sqlite_datareader.Read())
            {
                Value = sqlite_datareader.GetString(0);
            }
            sqlite_datareader.Close();
            return Value;
        }
    }
}
