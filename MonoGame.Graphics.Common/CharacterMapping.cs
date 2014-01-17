using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Graphics.Common
{
    public class CharacterMapping
    {
        private readonly Dictionary<char, Rectangle> _characterLocation = new Dictionary<char, Rectangle>();

        public readonly Texture2D Texture;
        public readonly int Height;
        private readonly bool _isUpperCaseOnly;
        public readonly int Width;

        public Rectangle GetRectangle(char character)
        {
            //if (_isUpperCaseOnly)
            //{
            //    character = character.ToString(CultureInfo.InvariantCulture).ToUpperInvariant().ToCharArray()[0];
            //}
            return _characterLocation[character];
        }

        public CharacterMapping(Texture2D texture, int width, int height, string characters, bool isVertical = false, bool isUpperCaseOnly = false)
        {
            Texture = texture;
            Height = height;
            _isUpperCaseOnly = isUpperCaseOnly;
            Width = width;
            var xLocation = 0;
            var yLocation = 0;
            foreach (var character in characters.ToCharArray())
            {
                if (!_characterLocation.Keys.Contains(character))
                {
                    _characterLocation.Add(character, new Rectangle(xLocation, yLocation, width, height));
                }
                if (isVertical)
                {
                    yLocation += height;
                }
                else
                {
                    xLocation += width;
                }
                
            }
        }
    }
}