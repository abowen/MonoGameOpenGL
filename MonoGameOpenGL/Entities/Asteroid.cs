using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameOpenGL.Entities
{
    internal class Asteroid : Sprite
    {
        public Asteroid(Texture2D texture, Vector2 location)
            : base(texture, location)
        {
            Direction = new Vector2(-1, 0);
        }        
        
        protected override void CheckBounds()
        {
            if (Location.X < 0)
            {
                IsRemoved = true;
            }
        }
    }
}
