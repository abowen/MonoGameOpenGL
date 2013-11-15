using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Infrastructure;

namespace MonoGame.Game.Common.Entities
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
