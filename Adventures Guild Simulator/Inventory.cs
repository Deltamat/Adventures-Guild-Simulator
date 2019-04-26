using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
        MouseState mouseState = Mouse.GetState();

        public bool IsEquipped { get => isEquipped; set => isEquipped = value; }

        List<Item> currentInventory = new List<Item>();

     
        public Inventory(Vector2 position, int id, string name, string spriteName, string type, string rarity, int goldCost, int skillRating, bool isEquipped) :base(position, id, name, spriteName, type, rarity, goldCost, skillRating)
        {
            IsEquipped = isEquipped;
        }

        //Adds an item from the temporary item list to the inventory list
        public static void AddToInventory()
        {
            foreach (Item item in GameWorld.itemList)
            {
                 if (item.Owned == true)
                {
                    if (GameWorld.Instance.inventoryFrameList.Count > GameWorld.Instance.inventoryList.Count)
                    {
                        GameWorld.Instance.inventoryList.Add(item);
                        GameWorld.toBeRemovedItem.Add(item);

                    }
                }

            }

            //Turns the frames of the inventory into the rarity of the item in the frame

            for (int i = 0; i < GameWorld.Instance.inventoryList.Count; i++)
            {
                GameWorld.Instance.inventoryFrameList[i].Rarity = GameWorld.Instance.inventoryList[i].Rarity;
            }

            for (int i = 0; i < GameWorld.Instance.inventoryList.Count; i++)
            {
                GameWorld.Instance.inventoryList[i].Position = GameWorld.Instance.inventoryFrameList[i].Position + new Vector2(10,10);
            }

        }

        //Creates the frames
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
