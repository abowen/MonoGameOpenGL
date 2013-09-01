﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.Enums;
using MonoGameOpenGL.Extensions;
using MonoGameOpenGL.Managers;

namespace MonoGameOpenGL.Entities
{
    internal class Asteroid : Sprite
    {
        private readonly Texture2D[] _deathTextures;

        public Asteroid(Texture2D texture, Vector2 location, FaceDirection faceDirection, Texture2D[] deathTextures, GameLayer gameLayer)
            : base(texture, location, gameLayer)
        {
            _deathTextures = deathTextures;
            MovementDirection = faceDirection.GetVector2();
            FaceDirection = faceDirection;          
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
