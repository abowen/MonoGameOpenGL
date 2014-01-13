using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Common.Components.Graphics;
using MonoGame.Common.Entities;
using MonoGame.Common.Infrastructure;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Common.Helpers;

namespace MonoGame.Common.Managers
{
    public class TileGenerator
    {        
        public static void GenerateFromCsv(string csv, SpriteMapping spriteMapping, GameLayer layer)
        {            
            var spriteNames = CsvHelper.ReadCsv(csv);
            var tiles = TransformNamesToVectors(spriteNames, spriteMapping.Width, spriteMapping.Height);
            
            foreach (var tile in tiles)
            {
                var spriteName = tile.Value;                
                var gameObject = new GameObject("Tile", tile.Key);
                var sprite = new SpriteMappingComponent(spriteMapping, spriteName);
                gameObject.AddComponent(sprite);
                layer.AddGameObject(gameObject);
            }
        }

        private static Dictionary<Vector2, string> TransformNamesToVectors(IEnumerable<IEnumerable<string>> namesList, int width, int height)
        {
            var namedDimensions = new Dictionary<Vector2, string>();
            var xLocation = 0;
            var yLocation = 0;
            foreach (var names in namesList)
            {
                foreach (var name in names)
                {
                    namedDimensions.Add(new Vector2(xLocation, yLocation), name);
                    xLocation += width;
                }
                yLocation += height;
                xLocation = 0;
            }
            return namedDimensions;
        }
    }
}