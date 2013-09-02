using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.Enums;
using MonoGameOpenGL.Extensions;

namespace MonoGameOpenGL.Entities
{
    internal class Bullet : Sprite
    {
        public Bullet(Texture2D texture, Vector2 location, FaceDirection faceDirection, GameLayer gameLayer)
            : base(texture, location, faceDirection, gameLayer)
        {
            Speed = 2;            
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
