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

     
        public Inventory(Vector2 position, int id, string name, string spriteName, string type, string rarity, int goldCost, int skillRating, bool isEquipped) :base(position, id, name, spriteName, type, rarity, goldCost, skillRating, isEquipped)
        {
            IsEquipped = isEquipped;
        }

        public static void AddToInventory()
        {
            foreach (Item item in GameWorld.itemList)
            {
                if (item.Owned == true)
                {
                    GameWorld.Instance.inventoryList.Add(item);
                    GameWorld.toBeRemovedItem.Add(item);
                }
            }


            for (int i = 0; i < GameWorld.Instance.inventoryList.Count; i++)
            {
                GameWorld.Instance.inventoryFrameList[i].Rarity = GameWorld.Instance.inventoryList[i].Rarity;
            }

        }

        public static void GenerateInventoryFrames()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int t = 0; t < 4; t++)
                {
                    GameWorld.Instance.inventoryFrameList.Add(new GameObject(new Vector2(1400 + t * 125, 50 + 125 * i), "Frame"));
                }
            }
        }
    }
}
