﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Graphics.Common
{
    public class GeneralGraphics
    {

        public static Texture2D WhiteCubeAsset { get; private set; }
 
        public static void LoadContent(ContentManager content)
        {
            content.RootDirectory = @".\General";

            WhiteCubeAsset = content.Load<Texture2D>("WhiteCube_8x8");
        }
    }
}
