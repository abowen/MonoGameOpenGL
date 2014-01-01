using Microsoft.Xna.Framework;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class ConstantMovementComponent : SimpleComponent, ISimpleUpdateable
    {
        private readonly Vector2 _movement;

        public ConstantMovementComponent(Vector2 movement)
        {
            _movement = movement;
        }

        public void Update(GameTime gameTime)
        {
            Owner.Velocity += _movement;
        }
    }
}
