using Microsoft.Xna.Framework;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.Movement
{
    public class RotationComponent : SimpleComponent, ISimpleUpdateable, IMovementComponent
    {
        public RotationComponent(Vector2 movementInputDirection)
        {

            InputDirection = movementInputDirection;
        }

        /// <summary>
        /// Movement direction the player wants changed
        /// </summary>
        public Vector2 InputDirection { get; set; }


        public void Update(GameTime gameTime)
        {
           // Rotate Left / Right by Radian
            if (InputDirection.X < 0)
            {
                Owner.Rotation -= 0.05f;
            }
            if (InputDirection.X > 0)
            {
                Owner.Rotation += 0.05f;
            }
        }
    }
}
