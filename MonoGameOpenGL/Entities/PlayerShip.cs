using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameOpenGL.Helpers;
using MonoGameOpenGL.Managers;

namespace MonoGameOpenGL.Entities
{
    public class PlayerShip : Sprite
    {

        public PlayerShip(Texture2D texture, Vector2 location, Texture2D bulletTexture, Texture2D healthTexture, int lives, GameState gameState)
            : base(texture, location)
        {
            _bulletManager = new BulletManager(bulletTexture, gameState);            
            HealthManager = new HealthManager(healthTexture, new Vector2(20,20), lives, gameState);
        }

        private BulletManager _bulletManager;
        public HealthManager HealthManager { get; private set; }

        public override void Update(GameTime gameTime)
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();
            Direction = InputHelper.KeyboardDirection(keysPressed);

            if (keysPressed.Any(k => k == Keys.Space))
            {
                _bulletManager.FirePressed(this);
            }

            base.Update(gameTime);
        }

        protected override void CheckBounds()
        {
            var topLeft = new Vector2(0, 0);
            var bottomRight = new Vector2(GameConstants.ScreenBoundary.Width - _texture.Width, GameConstants.ScreenBoundary.Height - _texture.Height);
            Location = Vector2.Clamp(Location, topLeft, bottomRight);
        }
    }
}
