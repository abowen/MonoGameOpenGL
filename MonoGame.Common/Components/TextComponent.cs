﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Interfaces;
using MonoGame.Graphics.Common;

namespace MonoGame.Common.Components
{
    public class TextComponent : SimpleComponent, ISimpleDrawable
    {
        private readonly CharacterMapping _characterMapping;
        public string Text;
        private readonly int _scale;
        private readonly Vector2 _relativePosition;

        
        public TextComponent(CharacterMapping characterMapping, string text, Vector2? relativePosition = null, int scale = 1)
        {
            _characterMapping = characterMapping;
            Text = text;
            _scale = scale;
            _relativePosition = relativePosition ?? Vector2.Zero;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var location = Owner.TopLeft;
            var width = _characterMapping.Width * _scale;
            foreach (var character in Text.ToCharArray())
            {
                if (!char.IsWhiteSpace(character))
                {
                    spriteBatch.Draw(_characterMapping.Texture, location + _relativePosition, _characterMapping.GetRectangle(character), Color.White, 0, Vector2.Zero, _scale, SpriteEffects.None, 0);
                }
                location.X += width;
            }
        }
    }
}
