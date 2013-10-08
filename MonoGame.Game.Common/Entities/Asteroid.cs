﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Infrastructure;
using MonoGame.Game.Common.Managers;

namespace MonoGame.Game.Common.Entities
{
    public class Asteroid : Sprite
    {
        private readonly Texture2D[] _deathTextures;

        public Asteroid(Texture2D texture, Vector2 location, FaceDirection faceDirection, Texture2D[] deathTextures, GameLayer gameLayer)
            : base(texture, location, faceDirection, gameLayer)
        {
            _deathTextures = deathTextures;            
            _deathManager = new DeathManager(deathTextures, this, gameLayer);
        }

        public override void RemoveEntity()
        {
            _deathManager.Fire();
            base.RemoveEntity();
        }

        private readonly DeathManager _deathManager;
        
        protected override void CheckBounds()
        {
            if (Location.X < 0)
            {
                IsRemoved = true;
            }
        }
    }
}
