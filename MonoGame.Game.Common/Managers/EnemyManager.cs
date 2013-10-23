using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Infrastructure;
using MonoGameOpenGL.Interfaces;

namespace MonoGame.Game.Common.Managers
{
    public class EnemyManager : IManager
    {
        private readonly Texture2D _shipTexture;
        private readonly Texture2D _bulletTexture;
        private readonly GameLayer _gameLayer;
        private readonly int _speed;
        private double _elapsedTimeMilliseconds;
        private readonly Random _random;
        private readonly double _spawnDelayMilliseconds;
        private readonly double _bulletDelayMilliseconds;

        public EnemyManager(Texture2D shipTexture, Texture2D bulletTexture, double spawnDelayMilliseconds, double bulletDelayMilliseconds, GameLayer gameLayer, int speed)
        {
            _shipTexture = shipTexture;
            _bulletTexture = bulletTexture;
            _gameLayer = gameLayer;
            _speed = speed;
            _random = new Random();
            _spawnDelayMilliseconds = spawnDelayMilliseconds;
            _bulletDelayMilliseconds = bulletDelayMilliseconds;
        }

        private const int MAX_ENEMIES = 1;

        public void Update(GameTime gameTime)
        {
            var enemyCount = _gameLayer.GameEntities.Count(sprite => typeof (EnemyShip) == sprite.GetType());

            _elapsedTimeMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_elapsedTimeMilliseconds > _spawnDelayMilliseconds && enemyCount < MAX_ENEMIES)
            {
                _elapsedTimeMilliseconds = 0;
                var xLocation = _random.Next(0, GameConstants.ScreenBoundary.Right - _shipTexture.Width);
                var enemy = new EnemyShip(_shipTexture, new Vector2(xLocation, 0), _bulletTexture, _bulletDelayMilliseconds, FaceDirection.Down,  _speed, _gameLayer);
                _gameLayer.GameEntities.Add(enemy);
            }
        }
    }
}
