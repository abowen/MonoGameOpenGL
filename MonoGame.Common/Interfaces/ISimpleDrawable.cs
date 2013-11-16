using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Common.Interfaces
{
    // Simpler version of XNA. Introduce additional concepts as needed
    public interface ISimpleDrawable
    {        
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
