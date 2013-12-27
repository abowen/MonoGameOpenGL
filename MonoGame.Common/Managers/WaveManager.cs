using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Components;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Surfing;

namespace MonoGame.Common.Managers
{
    public class WaveManager : IManager
    {
        private readonly Texture2D[] _majorTextures;
        private readonly Texture2D[] _minorTextures;
        private readonly GameLayer _backgroundLayer;
        private readonly GameLayer _topLayer;        
        private readonly int _waveHeight;
        private readonly int _yPosition;
        private readonly Random _random;
        private TimeSpan _lastTimeSpan;

        public WaveManager(Texture2D[] majorTextures, Texture2D[] minorTextures, GameLayer backgroundLayer, GameLayer topLayer, int waveHeight, int yPosition)
        {
            _majorTextures = majorTextures;
            _minorTextures = minorTextures;
            _backgroundLayer = backgroundLayer;
            _topLayer = topLayer;
            _lastTimeSpan = new TimeSpan();
            _random = new Random();            
            _waveHeight = waveHeight;
            _yPosition = yPosition;

            var surfBackground = new GameObject("WAVE", new Vector2(0, yPosition));
            var waveTexture = SurfingGraphics.WaveAsset;
            var scaleVector = new Vector2(GameConstants.ScreenBoundary.Right / waveTexture.Width , waveHeight / waveTexture.Height);
            var spriteComponent = new SpriteComponent(waveTexture, Vector2.Zero, scaleVector);
            surfBackground.AddComponent(spriteComponent);
            backgroundLayer.AddGameObject(surfBackground);
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
            var bottomPadding = GameConstants.ScreenBoundary.Bottom - (_yPosition + _waveHeight);
            var gameObject = new GameObject("FOAM", new Vector2(xPosition, _yPosition));
            var movementComponent = new MovementComponent(1, FaceDirection.Down, new Vector2(0, 1));
            var spriteComponent = new SpriteComponent(GetRandomTexture(_majorTextures));
            var outOfBoundsComponent = new OutOfBoundsComponent(gameObject, bottomPadding: bottomPadding);
            gameObject.AddComponent(movementComponent);
            gameObject.AddComponent(spriteComponent);
            gameObject.AddComponent(outOfBoundsComponent);
            _topLayer.AddGameObject(gameObject);
        }

        public Texture2D GetRandomTexture(Texture2D[] textures)
        {
            var randomValue = _random.Next(0, textures.Count());
            return textures[randomValue];
        }
    }
}
