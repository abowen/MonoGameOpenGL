using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.Enums;
using MonoGameOpenGL.Extensions;

namespace MonoGameOpenGL.Entities
{
    internal class Planet : Sprite
    {
        public Planet(Texture2D texture, Vector2 location, FaceDirection faceDirection, GameLayer gameLayer)
            : base(texture, location, gameLayer)
        {
            MovementDirection = faceDirection.GetVector2();
            FaceDirection = faceDirection;
            Speed = 1;
            // TODO: Add feature that a planet can be blown up?
        }        
        
        protected override void CheckBounds()
        {            
        }
    }
}
