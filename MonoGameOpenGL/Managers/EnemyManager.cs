using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.Entities;

namespace MonoGameOpenGL.Managers
{
    public class EnemyManager
    {
        private readonly Texture2D _shipTexture;
        private readonly Texture2D _bulletTexture;
        private readonly GameState _gameState;
        private double _elapsedTimeMilliseconds;
        private readonly Random _random;

        public EnemyManager(Texture2D shipTexture, Texture2D bulletTexture, GameState gameState)
        {
            _shipTexture = shipTexture;
            _bulletTexture = bulletTexture;
            _gameState = gameState;
            _random = new Random();
        }

        public void Update(GameTime gameTime)
        {
            _elapsedTimeMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_elapsedTimeMilliseconds > 1500)
            {
                _elapsedTimeMilliseconds = 0;
                var xLocation = _random.Next(0, GameConstants.ScreenBoundary.Right - _shipTexture.Width);
                var enemy = new EnemyShip(_shipTexture, new Vector2(xLocation, 0), _bulletTexture, _gameState);
                _gameState.GameEntities.Add(enemy);
            }
        }
    }
}
