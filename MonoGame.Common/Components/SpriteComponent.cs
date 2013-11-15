using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Components
{
    public class SpriteComponent : IComponent
    {
        internal Texture2D Texture;
        private readonly Vector2 _relativeLocation;

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }        

        public GameObject Owner { get; set; }

        public SpriteComponent(Texture2D texture)
        {            
            Texture = texture;
            _relativeLocation = Vector2.Zero;
        }

        public SpriteComponent(Texture2D texture, Vector2 relativeLocation)
        {
            Texture = texture;
            _relativeLocation = relativeLocation;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Owner.TopLeft + _relativeLocation, Color.White);
        }

        public void Update(GameTime gameTime)
        {            
         
        }        
    }
}
