using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Common.Components;
using MonoGame.Common.Components.Boundary;
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
                var tileNames = tile.Value;
                var tileArray = tileNames.Split(';');                
                var tileName = string.Format("Tile_{0}_{1}_{2}", tileNames, tile.Key.X, tile.Key.Y);
                var gameObject = new GameObject(tileName, tile.Key);

                var spriteName = tileArray[0];
                var sprite = new SpriteMappingComponent(spriteMapping, spriteName);

                if (tileArray.Count() > 1)
                {
                    var blockingProperty = tileArray[1];
                    if (blockingProperty.ToUpperInvariant() == "B")
                    {
                        var boundary = new BoundaryComponent(spriteMapping.Width, spriteMapping.Height);
                        gameObject.AddComponent(boundary);
                    }
                }
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