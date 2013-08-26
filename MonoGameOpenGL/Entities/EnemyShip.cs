using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.Enums;
using MonoGameOpenGL.Extensions;
using MonoGameOpenGL.Managers;

namespace MonoGameOpenGL.Entities
{
    public class EnemyShip : Sprite
    {

        public EnemyShip(Texture2D texture, Vector2 location, Texture2D bulletTexture, GameState gameState)
            : base(texture, location)
        {
            Speed = 1;
            FaceDirection = FaceDirection.Bottom;
            MovementDirection = FaceDirection.GetVector2();
            _bulletManager = new BulletManager(bulletTexture, gameState);
        }

        private double _timeElapsedMilliseconds;
        private readonly BulletManager _bulletManager;

        public override void Update(GameTime gameTime)
        {
            _timeElapsedMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_timeElapsedMilliseconds > 2000)
            {
                _timeElapsedMilliseconds = 0;
                _bulletManager.Fire(this);
            }
            base.Update(gameTime);
        }

        protected override void CheckBounds()
        {
            if (Location.Y > GameConstants.ScreenBoundary.Bottom)
            {
                IsRemoved = true;
            }
        }
    }
}
