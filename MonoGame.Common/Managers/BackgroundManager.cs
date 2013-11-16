using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Components;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Managers
{
    public class BackgroundManager : IManager
    {
        private readonly Texture2D[] _majorTextures;
        private readonly Texture2D[] _minorTextures;
        private readonly GameLayer _gameLayer;
        private readonly Random _random;
        private TimeSpan _lastTimeSpan;

        public BackgroundManager(Texture2D[] majorTextures, Texture2D[] minorTextures, GameLayer gameLayer)
        {
            _majorTextures = majorTextures;
            _minorTextures = minorTextures;
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
                var x = _random.Next(0, GameConstants.ScreenBoundary.Right);
                var isMajorItem = _random.Next(0, 100) == 1;
                if (isMajorItem)
                {                    
                    var star = new GameObject("World", _gameLayer, new Vector2(x, 0));
                    var movementComponent = new MovementComponent(1, FaceDirection.Down, new Vector2(0, 1));
                    var spriteComponent = new SpriteComponent(GetRandomTexture(_majorTextures));
                    star.AddComponent(movementComponent);
                    star.AddComponent(spriteComponent);
                    _gameLayer.GameObjects.Add(star);
                }
                else
                {
                    var star = new GameObject("Star", _gameLayer, new Vector2(x, 0));
                    var movementComponent = new MovementComponent(1, FaceDirection.Down, new Vector2(0, 1));
                    var spriteComponent = new SpriteComponent(GetRandomTexture(_minorTextures));
                    star.AddComponent(movementComponent);
                    star.AddComponent(spriteComponent);
                    _gameLayer.GameObjects.Add(star);
                }
            }
        }

        public Texture2D GetRandomTexture(Texture2D[] textures)
        {
            var randomValue = _random.Next(0, textures.Count());
            return textures[randomValue];
        }
    }
}
