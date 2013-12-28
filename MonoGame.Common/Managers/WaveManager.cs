using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Components;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;
using MonoGame.Graphics.Surfing;

namespace MonoGame.Common.Managers
{
    public class WaveManager : IManager
    {
        private readonly Texture2D[] _majorTextures;
        
        
        private readonly GameLayer _topLayer;        
        private readonly int _waveHeight;
        private readonly int _yPosition;
        private readonly float _waveSpeed;
        private readonly Random _random;
        private TimeSpan _lastTimeSpan;

        public WaveManager(Texture2D[] majorTextures, GameLayer backgroundLayer, GameLayer topLayer, int waveHeight, int yPosition, float waveSpeed)
        {
            _majorTextures = majorTextures;
            _topLayer = topLayer;
            _lastTimeSpan = new TimeSpan();
            _random = new Random();            
            _waveHeight = waveHeight;
            _yPosition = yPosition;
            _waveSpeed = waveSpeed;

            var surfBackground = new GameObject("WAVE", new Vector2(0, yPosition));
            var waveTexture = SurfingGraphics.Wave_8x100_Asset;
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
                                                
                GenerateFoam(GetRandomX(2), 1);
                GenerateFoam(GetRandomX(2), 1);
                GenerateFoam(GetRandomX(2), 1);
                GenerateFoam(GetRandomX(3), 4);
                GenerateFoam(GetRandomX(4), 6);
                GenerateFoam(GetRandomX(5), 10);
            }
        }

        private int GetRandomX(int divider)
        {
            return _random.Next(0, GameConstants.ScreenBoundary.Right/divider);
        }

        private void GenerateFoam(int xPosition, int width)
        {
            var bottomPadding = GameConstants.ScreenBoundary.Bottom - (_yPosition + _waveHeight);
            var gameObject = new GameObject("FOAM", new Vector2(xPosition, _yPosition));
            var movementComponent = new MovementComponent(1, FaceDirection.Down, new Vector2(_waveSpeed, 1));
            var spriteComponent = new SpriteComponent(GetRandomTexture(_majorTextures), Vector2.Zero, new Vector2(width, 1));
            var outOfBoundsComponent = new OutOfBoundsComponent(ObjectEvent.RemoveEntity, leftPadding: -50, bottomPadding: bottomPadding);
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
