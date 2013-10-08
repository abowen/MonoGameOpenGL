using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.Entities;
using MonoGameOpenGL.Enums;
using MonoGameOpenGL.Infrastructure;
using MonoGameOpenGL.Interfaces;

namespace MonoGameOpenGL.Managers
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
                    var item = new StaticSprite(GetRandomTexture(_majorTextures), new Vector2(x, 0), FaceDirection.Down, _gameLayer);
                    _gameLayer.GameEntities.Add(item);
                }
                else
                {
                    var planet = new StaticSprite(GetRandomTexture(_minorTextures), new Vector2(x, 0), FaceDirection.Down, _gameLayer);
                    _gameLayer.GameEntities.Add(planet);
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
