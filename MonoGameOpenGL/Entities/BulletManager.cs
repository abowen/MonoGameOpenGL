using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameOpenGL.Entities
{
    public class BulletManager
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private Texture2D _texture2D;
        private GameState _gameState;

        public BulletManager(Texture2D texture, GameState gameState)
        {
            _stopwatch.Start();
            _texture2D = texture;
            _gameState = gameState;
        }

        public void FirePressed(PlayerShip ship)
        {
            if (_stopwatch.ElapsedMilliseconds > 1000)
            {                
                var bullet = new Bullet(_texture2D, new Vector2(ship.Centre.X, ship.Centre.Y));
                bullet.BulletOutOfBounds += BulletOnBulletOutOfBounds;
                _stopwatch.Restart();
                _gameState.GameEntities.Add(bullet);
            }
        }

        private void BulletOnBulletOutOfBounds(object sender, EventArgs eventArgs)
        {
            var bullet = sender as Bullet;
            if (bullet != null)
            {
                _gameState.GameEntities.Remove(bullet);
            }
        }
    }
}
