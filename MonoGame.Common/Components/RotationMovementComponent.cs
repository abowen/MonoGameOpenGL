using System;
using Microsoft.Xna.Framework;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Events;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class RotationMovementComponent : SimpleComponent, ISimpleUpdateable, IMovementComponent
    {
        private readonly float _originalSpeed;
        private float _speed;

        public RotationMovementComponent(float speed, Vector2 movementInputDirection)
        {
            _originalSpeed = speed;
            _speed = speed;

            InputDirection = movementInputDirection;
        }

        /// <summary>
        /// Movement direction the player wants changed
        /// </summary>
        public Vector2 InputDirection { get; set; }

        public override void SetOwner(GameObject owner)
        {
            Owner = owner;
            owner.ObjectEvent += OwnerOnObjectEvent;
        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs objectEventArgs)
        {
            if (ObjectEvent.ResetEntity == objectEventArgs.Action)
            {
                _speed = _originalSpeed;
            }
        }

        public void Update(GameTime gameTime)
        {
            // Process Input
            // Move forward

            if (InputDirection.Y < 0)
            {
                if (_speed < 30)
                {
                    _speed++;
                }
            }
            else if (InputDirection.Y > 0)
            {
                if (_speed > 0)
                {
                    _speed--;
                }
            }


            // Rotate Left / Right by Radian
            if (InputDirection.X < 0)
            {
                Owner.Rotation -= 0.05f;
            }
            if (InputDirection.X > 0)
            {
                Owner.Rotation += 0.05f;
            }


            var posX = _speed / 10f * ((float)Math.Cos(Owner.Rotation));
            var posY = _speed / 10f * ((float)Math.Sin(Owner.Rotation));
            Owner.Velocity = new Vector2(posX, posY);          
        }
    }
}
