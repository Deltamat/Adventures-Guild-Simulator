using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    class GameObject
    {
        protected Texture2D sprite;
        protected Vector2 position;
        /// <summary>
        /// Get-set property for the sprite Texture2D
        /// </summary>
        public Texture2D Sprite { get => sprite; set => sprite = value; }
        /// <summary>
        /// Get-set property for the position
        /// </summary>
        public Vector2 Position { get => position; set => position = value; }
        
        /// <summary>
        /// Constructor for the GameObject
        /// </summary>
        /// <param name="position">The position of the building</param>
        /// <param name="spriteName">The name of the sprite</param>
        public GameObject(Vector2 position, string spriteName)
        {
            this.Position = position;
            Sprite = GameWorld.ContentManager.Load<Texture2D>(spriteName);
        }

        /// <summary>
        /// Get property that returns a collisionbox
        /// </summary>
        public virtual Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)(Position.X - Sprite.Width * 0.5), (int)(Position.Y - Sprite.Height * 0.5), Sprite.Width, Sprite.Height);
            }
        }


        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
