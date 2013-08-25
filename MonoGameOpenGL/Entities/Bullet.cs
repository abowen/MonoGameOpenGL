using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameOpenGL.Entities
{
    internal class Bullet : Sprite
    {
        public Bullet(Texture2D texture, Vector2 location, Rectangle screenBounds)
            : base(texture, location, screenBounds)
        {
        }        

        protected override void CheckBounds()
        {
            
        }
    }
}
