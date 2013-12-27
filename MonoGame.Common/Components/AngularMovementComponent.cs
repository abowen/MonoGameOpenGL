using Microsoft.Xna.Framework;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class AngularMovementComponent : ISimpleComponent, ISimpleUpdateable, IMovementComponent, IRotationComponent
    {
        private readonly float _minSpeed;
        private readonly float _maxSpeed;

        public AngularMovementComponent(float baseSpeed, float minSpeed, float maxSpeed, float startRotation, Vector2 movementInputDirection)
        {
            _minSpeed = minSpeed;
            _maxSpeed = maxSpeed;
            BaseSpeed = baseSpeed;
            CurrentSpeed = BaseSpeed;
            Rotation = startRotation;
            InputDirection = movementInputDirection;
        }

        /// <summary>
        /// Speed of the object ignoring the location of the camera        
        /// </summary>
        public float BaseSpeed { get; protected set; }

        public float CurrentSpeed { get; protected set; }

        /// <summary>
        /// Speed of the object relative to the camera
        /// </summary>
        public float RelativeSpeed
        {
            get
            {
                var gameDepth = Owner.GameLayer.GameLayerDepth;
                if (gameDepth != GameLayerDepth.Display)
                {
                    var result = CurrentSpeed / (int)gameDepth;
                    if (result == 0)
                    {
                        // DEBUG
                    }
                    return result;
                }
                return 0;
            }
        }

        /// <summary>
        /// Movement direction the player wants changed
        /// </summary>
        public Vector2 InputDirection { get; set; }

        /// <summary>
        /// Actual direction of the object
        /// </summary>
        public Vector2 Direction { get; set; }

        /// <summary>
        /// InputDirection the Game Object is facing
        /// </summary>
        /// <remarks>
        /// It's important to keep FaceDirection separate 
        /// from InputDirection to allow for strafing
        /// </remarks>
        public float Rotation { get; private set; }

        //public Vector2 Velocity
        //{
        //    get
        //    {
        //        return RelativeSpeed * Direction;
        //    }
        //}

        public GameObject Owner { get; set; }


        public void Update(GameTime gameTime)
        {
            // Process Input
            // Move forward
            if (InputDirection.Y < 0)
            {
                if (CurrentSpeed < _maxSpeed)
                {
                    CurrentSpeed++;
                }
            }
            else if (InputDirection.Y > 0)
            {
                if (CurrentSpeed > _minSpeed)
                {
                    CurrentSpeed--;
                }

            }
            if (InputDirection.X < 0)
            {
                Rotation -= 0.05f;
            }
            if (InputDirection.X > 0)
            {
                Rotation += 0.05f;
            }

            // Refactor this to use speed properties and angular inputs
            var matrix = Matrix.CreateRotationZ(Rotation);

            var x = Owner.TopLeft.X;
            var y = Owner.TopLeft.Y;

            x += matrix.M12 * 0.1f * CurrentSpeed;
            y -= matrix.M11 * 0.1f * CurrentSpeed;

            Owner.TopLeft = new Vector2(x, y);

            // http://stackoverflow.com/questions/8200021/getting-a-proper-rotation-from-a-vector-direction              
        }
    }
}
