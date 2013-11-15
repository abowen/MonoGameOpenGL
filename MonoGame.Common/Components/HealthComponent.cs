using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Events;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class HealthComponent : IComponent
    {
        private readonly GameLayer _gameLayer;
        private readonly List<Health> _lives = new List<Health>();        

        public HealthComponent(GameObject owner, Texture2D texture2D, Vector2 location, int lives, GameLayer gameLayer)
        {
            Owner = owner;
            owner.ObjectEvent += OwnerOnObjectEvent;
            _gameLayer = gameLayer;

            for (var life = 1; life <= lives; life++)
            {
                var xOffset = life * texture2D.Width * 2;
                var health = new Health(texture2D, new Vector2(location.X + xOffset, location.Y), life, _gameLayer);
                _gameLayer.GameEntities.Add(health);
                _lives.Add(health);
            }
        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs objectEventArgs)
        {
            if (objectEventArgs.Action == ObjectEvent.Collision)
            {
                _hasCollided = true;
            }
        }

        private bool _hasCollided;

        public GameObject Owner { get; set; }

        public void RemoveLife()
        {
            if (_lives.Any())
            {
                var life = _lives.OrderBy(h => h.LifeNumber).Last();
                life.RemoveEntity();
                _lives.Remove(life);    
            }
            else
            {
                //owner.RemoveEntity();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (_hasCollided)
            {
                _hasCollided = false;
                RemoveLife();
            }
        }


        public void Draw(SpriteBatch gameTime)
        {
        }
    }
}
