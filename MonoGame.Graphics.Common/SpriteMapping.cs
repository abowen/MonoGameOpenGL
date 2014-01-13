using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Graphics.Common.Helpers;

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
            var rectangle = _spriteRectangles[spriteName];
            return rectangle;
        }

        public SpriteMapping(Texture2D texture, int width, int height, string csvFile)
        {
            Texture = texture;
            Height = height;
            Width = width;

            var spriteNames = CsvHelper.ReadCsv(csvFile);
            AssignSpriteNameToRectangles(spriteNames);
        }


        private void AssignSpriteNameToRectangles(IEnumerable<IEnumerable<string>> spriteNames)
        {
            var xLocation = 0;
            var yLocation = 0;
            foreach (var sprites in spriteNames)
            {                
                foreach (var sprite in sprites)
                {
                    if (!_spriteRectangles.Keys.Contains(sprite))
                    {
                        _spriteRectangles.Add(sprite, new Rectangle(xLocation, yLocation, Width, Height));
                    }
                    xLocation += Width;                   
                }
                yLocation += Height;
                xLocation = 0;
            }
        }
    }
}