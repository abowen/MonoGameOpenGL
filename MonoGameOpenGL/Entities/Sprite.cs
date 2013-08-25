using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameOpenGL.Entities
{
    abstract public class Sprite
    {
        internal Texture2D _texture;
        public Vector2 Location;
        internal Vector2 Velocity;
        private GameState _gameState;
        //protected Rectangle _screenBounds;

        public int Width
        {
            get
            {
                return _texture.Width;
            }
        }

        public int Height
        {
            get
            {
                return _texture.Height;
            }
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
            Location += Velocity;
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
