using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Components;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;
using MonoGame.Graphics.Space;

namespace MonoGame.Common.Managers
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

                    var asteroid = new GameObject("Asteroid", new Vector2(x, y));                    
                    var asteroidInstance = new InstanceComponent();
                    var asteroidGraphics = new SpriteComponent(GetRandomTexture());
                    var asteroidMovement = new MovementComponent(1, FaceDirection.Left, new Vector2(-1,0));
                    var asteroidBoundary = new BoundaryComponent(SpaceGraphics.BoundaryAsset.First(),
                        asteroidGraphics.Width, asteroidGraphics.Height);
                    var asteroidOutOfBounds = new OutOfBoundsComponent(ObjectEvent.RemoveEntity, -50, 0, -50, 0);
                    asteroid.AddComponent(asteroidInstance);
                    asteroid.AddComponent(asteroidGraphics);
                    asteroid.AddComponent(asteroidMovement);
                    asteroid.AddComponent(asteroidBoundary);
                    asteroid.AddComponent(asteroidOutOfBounds);
                    _gameLayer.AddGameObject(asteroid);
                }
                else
                {
                    var x = GameConstants.ScreenBoundary.Left;
                    var asteroid = new GameObject("Asteroid", new Vector2(x, y));                    
                    var asteroidInstance = new InstanceComponent();
                    var asteroidGraphics = new SpriteComponent(GetRandomTexture());
                    var asteroidMovement = new MovementComponent(1, FaceDirection.Right, new Vector2(1, 0));
                    var asteroidBoundary = new BoundaryComponent(SpaceGraphics.BoundaryAsset.First(),
                        asteroidGraphics.Width, asteroidGraphics.Height);
                    var asteroidOutOfBounds = new OutOfBoundsComponent(ObjectEvent.RemoveEntity, -50, 0, -50, 0);
                    asteroid.AddComponent(asteroidInstance);
                    asteroid.AddComponent(asteroidGraphics);
                    asteroid.AddComponent(asteroidMovement);
                    asteroid.AddComponent(asteroidBoundary);
                    asteroid.AddComponent(asteroidOutOfBounds);

                    _gameLayer.AddGameObject(asteroid);
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
