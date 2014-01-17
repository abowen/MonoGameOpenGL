using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Entities;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;
using MonoGame.Graphics.Common;

namespace MonoGame.Common.Components.Graphics
{
    public class TextComponent : SimpleComponent, ISimpleDrawable
    {
        private readonly CharacterMapping _characterMapping;
        private readonly ICounterComponent _counterComponent;
        private readonly Func<GameObject, string> _stringFunc;
        public string Text;
        private readonly int _drawScale;
        private readonly Vector2 _relativePosition;

        
        public TextComponent(CharacterMapping characterMapping, string text, Vector2? relativePosition = null, int drawScale = 1)
        {
            Contract.Assert(characterMapping != null);
            _characterMapping = characterMapping;
            Text = text;
            _drawScale = drawScale;
            _relativePosition = relativePosition ?? Vector2.Zero;
        }

        public TextComponent(CharacterMapping characterMapping, ICounterComponent counterComponent, Vector2? relativePosition = null, int drawScale = 1)
        {
            Contract.Assert(characterMapping != null);
            _characterMapping = characterMapping;
            _counterComponent = counterComponent;            
            _drawScale = drawScale;
            _relativePosition = relativePosition ?? Vector2.Zero;
        }

        public TextComponent(CharacterMapping characterMapping, Func<GameObject, string> stringFunc, Vector2? relativePosition = null, int drawScale = 1)
        {
            Contract.Assert(characterMapping != null);
            _characterMapping = characterMapping;
            _stringFunc = stringFunc;            
            _drawScale = drawScale;
            _relativePosition = relativePosition ?? Vector2.Zero;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_stringFunc != null)
            {
                Text = _stringFunc(Owner);
            }
            else if (_counterComponent != null)
            {
                Text = _counterComponent.CurrentValue.ToString(CultureInfo.InvariantCulture);
            }

            var location = Owner.TopLeft;
            var width = _characterMapping.Width * _drawScale;
            foreach (var character in Text.ToCharArray())
            {
                if (!char.IsWhiteSpace(character))
                {
                    var locationScaled = (location + _relativePosition)*GameConstants.Scale;
                    var drawScale = _drawScale*GameConstants.Scale;                    
                    spriteBatch.Draw(_characterMapping.Texture, locationScaled, _characterMapping.GetRectangle(character), Color.White, 0, Vector2.Zero, drawScale, SpriteEffects.None, 0);
                }
                location.X += width;
            }
        }
    }
}
