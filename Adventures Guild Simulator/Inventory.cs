using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    class Inventory : Item
    {
        bool isEquipped;

        public bool IsEquipped { get => isEquipped; set => isEquipped = value; }

        List<Item> currentInventory = new List<Item>();

     
        public Inventory(Vector2 position, string spriteName, int id, string rarity, int skillRating, string type, int goldCost, string name, bool isEquipped) :base(position, spriteName, id, rarity, skillRating, type, goldCost, name)
        {
            IsEquipped = isEquipped;
        }
    }
}
