using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;
using MonoGame.Graphics.Common;

namespace MonoGame.Common.Components
{
    public class TextComponent : SimpleComponent, ISimpleDrawable
    {
        private readonly CharacterMapping _characterMapping;
        public string Text;

        // TODO: Allow for forced upper or lower case
        public TextComponent(CharacterMapping characterMapping, string text)
        {
            _characterMapping = characterMapping;
            Text = text;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var location = Owner.TopLeft;
            var width = _characterMapping.Width;
            foreach (var character in Text.ToCharArray())
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
