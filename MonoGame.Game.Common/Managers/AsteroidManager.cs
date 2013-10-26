using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Components;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Infrastructure;
using MonoGameOpenGL.Interfaces;

namespace MonoGame.Game.Common.Managers
{
    public class AsteroidManager : IManager
    {
        private readonly Texture2D[] _texture2D;
        private readonly Texture2D[] _deathTextures;
        private readonly GameLayer _gameLayer;
        private readonly Random _random;
        private TimeSpan _lastTimeSpan;

        public AsteroidManager(Texture2D[] textures, Texture2D[] deathTextures, GameLayer gameLayer)
        {
            _texture2D = textures;
            _deathTextures = deathTextures;
            _lastTimeSpan = new TimeSpan();
            _random = new Random();
            _gameLayer = gameLayer;
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime > _lastTimeSpan.Add(new TimeSpan(0, 0, 0, 1)))
            {
                _lastTimeSpan = gameTime.TotalGameTime;

                // TODO: Use perpendicular
                var y = _random.Next(0, GameConstants.ScreenBoundary.Bottom);
                var isLeftDirection = _random.Next(0, 2) == 1;
                if (isLeftDirection)
                {
                    var x = GameConstants.ScreenBoundary.Right;

                    var asteroid = new GameObject(_gameLayer, new Vector2(x, y));
                    var asteroidGraphics = new SpriteComponent(GetRandomTexture());
                    var asteroidMovement = new MovementComponent(1, FaceDirection.Left, new Vector2(-1,0));
                    asteroid.AddGraphicsComponent(asteroidGraphics);
                    asteroid.AddPhysicsComponent(asteroidMovement);
                    _gameLayer.GameObjects.Add(asteroid);
                    //var asteroid = new Asteroid(GetRandomTexture(), new Vector2(x, y), FaceDirection.Left, _deathTextures, _gameLayer);
                    //_gameLayer.GameEntities.Add(asteroid);
                }
                else
                {
                    var x = GameConstants.ScreenBoundary.Left;
                    var asteroid = new Asteroid(GetRandomTexture(), new Vector2(x, y), FaceDirection.Right, _deathTextures, _gameLayer);
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
