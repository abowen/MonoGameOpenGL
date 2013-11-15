using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Enums;
using MonoGame.Common.Infrastructure;

namespace MonoGame.Common.Entities
{
    internal class StaticSprite : Sprite
    {
        public StaticSprite(Texture2D texture, Vector2 location, FaceDirection faceDirection, GameLayer gameLayer)
            : base(texture, location, faceDirection, gameLayer)
        {            
            Speed = 1;            
        }        
        
        protected override void CheckBounds()
        {            
        }
    }
}
