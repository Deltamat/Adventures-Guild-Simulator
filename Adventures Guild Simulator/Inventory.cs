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

     
        public Inventory(Vector2 position, int id, string name, string spriteName, string type, string rarity, int goldCost, int skillRating, bool isEquipped) :base(position, id, name, spriteName, type, rarity, goldCost, skillRating)
        {
            IsEquipped = isEquipped;
        }
    }
}
