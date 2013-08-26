using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.Entities;

namespace MonoGameOpenGL.Managers
{
    public class AsteroidManager
    {        
        private readonly Texture2D[] _texture2D;
        private readonly GameState _gameState;        
        private readonly Random _random;
        private TimeSpan _lastTimeSpan;

        public AsteroidManager(Texture2D[] textures, GameState gameState)
        {            
            _texture2D = textures;            
            _lastTimeSpan = new TimeSpan();
            _random = new Random();
            _gameState = gameState;
            
        }

        public void Update(GameTime gameTime)
        {            
            if (gameTime.TotalGameTime > _lastTimeSpan.Add(new TimeSpan(0, 0, 0, 1)))
            {
                _lastTimeSpan = gameTime.TotalGameTime;
                var x = GameConstants.ScreenBoundary.Right;
                var y = _random.Next(0, GameConstants.ScreenBoundary.Bottom);

                var asteroid = new Asteroid(GetRandomTexture(), new Vector2(x, y));                
                
                _gameState.GameEntities.Add(asteroid);
            }
        }

        public Texture2D GetRandomTexture()
        {
            var randomValue = _random.Next(0,_texture2D.Count());
            return _texture2D[randomValue];
        }
    }
}
