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
    public class WaveManager : IManager
    {
        private readonly Texture2D[] _majorTextures;
        private readonly Texture2D[] _minorTextures;
        private readonly GameLayer _gameLayer;
        private readonly int _waveHeight;
        private readonly int _yPosition;
        private readonly Random _random;
        private TimeSpan _lastTimeSpan;

        public WaveManager(Texture2D[] majorTextures, Texture2D[] minorTextures, GameLayer gameLayer, int waveHeight, int yPosition)
        {
            _majorTextures = majorTextures;
            _minorTextures = minorTextures;
            _lastTimeSpan = new TimeSpan();
            _random = new Random();
            _gameLayer = gameLayer;
            _waveHeight = waveHeight;
            _yPosition = yPosition;
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime > _lastTimeSpan.Add(new TimeSpan(0, 0, 0, 0, 100)))
            {
                _lastTimeSpan = gameTime.TotalGameTime;
                
                GenerateFoam(GetRandomX(2));
                GenerateFoam(GetRandomX(3));
                GenerateFoam(GetRandomX(4));
                GenerateFoam(GetRandomX(4));
                GenerateFoam(GetRandomX(8));
                GenerateFoam(GetRandomX(8));
                GenerateFoam(GetRandomX(8));
                GenerateFoam(GetRandomX(16));
            }
        }

        private int GetRandomX(int divider)
        {
            return _random.Next(0, GameConstants.ScreenBoundary.Right/divider);
        }

        private void GenerateFoam(int xPosition)
        {
       
            var gameObject = new GameObject("Foam", _gameLayer, new Vector2(xPosition, _yPosition));
            var movementComponent = new MovementComponent(1, FaceDirection.Down, new Vector2(0, 1));
            var spriteComponent = new SpriteComponent(GetRandomTexture(_majorTextures));
            var outOfBoundsComponent = new OutOfBoundsComponent(gameObject);
            gameObject.AddComponent(movementComponent);
            gameObject.AddComponent(spriteComponent);
            gameObject.AddComponent(outOfBoundsComponent);
            _gameLayer.GameObjects.Add(gameObject);
        }

        public Texture2D GetRandomTexture(Texture2D[] textures)
        {
            var randomValue = _random.Next(0, textures.Count());
            return textures[randomValue];
        }
    }
}
