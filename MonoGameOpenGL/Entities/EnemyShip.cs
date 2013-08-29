using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.Enums;
using MonoGameOpenGL.Extensions;
using MonoGameOpenGL.Managers;

namespace MonoGameOpenGL.Entities
{
    public class EnemyShip : Sprite
    {

        public EnemyShip(Texture2D texture, Vector2 location, Texture2D bulletTexture, GameLayer gameLayer)
            : base(texture, location, gameLayer)
        {
            Speed = 1;
            FaceDirection = FaceDirection.Bottom;
            MovementDirection = FaceDirection.GetVector2();
            _bulletManager = new BulletManager(bulletTexture, gameLayer);
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

            var xChange = (float) Math.Cos(Location.Y / 40);
            MovementDirection = new Vector2(xChange, MovementDirection.Y);

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
