using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Graphics.Common;

namespace MonoGame.Graphics.Rpg
{
    public class RpgGraphics
    {
        public static SpriteMapping GameboySpriteMapping;

        public static void LoadContent(ContentManager content)
        {
            content.RootDirectory = @".\";

            var digifontTexture = content.Load<Texture2D>("gameboy");
           
            GameboySpriteMapping = new SpriteMapping(digifontTexture, 16, 16, @".\gameboy.csv");
        }
    }
}
