using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameOpenGL.Entities
{
    abstract public class Sprite
    {
        internal Texture2D _texture;
        private GameState _gameState;

        /// <summary>
        /// Top-Left co-ordinates
        /// </summary>
         public Vector2 Location { get; protected set; }        

        /// <summary>
        /// Velocity = Speed * Direction
        /// </summary>
        public int Speed { get; protected set; }

        /// <summary>
        /// Direction of sprite
        /// </summary>
        public Vector2 Direction { get; protected set; }

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

        protected Sprite(Texture2D texture, Vector2 location, GameState gameState)
        {
            Speed = 1;
            _texture = texture;
            Location = location;
            _gameState = gameState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Location, Color.White);
        }

        public virtual void Update(GameTime gameTime)
        {
            Location += Direction * Speed;
            CheckBounds();
        }

        public void Destroy()
        {
            if (_gameState.GameEntities.Contains(this))
            {
                _gameState.GameEntities.Remove(this);
            }
        }

        protected abstract void CheckBounds();
    }
}
