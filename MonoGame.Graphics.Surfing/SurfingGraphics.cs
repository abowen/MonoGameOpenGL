﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Graphics.Surfing
{
    public class SurfingGraphics
    {
        public static Texture2D SurfboardAsset { get; private set; }
        public static Texture2D Wave_8x8_Asset { get; private set; }
        public static Texture2D Wave_8x100_Asset { get; private set; }        

        public static void LoadContent(ContentManager content)
        {
            content.RootDirectory = @".\Graphics";

            SurfboardAsset = content.Load<Texture2D>("Surfboard");
            Wave_8x8_Asset = content.Load<Texture2D>("Wave_8x8");
            Wave_8x100_Asset = content.Load<Texture2D>("Wave_8x100");
        }
    }
}
