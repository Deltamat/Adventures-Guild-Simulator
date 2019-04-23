using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    class Item : GameObject
    {
        int id;
        int skillRating;
        string type;
        string rarity;
        int goldCost;
        string name;
        

        public int Id { get => id; set => id = value; }
        public int SkillRating { get => skillRating; set => skillRating = value; }
        public string Type { get => type; set => type = value; }
        public string Name { get => name; set => name = value; }
        public int GoldCost { get => goldCost; set => goldCost = value; }
        public string Rarity { get => rarity; set => rarity = value; }

        public Item(Vector2 position, string spriteName, int id, string rarity, int skillRating, string type, int goldCost, string name) : base(position, spriteName)
        {            
            Rarity = rarity;
            SkillRating = skillRating;
            Type = type;
            Name = name;
            GoldCost = goldCost;
        }
    }
}
