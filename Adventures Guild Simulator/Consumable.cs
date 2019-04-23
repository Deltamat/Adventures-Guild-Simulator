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

        public Consumable(Vector2 position, string spriteName, int id, string rarity, int skillRating, string type, int goldCost, string name, int uses) : base(position, spriteName, id, rarity, skillRating, type, goldCost, name)
        {
            Uses = uses;
        }

        
    }
}
