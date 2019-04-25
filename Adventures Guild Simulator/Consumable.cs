using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    class Consumable : Item
    {
        int uses;
                
        public int Uses { get => uses; set => uses = value; }

        public Consumable(Vector2 position, int id, string name, string spriteName, string type, string rarity, int goldCost, int skillRating, int uses) : base(position, id, name, spriteName, type, rarity, goldCost, skillRating)
        {
            Uses = uses;
        }
    }
}
