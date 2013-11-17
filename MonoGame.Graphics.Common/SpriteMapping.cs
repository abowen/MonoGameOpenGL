using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Graphics.Common
{
    public class SpriteMapping
    {
        private readonly Dictionary<string, Rectangle> _spriteRectangles = new Dictionary<string, Rectangle>();

        public readonly Texture2D Texture;
        public readonly int Height;
        public readonly int Width;

        public Rectangle GetRectangle(string spriteName)
        {
            return _spriteRectangles[spriteName];
        }

        public SpriteMapping(Texture2D texture, int width, int height, int column, IEnumerable<string> spriteNames)
        {
            Texture = texture;
            Height = height;
            Width = width;
            var xLocation = 0;
            var columnCount = 0;
            var yLocation = 0;
            foreach (var spriteName in spriteNames)
            {
                if (!_spriteRectangles.Keys.Contains(spriteName))
                {
                    _spriteRectangles.Add(spriteName, new Rectangle(xLocation, yLocation, width, height));
                }
                xLocation += width;
                columnCount++;
                if (columnCount == column)
                {
                    columnCount = 0;
                    yLocation += height;
                }
            }
        }
    }
}