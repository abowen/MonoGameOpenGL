using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private readonly Vector2 _scale;


        //public int Width { get; private set; }
        //public int Height { get; private set; }

        public SpriteMappingsComponent(SpriteMapping spriteMapping, string[] spriteNames, Vector2 relativeLocation, ICounterComponent counterIncrement, Vector2? scale = null)
        {            
            _spriteMapping = spriteMapping;
            _spriteNames = spriteNames;
            _relativeLocation = relativeLocation;
            _counterIncrement = counterIncrement;
            _scale = scale ?? new Vector2(1,1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var spriteName = _spriteNames[_counterIncrement.CurrentValue];
            spriteBatch.Draw(_spriteMapping.Texture, Owner.TopLeft + _relativeLocation, _spriteMapping.GetRectangle(spriteName), Color.White, 0, Vector2.Zero, _scale, SpriteEffects.None, 0);                       
        }
    }
}
