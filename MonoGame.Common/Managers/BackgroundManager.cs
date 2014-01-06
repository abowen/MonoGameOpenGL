using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Components;
using MonoGame.Common.Components.Graphics;
using MonoGame.Common.Components.Movement;
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
        private readonly Vector2 _direction;
        private readonly int _delayMilliseconds;
        private readonly Random _random;
        private TimeSpan _lastTimeSpan;

        public BackgroundManager(Texture2D[] majorTextures, Texture2D[] minorTextures, GameLayer gameLayer, Vector2 direction, int delayMilliseconds = 1000)
        {
            _majorTextures = majorTextures;
            _minorTextures = minorTextures;
            _lastTimeSpan = new TimeSpan();
            _random = new Random();
            _gameLayer = gameLayer;
            _direction = direction;
            _delayMilliseconds = delayMilliseconds;
        }

        public void HorizontalBoundary(int minimum, int maximum)
        {
            _minX = minimum;
            _maxX = maximum;
        }

        public void VerticalBoundary(int minimum, int maximum)
        {
            _minY = minimum;
            _maxY = maximum;
        }

        private int _minX;
        private int _maxX = GameConstants.ScreenBoundary.Right;

        private int _minY;
        private int _maxY = GameConstants.ScreenBoundary.Bottom;


        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime > _lastTimeSpan.Add(new TimeSpan(0, 0, 0, 0, _delayMilliseconds)))
            {
                _lastTimeSpan = gameTime.TotalGameTime;

                var x = _random.Next(_minX, _maxX);
                var y = _random.Next(_minY, _maxY);
                var isMajorItem = _random.Next(0, 30) == 1;
                if (isMajorItem)
                {                    
                    var star = new GameObject("BackgroundMajor", new Vector2(x, y));
                    var movementComponent = new MovementComponent(1, FaceDirection.Down, _direction);
                    var spriteComponent = new SpriteComponent(GetRandomTexture(_majorTextures));
                    star.AddComponent(movementComponent);
                    star.AddComponent(spriteComponent);
                    _gameLayer.AddGameObject(star);
                }
                else
                {
                    var star = new GameObject("BackgroundMinor", new Vector2(x, y));
                    var movementComponent = new MovementComponent(1, FaceDirection.Down, _direction);
                    var spriteComponent = new SpriteComponent(GetRandomTexture(_minorTextures));
                    star.AddComponent(movementComponent);
                    star.AddComponent(spriteComponent);
                    _gameLayer.AddGameObject(star);
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
