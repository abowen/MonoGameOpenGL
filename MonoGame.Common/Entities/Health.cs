﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Enums;
using MonoGame.Common.Infrastructure;

namespace MonoGame.Common.Entities
{
    public class Health : Sprite
    {
        public readonly int LifeNumber;

        public Health(Texture2D texture2D, Vector2 location, int lifeNumber, GameLayer gameLayer) :base(texture2D, location, FaceDirection.None, gameLayer)
        {
            LifeNumber = lifeNumber;
        }

        protected override void CheckBounds()
        {            
        }
    }
}