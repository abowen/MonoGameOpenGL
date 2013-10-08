using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.Enums;
using MonoGameOpenGL.Infrastructure;

namespace MonoGameOpenGL.Entities
{
    public class Health : Sprite
    {
        public readonly int LifeNumber;

        public Health(Texture2D texture2D, Vector2 location, int lifeNumber, GameLayer gameLayer) :base(texture2D, location, FaceDirection.None, gameLayer)
        {
            LifeNumber = lifeNumber;
        }

        protected override void CheckBounds()
        {            
        }
    }
}
