using Microsoft.Xna.Framework;
using MonoGame.Common.Entities;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class OutOfBoundsComponent : ISimpleComponent, ISimpleUpdateable
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
    }
}
