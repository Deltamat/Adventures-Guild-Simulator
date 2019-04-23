using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    class Equipment : Item
    {        
        public Equipment(Vector2 position, string spriteName, int id, string rarity, int skillRating, string type, int goldCost, string name) : base(position, spriteName, id, rarity, skillRating, type, goldCost, name)
        {           
                        
        }
    }
}
