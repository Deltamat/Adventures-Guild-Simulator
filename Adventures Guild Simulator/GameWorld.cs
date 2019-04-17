using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Adventures_Guild_Simulator
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {
        //YEA BOI
        //KIllROY was here
        ModelAdventurer m; // midlertidig
        string name;
        int number = 1;
        double counter;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        private List<GameObject> userInterfaceObjects;

        private static ContentManager content;
        public static ContentManager ContentManager
        {
            get
            {
                return content;
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
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
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
            

            //midlertidig
            m = new ModelAdventurer();
            name = m.GetNameByID(1);

            // TODO: use this.Content to load your game content here

            //Buttons
            var testButton = new Button(content.Load<Texture2D>("Button"), content.Load<SpriteFont>("Font"), new Vector2((int)(ScreenSize.Width - ScreenSize.Center.X - 100), (int)(ScreenSize.Height - ScreenSize.Center.Y - 20)), "Button")
            {
                TextForButton = "test",
            };
            
            //sets a click event for each Button
            testButton.Click += TestButtonClickEvent;

            //List of our buttons
            userInterfaceObjects = new List<GameObject>()
            {
                testButton,
                
            };

            font = Content.Load<SpriteFont>("font");
        }
        /// <summary>
        /// Looks for the click event for the button which this event was added to.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestButtonClickEvent(object sender, EventArgs e)
        {
           //noget

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
            
            //updates our click-events for the UI
            foreach (var item in userInterfaceObjects)
            {
                item.Update(gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                m.CreateAdventurer("Gert");
            }

            counter += gameTime.ElapsedGameTime.TotalSeconds;
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && counter > 1)
            {
                number++;
                counter = 0;
                name = m.GetNameByID(number);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && counter > 1)
            {
                number--;
                counter = 0;
                name = m.GetNameByID(number);
            }
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            
            //Draws all the buttons of the UI
            foreach (var item in userInterfaceObjects)
            {
                item.Draw(spriteBatch);
            }

            spriteBatch.DrawString(font, $"Name: {name}, Level: {m.GetLevelByID(number)}", new Vector2(50), Color.White);

            spriteBatch.End();
            // TODO: Add your drawing code here

            
            base.Draw(gameTime);
        }
    }
}
