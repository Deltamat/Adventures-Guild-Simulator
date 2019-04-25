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

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public List<GameObject> UI = new List<GameObject>();

        public static SpriteFont font;
        public SpriteFont fontCopperplate;
        private List<GameObject> userInterfaceObjects = new List<GameObject>();
        public static List<Item> itemList = new List<Item>(); //Temporary
        public double globalDeltaTime;
        public List<Quest> quests = new List<Quest>();
        public List<Quest> questsToBeRemoved = new List<Quest>();
        public int gold;
        public List<Adventurer> adventurers;
        float delay = 0;

        //public List<string> 

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
            adventurers = Controller.Instance.LoadAdventurers();
            gold = Controller.Instance.LoadGold();

            //UI
            UI.Add(new GameObject(Vector2.Zero, "boardBackground"));
            UI.Add(new GameObject(new Vector2(10), "questShop"));
            UI.Add(new GameObject(new Vector2(10, 520), "questShop"));
            UI.Add(new GameObject(new Vector2(565, 10), "statPlank"));
            UI.Add(new GameObject(new Vector2(565, 100), "infoChar"));
            UI.Add(new GameObject(new Vector2(565, 560), "infoChar"));
            UI.Add(new GameObject(new Vector2(1370, 10), "inventory"));

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
            //fontCopperplate = Content.Load<SpriteFont>("fontCopperplate");



            //Buttons
            var testButton = new Button(content.Load<Texture2D>("Button"), content.Load<SpriteFont>("Font"), new Vector2((int)(ScreenSize.Width - ScreenSize.Center.X - 100), (int)(ScreenSize.Height - ScreenSize.Center.Y - 20)), "Button")
            {
                TextForButton = "test",
            };

            UpdateAdventurerButtons();

            //sets a click event for each Button
            testButton.Click += TestButtonClickEvent;

            //List of our buttons
            userInterfaceObjects = new List<GameObject>()
            {
                testButton,                
            };
            userInterfaceObjects.Add(testButton);


            font = Content.Load<SpriteFont>("font");
        }

        /// <summary>
        /// Looks for the click event for the button which this event was added to.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestButtonClickEvent(object sender, EventArgs e)
        {
           //something
        }

        private void ShowQuestInfo(object sender, EventArgs e)
        {
            
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
            foreach (var item in userInterfaceObjects)
            {
                item.Update(gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                //adventurers.Add(Controller.Instance.CreateAdventurer("Gert"));
                //UpdateAdventurerButtons();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.E) && delay > 2000)
            {
                Item.GenerateItem(new Vector2(300, 200));
                Item.GenerateItem(new Vector2(300, 350));
                Item.GenerateItem(new Vector2(300, 500));
                Item.GenerateItem(new Vector2(300, 650));
                Item.GenerateItem(new Vector2(300, 800));
                delay = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.C) && delay > 2000)
            {
                itemList.Clear();
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

            foreach (var item in itemList)
            {
                item.Draw(spriteBatch);
            }

            //Draws all the buttons of the UI
            foreach (var item in userInterfaceObjects)
            {
                item.Draw(spriteBatch);
            }

            //Draws quests
            int drawQuestVector = 540;
            foreach (Quest quest in quests)
            {
                quest.Position = new Vector2(30, drawQuestVector);
                quest.Draw(spriteBatch);
                //spriteBatch.DrawString(fontCopperplate, $"{quest.Enemy}", new Vector2(50, drawQuestVector + 25), Color.Cornsilk); //Writes which enemy is on this quest
                //spriteBatch.DrawString(fontCopperplate, $"{quest.DifficultyRating}", new Vector2(275, drawQuestVector + 25), Color.DarkOrange); //Writes how difficult the quest is
                //spriteBatch.DrawString(fontCopperplate, $"{quest.Reward}", new Vector2(375, drawQuestVector + 25), Color.Gold); //Writes how much gold the reward is on
                //if (quest.Ongoing == false) //If the quest is NOT under way
                //{
                //    spriteBatch.DrawString(fontCopperplate, $"{quest.ExpireTime - Math.Round(quest.TimeToExpire, 0)}", new Vector2(475, drawQuestVector + 25), Color.MistyRose); //Writes the countdown timer
                //}
                //else if (quest.Ongoing == true) //If the quest is under way
                //{
                //    spriteBatch.DrawString(fontCopperplate, $"{quest.DurationTime - Math.Round(quest.ProgressTime, 0)}", new Vector2(475, drawQuestVector + 25), Color.Turquoise); //Writes the progression timer
                //}
                drawQuestVector += 90; //Moves the next quest down by a margin
            }           

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public int GenerateRandom(int minValue, int maxValue)
        {
            int value = rng.Next(minValue, maxValue);
            return value;
        }

        private void UpdateAdventurerButtons()
        {
            int i = 0;
            int line = 0;
            foreach (var item in adventurers)
            {
                var AdventurerButton = new Button(content.Load<Texture2D>("AB"), content.Load<SpriteFont>("Font"), new Vector2(700 + line * 250, 600 + i * 45), "AB")
                {
                    TextForButton = $"{item.Name} LvL: {item.Level}",
                    FontColor = Color.White
                };
                userInterfaceObjects.Add(AdventurerButton);
                i++;
                if (i == 9)
                {
                    line++;
                    i = 0;
                }
                AdventurerButton.Click += AdventurerButtonClickEvent;
            }
        }

        private void AdventurerButtonClickEvent(object sender, EventArgs e)
        {

        }
    }
}
