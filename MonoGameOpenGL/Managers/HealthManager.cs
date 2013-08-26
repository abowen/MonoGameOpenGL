﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.Entities;

namespace MonoGameOpenGL.Managers
{
    public class HealthManager
    {
        private readonly GameState _gameState;
        private readonly List<Health> _lives = new List<Health>();        

        public HealthManager(Texture2D texture2D, Vector2 location, int lives, GameState gameState)
        {                        
            _gameState = gameState;

            for (var life = 1; life <= lives; life++)
            {
                var xOffset = life * texture2D.Width * 2;
                var health = new Health(texture2D, new Vector2(location.X + xOffset, location.Y), life);
                _gameState.GameEntities.Add(health);
                _lives.Add(health);
            }
        }

        public void RemoveLife()
        {
            var life = _lives.OrderBy(h => h.LifeNumber).Last();
            life.IsRemoved = true;
            _lives.Remove(life);
        }
    }
}