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
        string name;
        private static bool anySelected = false;
        private bool owned = true;
        private bool isInInventory = false;
        private bool isEquipped = false;
        private Color rarityColor = Color.White;
        private static int selectedID;
        bool selectedSwitch = false;
        bool previousSelectedSwitch = false;
        float delay;

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
        public int SkillRating { get => skillRating; set => skillRating = value; }
        public string Type { get => type; set => type = value; }
        public string Name { get => name; set => name = value; }
        public int GoldCost { get => goldCost; set => goldCost = value; }
        public bool Owned { get => owned; set => owned = value; }
        public static bool AnySelected { get => anySelected; set => anySelected = value; }
        public Color RarityColor { get => rarityColor; set => rarityColor = value; }
        public bool IsInInventory { get => isInInventory; set => isInInventory = value; }
        public bool IsEquipped { get => isEquipped; set => isEquipped = value; }
        public static int SelectedID { get => selectedID; set => selectedID = value; }
        public bool SelectedSwitch { get => selectedSwitch; set => selectedSwitch = value; }
        public bool PreviousSelectedSwitch { get => previousSelectedSwitch; set => previousSelectedSwitch = value; }


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

            delay += gameTime.ElapsedGameTime.Milliseconds;

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            //Checks if the mouseRectangle intersects with a button's Rectangle. 
            if (mouseRectangle.Intersects(Rectangle))
            {

                //while hovering over a button, it checks whether you click it 
                //(and release the mouse button while still inside the button's rectangle)
                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    GameWorld.Instance.drawSelectedAdventurer = false;
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
                    GameWorld.Instance.drawSelectedAdventurer = false;

                    selected = true;
                    SelectedID = id;
                    AnySelected = true;

                    //If the item is in the shop
                    if (GameWorld.Instance.shop.Contains(this) && GameWorld.Instance.gold >= GoldCost)
                    {
                        GameWorld.Instance.gold -= GoldCost;
                        Controller.Instance.UpdateStats();
                        if (this.GetType() == typeof(Equipment))
                        {
                            GameWorld.Instance.inventoryList.Add(Controller.Instance.CreateEquipment(name, type, type, rarity, goldCost, skillRating, false)); //Adds the equipment to the database
                        }
                        else if (this.GetType() == typeof(Consumable))
                        {
                            GameWorld.Instance.inventoryList.Add(Controller.Instance.CreateConsumable(name, type, type, rarity, goldCost, skillRating, false, GameWorld.Instance.GenerateRandom(1, 4))); //Adds the consumable to the database
                        }
                        GameWorld.Instance.boughtItems.Add(this); //Removes the item from the shop
                        selected = false;
                        AnySelected = false;
                    }
                }

                if (currentMouse.RightButton == ButtonState.Released && previousMouse.RightButton == ButtonState.Pressed)
                {
                    foreach (Button adventurer in GameWorld.Instance.adventurerButtons)
                    {
                        if (adventurer.selected)
                        {
                            Adventurer A = GameWorld.Instance.adventurersDic[adventurer.Id];

                            if (type == "Helmet")
                            {
                                if (A.Helmet.GoldCost != 0)
                                {
                                    GameWorld.Instance.toBeAddedItem.Add(A.Helmet);
                                }
                                A.Helmet = (Equipment)this;
                                A.Helmet.IsEquipped = true;
                                A.HelmetFrame.Rarity = this.Rarity;
                                GameWorld.Instance.toBeRemovedItem.Add(this);

                                Controller.Instance.UpdateAdventurerHelmet(A.Helmet.id);
                                Controller.Instance.EquipEquipment(A.Helmet.id);
                            }

                            if (type == "Weapon")
                            {
                                if (A.Weapon.GoldCost != 0)
                                {
                                    GameWorld.Instance.toBeAddedItem.Add(A.Weapon);
                                }
                                A.Weapon = (Equipment)this;
                                A.WeaponFrame.Rarity = this.Rarity;
                                A.Weapon.IsEquipped = true;
                                GameWorld.Instance.toBeRemovedItem.Add(this);

                                // update the database
                                Controller.Instance.UpdateAdventurerWeapon(A.Weapon.Id);
                                Controller.Instance.EquipEquipment(A.Weapon.Id);
                            }

                            if (type == "Boot")
                            {
                                if (A.Boot.GoldCost != 0)
                                {
                                    GameWorld.Instance.toBeAddedItem.Add(A.Boot);
                                }
                                A.Boot = (Equipment)this;
                                A.BootFrame.Rarity = this.Rarity;
                                A.Boot.IsEquipped = true;
                                GameWorld.Instance.toBeRemovedItem.Add(this);

                                Controller.Instance.UpdateAdventurerBoot(A.Boot.id);
                                Controller.Instance.EquipEquipment(A.Boot.Id);
                            }

                            if (type == "Chest")
                            {
                                if (A.Chest.GoldCost != 0)
                                {
                                    GameWorld.Instance.toBeAddedItem.Add(A.Chest);
                                }
                                A.Chest = (Equipment)this;
                                A.ChestFrame.Rarity = this.Rarity;
                                A.Chest.IsEquipped = true;
                                GameWorld.Instance.toBeRemovedItem.Add(this);

                                Controller.Instance.UpdateAdventurerChest(A.Chest.id);
                                Controller.Instance.EquipEquipment(A.Chest.Id);
                            }

                            if (type == "Potion")
                            {
                                A.Consumable = (Consumable)this;
                                //A.ConsumableFrame.Rarity = this.Rarity;
                                A.Consumable.IsEquipped = true;
                                GameWorld.Instance.toBeRemovedItem.Add(this);

                                Controller.Instance.UpdateAdventurerConsumeable(A.Consumable.id);
                                Controller.Instance.EquipConsumable(A.Consumable.Id);
                            }



                            A.Weapon.Position = A.Position + new Vector2(150, 0);
                            A.Helmet.Position = A.Position + new Vector2(300, 0);
                            A.Chest.Position = A.Position + new Vector2(450, 0);
                            A.Boot.Position = A.Position + new Vector2(600, 0);

                            delay = 0;
                        }
                    }
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
                tempItemType = "Helmet";
            }

            else if (tempItemTypeGenerate == 3)
            {
                tempItemType = "Boot";
            }

            //Generates a name using the naming database
            string tempName = $"{Controller.Instance.GetName(38, 100)} {tempItemType} of {Controller.Instance.GetName(1, 39)}";

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

            switch (Rarity)
            {
                case "Common":
                    RarityColor = Color.White;
                    break;
                case "Uncommon":
                    RarityColor = Color.Green;
                    break;
                case "Rare":
                    RarityColor = Color.Blue;
                    break;
                case "Epic":
                    RarityColor = Color.Purple;
                    break;
                case "Legendary":
                    RarityColor = Color.Orange;
                    break;
            }

            spriteBatch.DrawString(GameWorld.Instance.fontCopperplate, $"{Name}", Position + new Vector2(100, 0), RarityColor);
            spriteBatch.DrawString(GameWorld.Instance.fontCopperplate, $"Cost: {GoldCost}", Position + new Vector2(100, 35), Color.Gold);
            spriteBatch.DrawString(GameWorld.Instance.fontCopperplate, $"SkillRating: {SkillRating}", Position + new Vector2(100, 70), Color.White);
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
                    GameWorld.Instance.gold += (GameWorld.Instance.inventoryList[i].GoldCost/3); //Adds the sells price to the players gold
                    Controller.Instance.UpdateStats(); //Updates the gold
                    if (GameWorld.Instance.inventoryList[i].GetType() == typeof(Equipment)) //Checks for equipment
                    {
                        Controller.Instance.SellEquipement(GameWorld.Instance.inventoryList[i].Id); //Deletes the item from the database
                    }
                    else if (GameWorld.Instance.inventoryList[i].GetType() == typeof(Consumable)) //Checks for consumable
                    {
                        Controller.Instance.SellConsumable(GameWorld.Instance.inventoryList[i].Id); //Deletes the item from the database
                    }
                    GameWorld.Instance.toBeRemovedItem.Add(GameWorld.Instance.inventoryList[i]); //Adds the item to the list toBeRemoved
                }
            }
            foreach (var item in GameWorld.Instance.inventoryFrameList)
            {
                item.Rarity = "Common";
            }
            AnySelected = false;
        }
    }
}
