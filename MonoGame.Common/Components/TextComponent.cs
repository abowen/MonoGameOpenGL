using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;
using MonoGame.Graphics.Common;

namespace MonoGame.Common.Components
{
    public class TextComponent : ISimpleComponent, ISimpleDrawable
    {
        private readonly CharacterMapping _characterMapping;
        private readonly string _text;

        // TODO: Allow for text to change
        // TODO: Allow for forced upper or lower case
        public TextComponent(CharacterMapping characterMapping, string text)
        {
            _characterMapping = characterMapping;
            _text = text;
        }

        public GameObject Owner { get; set; }
        public void Draw(SpriteBatch spriteBatch)
        {
            var location = Owner.TopLeft;
            var width = _characterMapping.Width;
            foreach (var character in _text.ToCharArray())
            {
                if (!char.IsWhiteSpace(character))
                {
                    spriteBatch.Draw(_characterMapping.Texture, location, _characterMapping.GetRectangle(character), Color.White);
                }
                location.X += width;
            }
        }
    }
}
