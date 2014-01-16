using Microsoft.Xna.Framework;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.Movement
{
    public class AccelerateMovementComponent : SimpleComponent, ISimpleUpdateable
    {
        private readonly Vector2 _movement;

        public AccelerateMovementComponent(Vector2 movement)
        {
            _movement = movement;
        }

        public void Update(GameTime gameTime)
        {
            Owner.Velocity += _movement;
        }
    }
}
