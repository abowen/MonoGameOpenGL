using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;
using MonoGame.Graphics.Common;

namespace MonoGame.Common.Components
{
    public class SpriteMappingComponent : ISimpleComponent, ISimpleDrawable
    {
        private readonly SpriteMapping _spriteMapping;
        private readonly string _spriteName;
        private readonly Vector2 _relativeLocation;

        public GameObject Owner { get; set; }

        public SpriteMappingComponent(SpriteMapping spriteMapping, string spriteName, Vector2 relativeLocation)
        {
            _spriteMapping = spriteMapping;
            _spriteName = spriteName;
            _relativeLocation = relativeLocation;            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_spriteMapping.Texture, Owner.TopLeft + _relativeLocation, _spriteMapping.GetRectangle(_spriteName), Color.White);
        }   
    }
}
