using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;
using MonoGame.Graphics.Common;

namespace MonoGame.Common.Components.Graphics
{
    public class SpriteMappingsComponent : SimpleComponent, ISimpleDrawable
    {
        private readonly SpriteMapping _spriteMapping;
        private readonly string[] _spriteNames;
        private readonly Vector2 _relativeLocation;
        private readonly ICounterComponent _counterIncrement;
        private readonly Vector2 _drawScale;

        public SpriteMappingsComponent(SpriteMapping spriteMapping, string[] spriteNames, Vector2 relativeLocation, ICounterComponent counterIncrement, Vector2? drawScale = null)
        {
            _spriteMapping = spriteMapping;
            _spriteNames = spriteNames;
            _relativeLocation = relativeLocation;
            _counterIncrement = counterIncrement;
            _drawScale = drawScale ?? new Vector2(1, 1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var spriteName = _spriteNames[_counterIncrement.CurrentValue];
            var drawScale = _drawScale * GameConstants.Scale;
            var locationScaled = (Owner.TopLeft + _relativeLocation) * GameConstants.Scale;
            spriteBatch.Draw(_spriteMapping.Texture, locationScaled, _spriteMapping.GetRectangle(spriteName), Color.White, 0, Vector2.Zero, drawScale, SpriteEffects.None, 0);
        }
    }
}
