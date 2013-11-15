using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Entities;

namespace MonoGame.Common.Interfaces
{
    public interface ISimpleComponent : IGameComponent
    {
        GameObject Owner { get; set; }        
    }

    public interface IGameComponent
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch gameTime);
    }
}
