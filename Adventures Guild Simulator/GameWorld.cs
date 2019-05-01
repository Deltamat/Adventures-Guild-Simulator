using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Adventures_Guild_Simulator
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {
        public static Random rng = new Random();

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private List<GameObject> userInterfaceObjects = new List<GameObject>();
        public List<GameObject> UI = new List<GameObject>();
        public List<GameObject> inventoryFrameList = new List<GameObject>();
        public List<GameObject> adventurerButtons = new List<GameObject>();
        public List<Item> inventoryList = new List<Item>();
        public List<Item> toBeRemovedItem = new List<Item>();
        public List<Item> toBeAddedItem = new List<Item>();
        public List<Item> itemList = new List<Item>();
        public List<Item> shop = new List<Item>();
        public List<Item> boughtItems = new List<Item>();
        public List<Quest> quests = new List<Quest>();
        public List<Quest> questsToBeRemoved = new List<Quest>();
        public List<string> infoScreen = new List<string>();
        public Dictionary<int, Equipment> equipmentDic = new Dictionary<int, Equipment>();
        public Dictionary<int, Consumable> consumableDic = new Dictionary<int, Consumable>();
        public Dictionary<int, Adventurer> adventurersDic;

        private float delay = 0;
        public int inventoryRowList;
        public int gold;
        public int adventurerDeaths;
        public int adventurerToShowId;
        public int questsCompleted;
        public double globalDeltaTime;
        public bool questSelected;
        public bool drawSelectedAdventurer;

        public SpriteFont font;
        public SpriteFont fontCopperplate;
        private Texture2D skullSprite;
        private Texture2D goldSprite;
        private Texture2D questionMarkSprite;
        private Button sellAdventurerButton;
        private Button resetButton;
        private Button sellItemButton;
        private Button restockShop;

        private static ContentManager content;
        public static ContentManager ContentManager
        {
            get
            {
                return content;
            }
        }

        static GameWorld instance;
        static public GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        /// <summary>
        /// Creates a rectangle whithin the bounds of the window
        /// </summary>
        public Rectangle ScreenSize
        {
            get
            {
                return graphics.GraphicsDevice.Viewport.Bounds;
            }
        }

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
            
            //Sets the window size
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;            
            //graphics.IsFullScreen = true;

            graphics.ApplyChanges();
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            equipmentDic = Controller.Instance.LoadEquipment();
            consumableDic = Controller.Instance.LoadConsumable();
            adventurersDic = Controller.Instance.LoadAdventurers();
            gold = Controller.Instance.LoadGold();
            adventurerDeaths = Controller.Instance.LoadDeaths();
            questsCompleted = Controller.Instance.LoadCompletedQuests();

            //UI
            UI.Add(new GameObject(Vector2.Zero, "boardBackground"));
            UI.Add(new GameObject(new Vector2(10), "questShop"));
            UI.Add(new GameObject(new Vector2(10, 520), "questShop"));
            UI.Add(new GameObject(new Vector2(565, 10), "statPlank"));
            UI.Add(new GameObject(new Vector2(565, 100), "infoChar"));
            UI.Add(new GameObject(new Vector2(565, 560), "infoChar"));
            UI.Add(new GameObject(new Vector2(1370, 10), "inventory"));

            Inventory.GenerateInventoryFrames();

            //Adds all equipment list items to the inventory
            foreach (var item in equipmentDic)
            {
                if (item.Value.IsEquipped == false)
                {                    
                    inventoryList.Add(item.Value);
                }
            }

            //Adds all consumable list items to the inventory
            foreach (var item in consumableDic)
            {
                if (item.Value.IsEquipped == false)
                {
                    inventoryList.Add(item.Value);
                }
            }


            Controller.Instance.Naming();

            base.Initialize();
        }        

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            fontCopperplate = Content.Load<SpriteFont>("fontCopperplate");
            goldSprite = Content.Load<Texture2D>("gold"); //Gold sprite
            skullSprite = Content.Load<Texture2D>("skull"); //Skull sprite
            questionMarkSprite = Content.Load<Texture2D>("quest_complete"); //Question mark sprite
            UpdateAdventurerButtons();

            //Buttons
            var buyAdventurerButton = new Button(content.Load<Texture2D>("AB"), content.Load<SpriteFont>("fontCopperplate"), new Vector2(950, 1020), "AB")
            {
                TextForButton = "Buy adv.",
                FontColor = Color.White
            };
            sellAdventurerButton = new Button(content.Load<Texture2D>("AB"), content.Load<SpriteFont>("fontCopperplate"), new Vector2(1230, 500), "AB")
            {
                TextForButton = "Sell adv.",
            };
            resetButton = new Button(content.Load<Texture2D>("AB"), content.Load<SpriteFont>("fontCopperplate"), new Vector2(1800, 1020), "AB")
            {
                TextForButton = "Reset",
            };
            sellItemButton = new Button(content.Load<Texture2D>("AB"), content.Load<SpriteFont>("fontCopperplate"), new Vector2(1230, 500), "AB")
            {
                TextForButton = "Sell item"
            };
            restockShop = new Button(content.Load<Texture2D>("AB"), content.Load<SpriteFont>("fontCopperplate"), new Vector2(280, 495), "AB")
            {
                TextForButton = "Restock (50)"
            };

            //sets a click event for each Button
            buyAdventurerButton.Click += BuyAdventurer;
            sellAdventurerButton.Click += SellAdventurer;
            resetButton.Click += Reset;
            sellItemButton.Click += SellItemCall;
            restockShop.Click += RestockShop;

            //List of our buttons
            userInterfaceObjects = new List<GameObject>()
            {
                buyAdventurerButton,
                resetButton
            };
            userInterfaceObjects.Add(buyAdventurerButton);

            font = Content.Load<SpriteFont>("font");
        }

        /// <summary>
        /// Looks for the click event for the button which this event was added to.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuyAdventurer(object sender, EventArgs e)
        {
            if (gold >= 20)
            {
                if (adventurersDic.Count >= 27)
                {
                    return;
                }
                Adventurer a = Controller.Instance.CreateAdventurer(Controller.Instance.GetName(100, 120));
                adventurersDic.Add(a.Id, a);
                drawSelectedAdventurer = false;
                UpdateAdventurerButtons();
                gold -= 20;
            }            
        }

        /// <summary>
        /// Looks for the click event for the button which this event was added to.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowQuestInfo(object sender, EventArgs e)
        {
            foreach (Quest quest in quests)
            {
                quest.selected = false;
            }
            infoScreen.Clear();
            infoScreen.Add("Select an adventurer to send on this quest!");
            questSelected = true;
            foreach (Button item in adventurerButtons)
            {
                item.selected = false;
            }
            foreach (Item item in inventoryList)
            {
                item.selected = false;
            }
            drawSelectedAdventurer = false;
            Item.AnySelected = false;
        }

        /// <summary>
        /// Looks for the click event for the button which this event was added to.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SellAdventurer(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            gold += adventurersDic[adventurerToShowId].Level;

            if (adventurersDic[adventurerToShowId].Weapon.GoldCost > 1)
            {
                adventurersDic[adventurerToShowId].Weapon.IsEquipped = false;
                Controller.Instance.UnequipEquipment(adventurersDic[adventurerToShowId].Weapon.Id);
                inventoryList.Add(adventurersDic[adventurerToShowId].Weapon);
            }
            if (adventurersDic[adventurerToShowId].Chest.GoldCost > 1)
            {
                adventurersDic[adventurerToShowId].Chest.IsEquipped = false;
                Controller.Instance.UnequipEquipment(adventurersDic[adventurerToShowId].Chest.Id);
                inventoryList.Add(adventurersDic[adventurerToShowId].Chest);
            }
            if (adventurersDic[adventurerToShowId].Helmet.GoldCost > 1)
            {
                adventurersDic[adventurerToShowId].Helmet.IsEquipped = false;
                Controller.Instance.UnequipEquipment(adventurersDic[adventurerToShowId].Helmet.Id);
                inventoryList.Add(adventurersDic[adventurerToShowId].Helmet);
            }
            if (adventurersDic[adventurerToShowId].Boot.GoldCost > 1)
            {
                adventurersDic[adventurerToShowId].Boot.IsEquipped = false;
                Controller.Instance.UnequipEquipment(adventurersDic[adventurerToShowId].Boot.Id);
                inventoryList.Add(adventurersDic[adventurerToShowId].Boot);
            }
            Controller.Instance.RemoveAdventurer(adventurerToShowId);
            adventurersDic.Remove(adventurerToShowId);
            UpdateAdventurerButtons();
            drawSelectedAdventurer = false;
        }

        /// <summary>
        /// Calls SellItem method from Item class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SellItemCall(object sender, EventArgs e)
        {
            Item.SellItem();
        }

        /// <summary>
        /// Deletes everything currently in the shop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RestockShop(object sender, EventArgs e)
        {
            if (gold >= 50)
            {
                gold -= 50;
                shop.Clear();
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            delay += gameTime.ElapsedGameTime.Milliseconds;
            globalDeltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            resetButton.Update(gameTime);
            restockShop.Update(gameTime);

            equipmentDic = Controller.Instance.LoadEquipment();            

            //If there are less than 5 quests, generate a new one
            while (quests.Count < 5)
            {
                Quest quest = new Quest();
                quests.Add(quest);
                quest.Click += ShowQuestInfo;
            }
            //Updates quests
            foreach (Quest quest in quests)
            {
                quest.Update(gameTime);
            }
            //Removes expired/completed quests
            foreach (Quest quest in questsToBeRemoved)
            {
                quests.Remove(quest);
            }
            questsToBeRemoved.Clear();

            //updates our click-events for the UI
            foreach (GameObject ui in userInterfaceObjects)
            {
                ui.Update(gameTime);
            }

            foreach (Item item in inventoryList)
            {
                item.Update(gameTime);
            }

            foreach (Item item in toBeAddedItem)
            {
                inventoryList.Add(item);
            }
            toBeAddedItem.Clear();

            foreach (Item item in toBeRemovedItem)
            {
                inventoryList.Remove(item);
            }
            toBeRemovedItem.Clear();

            //updates the adventurer buttons, it is its own list because the list have to be emptied sometimes
            foreach (GameObject button in adventurerButtons)
            {
                button.Update(gameTime);
            }

            foreach (Adventurer adventurer in adventurersDic.Values)
            {
                adventurer.Update(gameTime);
            }

            // updates the sell button
            if (drawSelectedAdventurer == true)
            {
                sellAdventurerButton.Update(gameTime);
            }

            if (Item.AnySelected == true)
            {
                sellItemButton.Update(gameTime);
            }            

            //Makes sure the inventory amount isn't exceeded
            while (inventoryList.Count > 28)
            {
                inventoryList.Remove(inventoryList[28]);
            }

            for (int i = 0; i < inventoryFrameList.Count; i++)
            {
                if (i <= inventoryList.Count - 1)
                {
                    inventoryFrameList[i].Rarity = inventoryList[i].Rarity;
                }
                else
                {
                    inventoryFrameList[i].Rarity = "Common";
                }
            }

            for (int i = 0; i < inventoryList.Count; i++)
            {
                inventoryList[i].Position = inventoryFrameList[i].Position + new Vector2(10, 10);
            }

            //Shop
            while (shop.Count < 4)
            {
                int rng = GenerateRandom(0, 5);
                if (rng == 0)
                {
                    shop.Add(Consumable.ReturnConsumable(Vector2.Zero));
                }
                else
                {
                    shop.Add(Equipment.ReturnEquipment(Vector2.Zero));
                }
            }
            foreach (Item item in shop)
            {
                item.Update(gameTime);
            }
            foreach (Item item in boughtItems)
            {
                shop.Remove(item);
            }

            if (Mouse.GetState().MiddleButton == ButtonState.Pressed)
            {
                foreach (Quest quest in quests)
                {
                    quest.selected = false;
                }
                foreach (Button adventurer in adventurerButtons)
                {
                    adventurer.selected = false;
                }
                foreach (Item item in inventoryList)
                {
                    item.selected = false;
                }
                infoScreen.Clear();
                drawSelectedAdventurer = false;
                questSelected = false;
                Item.AnySelected = false;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkBlue);
            spriteBatch.Begin();

            
            //Draws backgrounds for the UI
            foreach (GameObject UIelement in UI)
            {
                UIelement.Draw(spriteBatch);
            }

            //Stats
            spriteBatch.Draw(goldSprite, new Vector2(590, 25), Color.White);
            spriteBatch.DrawString(fontCopperplate, $"{gold}", new Vector2(650, 35), Color.Gold);
            spriteBatch.Draw(questionMarkSprite, new Vector2(900, 25), Color.White);
            spriteBatch.DrawString(fontCopperplate, $"{questsCompleted}", new Vector2(960, 35), Color.Orange);
            spriteBatch.Draw(skullSprite, new Vector2(1210, 25), Color.White);
            spriteBatch.DrawString(fontCopperplate, $"{adventurerDeaths}", new Vector2(1270, 35), Color.Beige);

            // Draws the selected adventurer info
            Adventurer value;
            if (drawSelectedAdventurer == true)
            {
                if (adventurersDic.TryGetValue(adventurerToShowId, out value))
                {
                    spriteBatch.DrawString(fontCopperplate, value.Name, new Vector2(750, 325), Color.White); // name
                    spriteBatch.Draw(value.Sprite, value.Position + new Vector2(-40, 125), Color.White); // icon                    
                    spriteBatch.DrawString(fontCopperplate, $"lvl: {value.Level}", new Vector2(750, 375), Color.White);
                    spriteBatch.DrawString(fontCopperplate, $"SR: {value.Skill}", new Vector2(750, 425), Color.White);

                    if (value.Helmet != null)
                    {
                        spriteBatch.Draw(value.Helmet.Sprite, value.Helmet.CollisionBox, Color.White);
                        spriteBatch.DrawString(fontCopperplate, $"SR:{value.Helmet.SkillRating}", value.Helmet.Position + new Vector2(-50, 70), Color.White);
                    }
                    if (value.Chest != null)
                    {
                        spriteBatch.Draw(value.Chest.Sprite, value.Chest.CollisionBox, Color.White);
                        spriteBatch.DrawString(fontCopperplate, $"SR:{value.Chest.SkillRating}", value.Chest.Position + new Vector2(-50, 70), Color.White);
                    }
                    if (value.Weapon != null)
                    {
                        spriteBatch.Draw(value.Weapon.Sprite, value.Weapon.CollisionBox, Color.White);
                        spriteBatch.DrawString(fontCopperplate, $"SR:{value.Weapon.SkillRating}", value.Weapon.Position + new Vector2(-50, 70), Color.White);
                    }
                    if (value.Boot != null)
                    {
                        spriteBatch.Draw(value.Boot.Sprite, value.Boot.CollisionBox, Color.White);
                        spriteBatch.DrawString(fontCopperplate, $"SR:{value.Boot.SkillRating}", value.Boot.Position + new Vector2(-50, 70), Color.White);
                    }
                    if (value.Consumable != null)
                    {
                        spriteBatch.Draw(value.Consumable.Sprite, value.Consumable.CollisionBox, Color.White);
                        spriteBatch.DrawString(fontCopperplate, $"SR:{value.Consumable.SkillRating}", value.Consumable.Position + new Vector2(-50, 70), Color.White);
                    }

                    if (value.BootFrame != null)
                    {
                        value.BootFrame.Draw(spriteBatch, true);
                    }

                    if (value.WeaponFrame != null)
                    {
                        value.WeaponFrame.Draw(spriteBatch, true);
                    }

                    if (value.ChestFrame != null)
                    {
                        value.ChestFrame.Draw(spriteBatch, true);
                    }
          
                    if (value.HelmetFrame != null)
                    {
                        value.HelmetFrame.Draw(spriteBatch, true);
                    }

                    if (value.ConsumableFrame != null)
                    {
                        value.ConsumableFrame.Draw(spriteBatch, true);
                    }
                }

                //draws the sell adventurer button
                if (drawSelectedAdventurer is true)
                {
                    sellAdventurerButton.Draw(spriteBatch);
                }
            }

            if (Item.AnySelected == true)
            {
                sellItemButton.Draw(spriteBatch);
            }

            foreach (Item item in itemList)
            {
                item.Draw(spriteBatch);
            }

            //Draws all the buttons of the UI
            foreach (GameObject ui in userInterfaceObjects)
            {
                ui.Draw(spriteBatch);
            }

            foreach (GameObject button in adventurerButtons)
            {
                button.Draw(spriteBatch);
            }

            restockShop.Draw(spriteBatch);

            //Draws shop
            Vector2 shopVector = new Vector2(30);
            foreach (Item item in shop)
            {
                item.Position = shopVector;
                item.Draw(spriteBatch);
                shopVector.Y += 110;
            }

            //Draws quests
            int drawQuestVector = 540;
            foreach (Quest quest in quests)
            {
                quest.Position = new Vector2(30, drawQuestVector);
                quest.Draw(spriteBatch);
                spriteBatch.DrawString(fontCopperplate, $"{quest.Enemy}", new Vector2(50, drawQuestVector + 25), Color.Cornsilk); //Writes which enemy is on this quest
                spriteBatch.DrawString(fontCopperplate, $"{quest.DifficultyRating}", new Vector2(275, drawQuestVector + 25), Color.Red); //Writes how difficult the quest is
                spriteBatch.DrawString(fontCopperplate, $"{quest.Reward}", new Vector2(375, drawQuestVector + 25), Color.Gold); //Writes how much gold the reward is on
                if (quest.Ongoing == false) //If the quest is NOT under way
                {
                    spriteBatch.DrawString(fontCopperplate, $"{quest.ExpireTime - Math.Round(quest.TimeToExpire, 0)}", new Vector2(475, drawQuestVector + 25), Color.MistyRose); //Writes the countdown timer
                }
                else if (quest.Ongoing == true) //If the quest is under way
                {
                    spriteBatch.DrawString(fontCopperplate, $"{quest.DurationTime - Math.Round(quest.ProgressTime, 0)}", new Vector2(475, drawQuestVector + 25), Color.Turquoise); //Writes the progression timer
                }
                drawQuestVector += 90; //Moves the next quest down by a margin
            }

            foreach (GameObject item in inventoryFrameList)
            {
                item.Draw(spriteBatch,true);
            }

            for (int i = 0; i < inventoryList.Count; i++)
            {
                inventoryList[i].Draw(spriteBatch, inventoryList[i].Position);
            }

            foreach (Item item in itemList)
            {
                item.Draw(spriteBatch);
            }

            //Writes to info screen
            Vector2 infoScreenVector = new Vector2(600, 120); //Top left of info screen
            foreach (string String in infoScreen)
            {
                spriteBatch.DrawString(fontCopperplate, String, new Vector2(infoScreenVector.X, infoScreenVector.Y), Color.White); //Writes string
                infoScreenVector.Y += 25; //Moves the next string down by a margin
            }

            //foreach (Item item in inventoryList)
            //{
            //    if (item.selected == true)
            //    {
            //            spriteBatch.DrawString(fontCopperplate, $"{item.Name}", new Vector2(800, 130), item.RarityColor);
            //            spriteBatch.DrawString(fontCopperplate, $"Cost: {item.GoldCost}", new Vector2(600, 230), Color.Gold);
            //            spriteBatch.DrawString(fontCopperplate, $"SkillRating: {item.SkillRating}", new Vector2(800, 230), Color.White);
            //    }
            //}

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Generates a random number between min- and maxValue
        /// </summary>
        /// <param name="minValue">Minimum value</param>
        /// <param name="maxValue">Maximum value</param>
        /// <returns></returns>
        public int GenerateRandom(int minValue, int maxValue)
        {
            int value = rng.Next(minValue, maxValue);
            return value;
        }

        /// <summary>
        /// Updates the buttons for the adventurers, if an adventurer dies this gets called etc.
        /// </summary>
        public void UpdateAdventurerButtons()
        {
            adventurerButtons.RemoveRange(0, adventurerButtons.Count);
            int i = 0;
            int line = 0;
            foreach (var item in adventurersDic)
            {
                Button AdventurerButton = new Button(content.Load<Texture2D>("AB"), content.Load<SpriteFont>("fontCopperplate"), new Vector2(700 + line * 250, 600 + i * 45), "AB")
                {
                    TextForButton = $"{item.Value.Name}",
                    FontColor = Color.White,
                    Id = item.Value.Id,
                    questActive = item.Value.OnQuest
                };

                adventurerButtons.Add(AdventurerButton);

                i++;
                if (i == 9)
                {
                    line++;
                    i = 0;
                }
                AdventurerButton.Click += AdventurerButtonClickEvent;
            }
        }

        /// <summary>
        /// The clickevent for the adventurer buttons.
        /// selects the adventurer the button belongs to. 
        /// also handles the code that sends the adventurer on a quest.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdventurerButtonClickEvent(object sender, EventArgs e)
        {            
            Button button = (Button)sender;
            if (adventurersDic[button.Id].OnQuest == false)
            {
                drawSelectedAdventurer = true;
                adventurerToShowId = button.Id;
                foreach (Button advButton in adventurerButtons)
                {
                    advButton.selected = false;
                }
                button.selected = true;
                foreach (Item item in inventoryList)
                {
                    item.selected = false;
                }

                Adventurer adventurer = null;
                if (questSelected == true)
                {
                    foreach (Button item in adventurerButtons)
                    {
                        if (item.selected == true)
                        {
                            adventurer = adventurersDic[item.Id];
                            adventurer.OnQuest = true;
                            button.questActive = true;
                            button.selected = false;
                            drawSelectedAdventurer = false;
                            infoScreen.Clear();
                        }
                    }
                    foreach (Quest item in quests)
                    {
                        if (item.selected == true)
                        {
                            item.assignedAdventurer = adventurer;
                            item.Ongoing = true;
                            item.selected = false;
                            questSelected = false;
                            infoScreen.Clear();
                        }
                    }                    
                }
                Item.AnySelected = false;
            }            
        }

        private void Reset(object sender, EventArgs e)
        {
            gold = 5000;
            questsCompleted = 0;
            adventurerDeaths = 0;
            inventoryList = new List<Item>();
            quests = new List<Quest>();
            drawSelectedAdventurer = false;
            foreach (GameObject item in inventoryFrameList)
            {
                item.Rarity = "Common";
            }

            Controller.Instance.Reset();

            equipmentDic = Controller.Instance.LoadEquipment();
            consumableDic = Controller.Instance.LoadConsumable();
            adventurersDic = Controller.Instance.LoadAdventurers();
            gold = Controller.Instance.LoadGold();
            adventurerDeaths = Controller.Instance.LoadDeaths();
            questsCompleted = Controller.Instance.LoadCompletedQuests();

            UpdateAdventurerButtons();
        }
    }
}
