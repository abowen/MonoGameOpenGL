using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameOpenGL.Entities
{
    public class Health : Sprite
    {
        public readonly int LifeNumber;

        public Health(Texture2D texture2D, Vector2 location, int lifeNumber, GameLayer gameLayer) :base(texture2D, location, gameLayer)
        {
            LifeNumber = lifeNumber;
        }

        protected override void CheckBounds()
        {            
        }
    }
}
