using System.Collections.Generic;
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
        public readonly int Width;

        public Rectangle GetRectangle(char character)
        {
            return _characterLocation[character];
        }

        public CharacterMapping(Texture2D texture, int width, int height, string characters)
        {
            Texture = texture;
            Height = height;
            Width = width;
            var xLocation = 0;
            foreach (var character in characters.ToCharArray())
            {
                if (!_characterLocation.Keys.Contains(character))
                {
                    _characterLocation.Add(character, new Rectangle(xLocation, 0, width, height));
                }
                xLocation += width;
            }
        }
    }
}