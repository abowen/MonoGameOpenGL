using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameOpenGL.Entities
{
    internal class Ball : Sprite
    {
        public Ball(Texture2D texture, Vector2 location, Rectangle screenBounds)
            : base(texture, location, screenBounds)
        {
        }

        private Paddle attachedToPaddle;

        protected override void CheckBounds()
        {
            if (Location.Y < 0 || Location.Y > (_screenBounds.Height - Height))
            {
                Velocity.Y = -Velocity.Y;
            }
            if (Location.X < 0 || Location.X > (_screenBounds.Width - Width))
            {
                Velocity.X = -Velocity.X;
            }
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) &&
                attachedToPaddle != null)
            {
                Velocity = new Vector2(2, attachedToPaddle.Velocity.Y);
                attachedToPaddle = null;
            }

            if (attachedToPaddle != null)
            {
                Location.X = attachedToPaddle.Location.X + attachedToPaddle.Width;
                Location.Y = attachedToPaddle.Location.Y;
            }
            else
            {
                if (BoundingBox.Intersects(gameObjects.PaddlePlayer.BoundingBox) ||
                    BoundingBox.Intersects(gameObjects.PaddleComputer.BoundingBox))
                {
                    Velocity.X = -Velocity.X;
                }
            }

            base.Update(gameTime, gameObjects);
        }

        public void AttachTo(Paddle paddle)
        {
            attachedToPaddle = paddle;
        }
    }
}
