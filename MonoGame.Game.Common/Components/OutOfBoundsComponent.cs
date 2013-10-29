using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Infrastructure;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Components
{
    public class OutOfBoundsComponent : IMonoGameComponent
    {
        public GameObject Owner { get; set; }

        public OutOfBoundsComponent(GameObject owner)
        {
            Owner = owner;
        }

        public void Update(GameTime gameTime)
        {
            if (Owner.Centre.X > GameConstants.ScreenBoundary.Right ||
                Owner.Centre.X < 0 ||
                Owner.Centre.Y < 0 ||
                Owner.Centre.Y > GameConstants.ScreenBoundary.Bottom)
            {
                Owner.RemoveGameObject();
            }
        }

        public void Draw(SpriteBatch gameTime)
        {
            
        }
    }
}
