using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameOpenGL.Entities
{
    public enum PlayerType
    {
        Human,
        Computer
    }

    internal class Paddle : Sprite
    {
        private PlayerType _playerType;        

        public Paddle(Texture2D texture, Vector2 location, Rectangle screenBounds, PlayerType playerType)
            : base(texture, location, screenBounds)
        {
            _playerType = playerType;            
        }



        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (_playerType == PlayerType.Human)
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
            }
            else if (_playerType == PlayerType.Computer)
            {
                var ball = gameObjects.Ball;
                if (ball.Location.Y + ball.Height < Location.Y)
                {
                    Velocity.Y = -2;
                }
                if (ball.Location.Y > Location.Y + Height)
                {
                    Velocity.Y = 2;
                }
            }

            
            base.Update(gameTime, gameObjects);
        }

        protected override void CheckBounds()
        {
            Location.Y = MathHelper.Clamp(Location.Y, 0, _screenBounds.Height - _texture.Height);
        }
    }
}
