using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameOpenGL.Entities
{
    public class PlayerShip : Sprite
    {

        public PlayerShip(Texture2D texture, Vector2 location, Texture2D bulletTexture, GameState gameState)
            : base(texture, location, gameState)
        {
            _bulletManager = new BulletManager(bulletTexture, gameState);
        }

        private BulletManager _bulletManager;

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Direction = new Vector2(0, -1);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                Direction = new Vector2(0, 1);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _bulletManager.FirePressed(this);
            }

            base.Update(gameTime);
        }

        protected override void CheckBounds()
        {
            //Location.Y = MathHelper.Clamp(Location.Y, 0, GameConstants.ScreenBoundary.Height - _texture.Height);
        }
    }
}
