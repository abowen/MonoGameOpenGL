using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameOpenGL.Entities
{
    internal class Bullet : Sprite
    {
        public Bullet(Texture2D texture, Vector2 location)
            : base(texture, location)
        {
            this.Velocity.X = 2;
        }

        public event EventHandler BulletOutOfBounds;
        
        protected override void CheckBounds()
        {
            if (Location.X > GameConstants.ScreenBoundary.Width)
            {
                if (BulletOutOfBounds != null)
                {
                    BulletOutOfBounds.BeginInvoke(this, null, null, null);
                }
            }
        }
    }
}
