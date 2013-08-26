using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameOpenGL.Entities
{
    internal class Asteroid : Sprite
    {
        public Asteroid(Texture2D texture, Vector2 location, GameState gameState)
            : base(texture, location, gameState)
        {
            Direction = new Vector2(-1, 0);
        }

        public event EventHandler OutOfBounds;
        
        protected override void CheckBounds()
        {
            if (Location.X < 0)
            {
                if (OutOfBounds != null)
                {
                    OutOfBounds.BeginInvoke(this, null, null, null);
                }
            }
        }
    }
}
