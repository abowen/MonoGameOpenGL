using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameOpenGL.Entities
{
    internal class Bullet : Sprite
    {
        public Bullet(Texture2D texture, Vector2 location)
            : base(texture, location)
        {
            Direction = new Vector2(2, 0);
        }        

        protected override void CheckBounds()
        {
            if (Location.X > GameConstants.ScreenBoundary.Width)
            {
                IsRemoved = true;
            }
        }
    }
}
