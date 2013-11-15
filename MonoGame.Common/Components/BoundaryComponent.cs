﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Entities;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class BoundaryComponent : IComponent
    {
        private readonly Texture2D _texture;
        public readonly int Width;
        public readonly int Height;

        public BoundaryComponent(GameObject owner, Texture2D texture, int width, int height)
        {
            _texture = texture;
            Width = width;
            Height = height;
            Owner = owner;
        }


        public GameObject Owner { get; set; }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameConstants.ShowObjectBoundary)
            {
                spriteBatch.Draw(_texture, new Vector2(Owner.TopLeft.X, Owner.TopLeft.Y), null, Color.White, 0, Vector2.Zero, new Vector2(Height, Width), SpriteEffects.None, 0 );
            }
        }
    }
}