using System;
using Microsoft.Xna.Framework;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.Movement
{
    public class AccelerateMovementComponent : SimpleComponent, ISimpleUpdateable
    {
        private readonly Func<GameObject, Vector2> _movementFunc;
        private Vector2 _movement;

        public AccelerateMovementComponent(Vector2 movement)
        {
            _movement = movement;
        }

        public AccelerateMovementComponent(Func<GameObject, Vector2> movementFunc)
        {
            _movementFunc = movementFunc;
        }

        public void Update(GameTime gameTime)
        {
            if (_movementFunc != null)
            {
                _movement = _movementFunc(Owner);
            }
            Owner.Velocity += _movement;
        }
    }
}
