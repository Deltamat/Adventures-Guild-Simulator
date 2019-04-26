using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    public class Item : GameObject
    {
        int id;
        int skillRating;
        int goldCost;
        string type;
        string rarity;
        string name;
        private static bool anySelected = false;
        private bool owned = true;
        private bool isInInventory = false;
        private bool isEquipped = false;
        private Color rarityColor = Color.White;


        private MouseState currentMouse;
        private MouseState previousMouse;


        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)(Position.X), (int)(Position.Y), sprite.Width, sprite.Height);
            }
        }

        public bool selected;

        public int Id { get => id; set => id = value; }
        public int SkillRating { get => SkillRating1; set => SkillRating1 = value; }
        public string Type { get => type; set => type = value; }
        public string Name { get => name; set => name = value; }
        public int GoldCost { get => goldCost; set => goldCost = value; }
        public string Rarity { get => rarity; set => rarity = value; }
        public bool Owned { get => owned; set => owned = value; }
        public static bool AnySelected { get => anySelected; set => anySelected = value; }
        public Color RarityColor { get => rarityColor; set => rarityColor = value; }
        public int SkillRating1 { get => skillRating; set => skillRating = value; }
        public bool IsInInventory { get => isInInventory; set => isInInventory = value; }
        public bool IsEquipped { get => isEquipped; set => isEquipped = value; }

        /// <summary>
        /// Constructor for generating items from the database (because it has the "id")
        /// </summary>        
        public Item(Vector2 position, int id, string name, string spriteName, string type, string rarity, int goldCost, int skillRating, bool isEquipped) : base(position, spriteName)
        {
            Id = id;
            Rarity = rarity;
            SkillRating = skillRating;
            Type = type;
            Name = name;
            GoldCost = goldCost;

            //Picks the color for the item text
            if (Rarity == "Common")
            {
               RarityColor = Color.White;
            }

            if (Rarity == "Uncommon")
            {
                RarityColor = Color.Green;
            }

            if (Rarity == "Rare")
            {
               RarityColor = Color.Blue;
            }

            if (Rarity == "Epic")
            {
                RarityColor = Color.Purple;
            }

            if (Rarity == "Legendary")
            {
                RarityColor = Color.Orange;
            }
        }
        
        /// <summary>
        /// Constructor for generating temporary items (because it doesn't need the "id")
        /// </summary>       
        public Item(Vector2 position, string name, string spriteName, string type, string rarity, int goldCost, int skillRating, bool isEquipped) : base(position, spriteName)
        {
            Rarity = rarity;
            SkillRating = skillRating;
            Type = type;
            Name = name;
            GoldCost = goldCost;


        }

        public override void Update(GameTime gameTime)
        {
            #region            
            //"Inception"
            previousMouse = currentMouse;
            //Gets current position and "click info" from the mouse
            currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            //Checks if the mouseRectangle intersects with a button's Rectangle. 
            if (mouseRectangle.Intersects(Rectangle))
            {

                //while hovering over a button, it checks whether you click it 
                //(and release the mouse button while still inside the button's rectangle)
                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    foreach (Quest quest in GameWorld.Instance.quests)
                    {
                        quest.selected = false;
                    }
                    foreach (Button button in GameWorld.Instance.adventurerButtons)
                    {
                        button.selected = false;
                    }
                    GameWorld.Instance.infoScreen.Clear();
                    GameWorld.Instance.questSelected = false;

                    if (AnySelected == true)
                    {
                        foreach (Item item in GameWorld.Instance.inventoryList)
                        {
                            item.selected = false;
                        }
                    }

                    selected = true;
                    AnySelected = true;
                   
                }
            }
            #endregion
        }

        //Generates a random item to the temporary list
        public static void GenerateItem(Vector2 itemPosition)
        {
            int tempRarityGenerator = GameWorld.Instance.GenerateRandom(0, 100);
            int tempSkillRating;
            string tempRarity;
            

            //Generates the rarity of the item
            if (tempRarityGenerator == 99)
            {
                tempRarity = "Legendary";
                tempSkillRating = GameWorld.Instance.GenerateRandom(0, 20) + 80;
            }

            else if (tempRarityGenerator > 90)
            {
                tempRarity = "Epic";
                tempSkillRating = GameWorld.Instance.GenerateRandom(0, 20) + 60;
            }

            else if (tempRarityGenerator > 75)
            {
                tempRarity = "Rare";
                tempSkillRating = GameWorld.Instance.GenerateRandom(0, 20) + 40;
            }

            else if (tempRarityGenerator > 50)
            {
                tempRarity = "Uncommon";
                tempSkillRating = GameWorld.Instance.GenerateRandom(0, 20) + 20;
            }

            else
            {
                tempRarity = "Common";
                tempSkillRating = GameWorld.Instance.GenerateRandom(0, 20) + 1;
            }

            //Generates the item type
            string tempItemType = "Weapon";
            int tempItemTypeGenerate = GameWorld.Instance.GenerateRandom(0, 4);

            if (tempItemTypeGenerate == 0)
            {
                tempItemType = "Weapon";
            }

            else if (tempItemTypeGenerate == 1)
            {
                tempItemType = "Chest";
            }

            else if (tempItemTypeGenerate == 2)
            {
                tempItemType = "helmet";
            }

            else if (tempItemTypeGenerate == 3)
            {
                tempItemType = "Boot";
            }

            //Generates a name using the naming database
            string tempName = $"{ModelNaming.SelectPrefix(GameWorld.Instance.GenerateRandom(38, 100))} {tempItemType} of {ModelNaming.SelectPrefix(GameWorld.Instance.GenerateRandom(1, 39))}";

            //Generates a gold cost from 75% - 125% of the skill rating
            double tempGoldCostGenerate = (Convert.ToDouble(GameWorld.Instance.GenerateRandom(1, 50)) / 100);
            int tempGoldCost = Convert.ToInt32(Math.Round(tempSkillRating * (tempGoldCostGenerate + 0.75)));


            //GameWorld.itemList.Add(new Item(itemPosition, tempItemType, tempRarity, tempSkillRating, tempItemType, tempGoldCost, tempName, false));
            //Adds the item to the database
            Controller.Instance.CreateEquipment(tempName, tempItemType, tempItemType, tempRarity, tempGoldCost, tempSkillRating, false);
            //GameWorld.itemList.Add(new Item(itemPosition, tempItemType, tempRarity, tempSkillRating, tempItemType, tempGoldCost, tempName));
        }

        //Code for drawing the temporary items list
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Position, Color.White);

            spriteBatch.DrawString(GameWorld.fontCopperplate, $"{Name}", Position + new Vector2(100, 0), RarityColor);
            spriteBatch.DrawString(GameWorld.fontCopperplate, $"Cost: {GoldCost}", Position + new Vector2(100, 35), Color.Gold);
            spriteBatch.DrawString(GameWorld.fontCopperplate, $"GearScore: {SkillRating1}", Position + new Vector2(100, 70), Color.White);

            
        }

        //Code for drawing the information once the inventory item is selected
        public void Draw(SpriteBatch spriteBatch, Vector2 Position)
        {
            spriteBatch.Draw(sprite, Position, Color.White);

            if (selected == true)
            {
                spriteBatch.Draw(sprite, Position, Color.Gray);
                spriteBatch.Draw(sprite, new Vector2(580, 130), Color.White);
            }
        }

        //Sells the item by removing it from the database and inventory
        public static void SellItem()
        {
            for (int i = 0; i < GameWorld.Instance.inventoryList.Count; i++)
            {
                if (GameWorld.Instance.inventoryList[i].selected == true)
                {
                    GameWorld.Instance.inventoryFrameList[i].Rarity = "Common";
                    ModelEquipment.SellEquipment(GameWorld.Instance.inventoryList[i].Id);
                    GameWorld.toBeRemovedItem.Add(GameWorld.Instance.inventoryList[i]);
                }
            }
            
            
        }
    }
}
