using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.Enums;
using MonoGameOpenGL.Extensions;

namespace MonoGameOpenGL.Entities
{
    internal class Asteroid : Sprite
    {
        public Asteroid(Texture2D texture, Vector2 location, FaceDirection faceDirection)
            : base(texture, location)
        {
            MovementDirection = faceDirection.GetVector2();
            FaceDirection = faceDirection;
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
