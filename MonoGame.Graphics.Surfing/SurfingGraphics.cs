using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Graphics.Surfing
{
    public class SurfingGraphics
    {
        public static Texture2D SurfboardAsset { get; private set; }
        public static Texture2D WaveAsset { get; private set; }        

        public static void LoadContent(ContentManager content)
        {
            content.RootDirectory = @".\Graphics";

            SurfboardAsset = content.Load<Texture2D>("Surfboard");
            WaveAsset = content.Load<Texture2D>("Wave_8x8");
        }
    }
}
