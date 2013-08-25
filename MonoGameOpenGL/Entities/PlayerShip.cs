using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameOpenGL.Entities
{
    internal class PlayerShip : Sprite
    {

        public PlayerShip(Texture2D texture, Vector2 location, Rectangle screenBounds)
            : base(texture, location, screenBounds)
        {

        }


        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (Velocity.Y > -2)
                {
                    Velocity.Y--;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (Velocity.Y < 2)
                {
                    Velocity.Y++;
                }
            }

            base.Update(gameTime);
        }

        protected override void CheckBounds()
        {
            Location.Y = MathHelper.Clamp(Location.Y, 0, _screenBounds.Height - _texture.Height);
        }
    }
}
