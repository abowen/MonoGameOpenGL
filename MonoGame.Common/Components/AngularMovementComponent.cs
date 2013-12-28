using System;
using Microsoft.Xna.Framework;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Events;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class AngularMovementComponent : ISimpleComponent, ISimpleUpdateable, IMovementComponent, IRotationComponent
    {
        private float _speed;
        private readonly ObjectEvent _enableEvent;
        private readonly ObjectEvent _disableEvent;

        public AngularMovementComponent(float speed, float startRotation, Vector2 movementInputDirection, ObjectEvent enableEvent, ObjectEvent disableEvent)
        {
            _speed = speed;
            _enableEvent = enableEvent;
            _disableEvent = disableEvent;
            Rotation = startRotation;
            InputDirection = movementInputDirection;
        }

        /// <summary>
        /// Movement direction the player wants changed
        /// </summary>
        public Vector2 InputDirection { get; set; }

        /// <summary>
        /// InputDirection the Game Object is facing
        /// </summary>
        /// <remarks>
        /// It's important to keep FaceDirection separate 
        /// from InputDirection to allow for strafing
        /// </remarks>
        public float Rotation { get; private set; }

        //public float Acceleration { get; private set; }

        public Vector2 Velocity { get; private set; }

        public GameObject Owner { get; private set; }

        public void SetOwner(GameObject owner)
        {
            Owner = owner;
            owner.ObjectEvent += OwnerOnObjectEvent;
        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs objectEventArgs)
        {
            if (_enableEvent == objectEventArgs.Action)
            {
                _movementDisabled = false;
            }
            else if (_disableEvent == objectEventArgs.Action)
            {
                _movementDisabled = true;
                //Acceleration = 0;
            }
        }

        private bool _movementDisabled;


        public void Update(GameTime gameTime)
        {
            // Process Input
            // Move forward
            if (!_movementDisabled)
            {
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
            }

            // Rotate Left / Right by Radian
            if (InputDirection.X < 0)
            {
                Rotation -= 0.05f;
            }
            if (InputDirection.X > 0)
            {
                Rotation += 0.05f;
            }

           
            if (!_movementDisabled)
            {
                var posX = _speed / 10f * ((float)Math.Cos(Rotation));
                var posY = _speed / 10f * ((float)Math.Sin(Rotation));
                Velocity = new Vector2(posX, posY);
            }
            else
            {
                Velocity += new Vector2(0, 0.05f);
            }

            Owner.TopLeft += Velocity;
        }
    }
}
