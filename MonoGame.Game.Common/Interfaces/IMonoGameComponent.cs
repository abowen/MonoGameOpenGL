using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;

namespace MonoGame.Game.Common.Interfaces
{
    public interface IMonoGameComponent
    {
        GameObject Owner { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch gameTime);
    }
}
