using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Components
{
    public class SpriteComponent : IMonoGameComponent
    {
        internal Texture2D Texture;
                                    
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Owner.Centre, Color.White);
        }

        public void Update(GameTime gameTime)
        {            
         
        }        
    }
}
