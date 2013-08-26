using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.Enums;
using MonoGameOpenGL.Extensions;

namespace MonoGameOpenGL.Entities
{
    abstract public class Sprite
    {
        internal Texture2D _texture;
        public bool IsRemoved { get; set; }

        /// <summary>
        /// Top-Left co-ordinates
        /// </summary>
        public Vector2 Location { get; protected set; }

        /// <summary>
        /// Velocity = Speed * MovementDirection
        /// </summary>
        public int Speed { get; protected set; }

        /// <summary>
        /// Movement direction of sprite
        /// </summary>
        public Vector2 MovementDirection { get; protected set; }

        /// <summary>
        /// Face direction of sprite
        /// </summary>
        public FaceDirection FaceDirection { get; protected set; }

        public int Width
        {
            get { return _texture.Width; }
        }

        public int Height
        {
            get { return _texture.Height; }
        }

        public Vector2 Centre
        {
            get
            {
                return new Vector2(BoundingBox.Center.X, BoundingBox.Center.Y);
            }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Location.X, (int)Location.Y, Width, Height);
            }
        }

        protected Sprite(Texture2D texture, Vector2 location)
        {
            Speed = 1;
            _texture = texture;
            Location = location;            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Location, Color.White);
        }

        public virtual void Update(GameTime gameTime)
        {
            Location += MovementDirection * Speed;
            CheckBounds();
        }

        protected abstract void CheckBounds();
    }
}
