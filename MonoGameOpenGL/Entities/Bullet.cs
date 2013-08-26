using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameOpenGL.Entities
{
    internal class Bullet : Sprite
    {
        public Bullet(Texture2D texture, Vector2 location, GameState gameState)
            : base(texture, location, gameState)
        {
            Direction = new Vector2(2, 0);
        }

        public event EventHandler OutOfBounds;

        protected override void CheckBounds()
        {
            if (Location.X > GameConstants.ScreenBoundary.Width)
            {
                if (OutOfBounds != null)
                {
                    OutOfBounds.BeginInvoke(this, null, null, null);
                }
            }
        }
    }
}
