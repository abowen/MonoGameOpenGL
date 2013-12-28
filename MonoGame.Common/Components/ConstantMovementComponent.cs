using Microsoft.Xna.Framework;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class ConstantMovementComponent : ISimpleComponent, ISimpleUpdateable
    {
        private readonly Vector2 _movement;

        public GameObject Owner { get; private set; }

        public void SetOwner(GameObject owner)
        {
            Owner = owner;
        }

        public ConstantMovementComponent(Vector2 movement)
        {
            _movement = movement;
        }

        public void Update(GameTime gameTime)
        {
            Owner.TopLeft += _movement;
        }
    }
}
