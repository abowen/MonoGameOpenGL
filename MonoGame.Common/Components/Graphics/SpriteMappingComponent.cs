using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;
using MonoGame.Graphics.Common;

namespace MonoGame.Common.Components.Graphics
{
    public class SpriteMappingComponent : SimpleComponent, ISimpleDrawable
    {
        private readonly SpriteMapping _spriteMapping;
        private readonly string _spriteName;
        private readonly Vector2 _relativeLocation;
        private readonly float _drawScale;

        public SpriteMappingComponent(SpriteMapping spriteMapping, string spriteName, Vector2? relativeLocation = null, float drawScale = 1f)
        {
            _spriteMapping = spriteMapping;
            _spriteName = spriteName;
            _relativeLocation = relativeLocation ?? Vector2.Zero;
            _drawScale = drawScale;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var locationScaled = (Owner.TopLeft + _relativeLocation) * GameConstants.Scale;
            var drawScale = _drawScale * GameConstants.Scale;
            spriteBatch.Draw(_spriteMapping.Texture, locationScaled, _spriteMapping.GetRectangle(_spriteName), Color.White, 0, Vector2.Zero, drawScale, SpriteEffects.None, 0);            
        }   
    }
}
