using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;

namespace MonoGame.Game.Common.Interfaces
{
    public interface IComponent : IGameComponent
    {
        GameObject Owner { get; set; }        
    }

    public interface IGameComponent
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch gameTime);
    }
}
