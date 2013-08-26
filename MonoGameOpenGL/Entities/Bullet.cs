using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.Enums;
using MonoGameOpenGL.Extensions;

namespace MonoGameOpenGL.Entities
{
    internal class Bullet : Sprite
    {
        public Bullet(Texture2D texture, Vector2 location, FaceDirection faceDirection)
            : base(texture, location)
        {
            Speed = 2;
            MovementDirection = faceDirection.GetVector2();
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
