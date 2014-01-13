using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoGame.Graphics.Common.Helpers
{
    public class SpriteHelper
    {
        public static Tuple<string, Rectangle>[] TransformNamesToRectangles(IEnumerable<IEnumerable<string>> namesList, int width, int height)
        {
            var namedDimensions = new List<Tuple<string, Rectangle>>(0);
            var xLocation = 0;
            var yLocation = 0;
            foreach (var names in namesList)
            {
                foreach (var name in names)
                {                    
                    namedDimensions.Add(new Tuple<string, Rectangle>(name, new Rectangle(xLocation, yLocation, width, height)));
                    xLocation += width;
                }
                yLocation += height;
                xLocation = 0;
            }
            return namedDimensions.ToArray();
        }

    }
}
