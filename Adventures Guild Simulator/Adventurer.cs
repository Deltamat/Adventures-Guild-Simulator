using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    class Adventurer : GameObject
    {
        int id;
        string name;
        int level;
        Item weapon;
        Item chest;
        Item helmet;
        Item boot;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Level { get => level; set => level = value; }
        internal Item Weapon { get => weapon; set => weapon = value; }
        internal Item Chest { get => chest; set => chest = value; }
        internal Item Helmet { get => helmet; set => helmet = value; }
        internal Item Boot { get => boot; set => boot = value; }

        public Adventurer()
        {

        }

        public Adventurer(int id, string name, int level, Item weapon, Item chest, Item helmet, Item boot)
        {
            Id = id;
            Name = name;
            Level = level;
            Weapon = weapon;
            Chest = chest;
            Helmet = helmet;
            Boot = boot;
        }
    }
}
