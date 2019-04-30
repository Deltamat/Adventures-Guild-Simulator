using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    /// <summary>
    /// This class is for creating buttons, with methods to see if the mouse is hovering over a certain button, 
    /// as well as if we are clicking the actual button, 
    /// which creates an "event" that we can use in other classes.
    /// </summary>
    class Button : GameObject
    {
        #region Fields

        private MouseState currentMouse;
        private SpriteFont font;
        private bool isHovering;
        private MouseState previousMouse;
        private Texture2D texture;
        private Vector2 positionButton;
        public bool selected = false;
        public Color color;
        public bool questActive;
        #endregion

        #region Properties

        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Color FontColor { get; set; }
        public int Id { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                //return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
                return new Rectangle((int)(Position.X - Sprite.Width * 0.5), (int)(Position.Y - texture.Height * 0.5), texture.Width, texture.Height);
            }
        }

        public string TextForButton { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Method used to create a Button. The color of the font is hardcoded to black.
        /// </summary>
        /// <param name="texture">loads the image as texture for the Button</param>
        /// <param name="font">loads the font</param>
        public Button(Texture2D texture, SpriteFont font, Vector2 position, string spriteName) : base(position, spriteName)
        {
            this.texture = texture;
            this.font = font;
            this.positionButton = position;
            FontColor = Color.Black;
            color = Color.White;
        }

        /// <summary>
        /// This is where it checks if the mouse is hovering over the button or not. 
        /// And then checks if you pressed and released the left button on your mouse.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //"Inception"
            previousMouse = currentMouse;
            //Gets current position and "click info" from the mouse
            currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            //Makes sure that the "hovering state" disappears from any button you previously hovered over with the mouse
            isHovering = false;

            //Checks if the mouseRectangle intersects with a button's Rectangle. 
            if (mouseRectangle.Intersects(Rectangle))
            {
                //Just to tell the draw method to tint the button gray.
                isHovering = true;

                //while hovering over a button, it checks whether you click it 
                //(and release the mouse button while still inside the button's rectangle)
                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Makes the color of the button gray if the mouse is hovering over it,
        /// and draws the text for the button, only if it hasn't already done so.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {            
            //Pretty selfexplanatory, but it changes the button's color to gray from  
            //white if you hover over it with the mouse.
            if (isHovering || selected)
            {
                color = Color.Gray;
            }
            else if (questActive)
            {
                color = Color.Red;
            }
            else
            {
                color = Color.White;
            }
            //Draws the button, without text.
            spriteBatch.Draw(texture, Rectangle, color);

            //If there's no text for the button, then it writes the text in the middle of the button via 
            //some simple math that calculates where the vector for the string should be.
            if (!string.IsNullOrEmpty(TextForButton))
            {
                //The simple calculations to determine where the text should be drawn on the button.
                var x = (Rectangle.X + (Rectangle.Width * 0.5f)) - (font.MeasureString(TextForButton).X * 0.5f);
                var y = (Rectangle.Y + (Rectangle.Height * 0.5f)) - (font.MeasureString(TextForButton).Y * 0.5f);

                //Draws the text inside the button.
                spriteBatch.DrawString(font, TextForButton, new Vector2(x, y), FontColor);
            }
        }
        #endregion
    }
}
