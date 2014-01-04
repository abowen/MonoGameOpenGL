using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Interfaces;
using MonoGame.Graphics.Common;

namespace MonoGame.Common.Components
{
    public class SpriteMappingComponent : SimpleComponent, ISimpleDrawable
    {
        private readonly SpriteMapping _spriteMapping;
        private readonly string _spriteName;
        private readonly Vector2 _relativeLocation;
        private readonly float _scale;

        public SpriteMappingComponent(SpriteMapping spriteMapping, string spriteName, Vector2 relativeLocation, float scale = 1f)
        {
            _spriteMapping = spriteMapping;
            _spriteName = spriteName;
            _relativeLocation = relativeLocation;
            _scale = scale;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_spriteMapping.Texture, Owner.TopLeft + _relativeLocation, _spriteMapping.GetRectangle(_spriteName), Color.White, 0, Vector2.Zero, _scale, SpriteEffects.None, 0);            
        }   
    }
}
