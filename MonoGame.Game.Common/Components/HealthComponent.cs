using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Infrastructure;
using MonoGame.Game.Common.Interfaces;
using MonoGameOpenGL.Interfaces;

namespace MonoGame.Game.Common.Components
{
    public class HealthComponent : IMonoGameComponent
    {
        private readonly GameLayer _gameLayer;
        private readonly List<Health> _lives = new List<Health>();        

        public HealthComponent(Texture2D texture2D, Vector2 location, int lives, GameLayer gameLayer)
        {                        
            _gameLayer = gameLayer;

            for (var life = 1; life <= lives; life++)
            {
                var xOffset = life * texture2D.Width * 2;
                var health = new Health(texture2D, new Vector2(location.X + xOffset, location.Y), life, _gameLayer);
                _gameLayer.GameEntities.Add(health);
                _lives.Add(health);
            }
        }

        public GameObject Owner { get; set; }

        public void RemoveLife(Sprite owner)
        {
            if (_lives.Any())
            {
                var life = _lives.OrderBy(h => h.LifeNumber).Last();
                life.RemoveEntity();
                _lives.Remove(life);    
            }
            else
            {
                owner.RemoveEntity();
            }
        }

        public void Update(GameTime gameTime)
        {
            
        }


        public void Draw(SpriteBatch gameTime)
        {
        }
    }
}
