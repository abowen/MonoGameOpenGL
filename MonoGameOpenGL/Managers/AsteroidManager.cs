using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.Entities;
using MonoGameOpenGL.Enums;

namespace MonoGameOpenGL.Managers
{
    public class AsteroidManager
    {
        private readonly Texture2D[] _texture2D;
        private readonly GameLayer _gameLayer;
        private readonly Random _random;
        private TimeSpan _lastTimeSpan;

        public AsteroidManager(Texture2D[] textures, GameLayer gameLayer)
        {
            _texture2D = textures;
            _lastTimeSpan = new TimeSpan();
            _random = new Random();
            _gameLayer = gameLayer;
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime > _lastTimeSpan.Add(new TimeSpan(0, 0, 0, 1)))
            {
                _lastTimeSpan = gameTime.TotalGameTime;

                var y = _random.Next(0, GameConstants.ScreenBoundary.Bottom);
                var isLeftDirection = _random.Next(0, 2) == 1;
                if (isLeftDirection)
                {
                    var x = GameConstants.ScreenBoundary.Right;
                    var asteroid = new Asteroid(GetRandomTexture(), new Vector2(x, y), FaceDirection.Left, _gameLayer);
                    _gameLayer.GameEntities.Add(asteroid);
                }
                else
                {
                    var x = GameConstants.ScreenBoundary.Left;                    
                    var asteroid = new Asteroid(GetRandomTexture(), new Vector2(x, y), FaceDirection.Right, _gameLayer);
                    _gameLayer.GameEntities.Add(asteroid);
                }
            }
        }

        public Texture2D GetRandomTexture()
        {
            var randomValue = _random.Next(0, _texture2D.Count());
            return _texture2D[randomValue];
        }
    }
}
