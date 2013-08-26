using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.Entities;

namespace MonoGameOpenGL.Managers
{
    public class AsteroidManager
    {        
        private Texture2D _texture2D;
        private GameState _gameState;
        private TimeSpan _lastTimeSpan;
        private readonly Random _random;

        public AsteroidManager(Texture2D texture, GameState gameState)
        {            
            _texture2D = texture;            
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

                var asteroid = new Asteroid(_texture2D, new Vector2(x, y));                
                
                _gameState.GameEntities.Add(asteroid);
            }
        }
    }
}
