using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.Entities;

namespace MonoGameOpenGL.Managers
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
            if (_stopwatch.ElapsedMilliseconds > 500)
            {
                var bullet = new Bullet(_texture2D, new Vector2(ship.Centre.X, ship.Centre.Y), ship.FaceDirection);                
                _stopwatch.Restart();
                _gameState.GameEntities.Add(bullet);
            }
        }
    }
}
