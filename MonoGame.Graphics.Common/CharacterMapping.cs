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

        public CharacterMapping(Texture2D texture, int width, int height, string characters, bool isVertical = false)
        {
            Texture = texture;
            Height = height;
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