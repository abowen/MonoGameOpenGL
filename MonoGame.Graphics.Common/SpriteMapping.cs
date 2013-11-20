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
            var rectangle = _spriteRectangles[spriteName];
            return rectangle;
        }

        public SpriteMapping(Texture2D texture, int width, int height, string csvFile)
        {
            Texture = texture;
            Height = height;
            Width = width;

            var spriteNames = ReadCsv(csvFile);
            AssignSpriteNameToRectangles(spriteNames);
        }

        private static List<List<string>> ReadCsv(string csvFile)
        {
            var lines = System.IO.File.ReadAllLines(csvFile);
            var spriteNames = new List<List<string>>();
            foreach (var line in lines)
            {
                var sprites = new List<string>();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    sprites = line.Split(',').ToList();
                }
                spriteNames.Add(sprites);
            }
            return spriteNames;
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