using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Graphics.Surfing
{
    public class SurfingGraphics
    {
        public static Texture2D Surfboard_Yellow_Asset { get; private set; }
        public static Texture2D Surfboard_White_Asset { get; private set; }
        public static Texture2D Surfboard_Large_White_Asset { get; private set; }
        public static Texture2D Wave_8x8_Asset { get; private set; }
        public static Texture2D Wave_8x100_Asset { get; private set; }
        public static Texture2D WaveBackgroundAsset { get; private set; }
        public static Texture2D CloudMajorAsset { get; private set; }
        public static Texture2D CloudMinorAsset { get; private set; }        

        public static void LoadContent(ContentManager content)
        {
            content.RootDirectory = @".\Graphics";

            Surfboard_White_Asset = content.Load<Texture2D>("Surfboard_White");
            Surfboard_Large_White_Asset = content.Load<Texture2D>("Surfboard_Large_White");
            Surfboard_Yellow_Asset = content.Load<Texture2D>("Surfboard_Yellow");
            Wave_8x8_Asset = content.Load<Texture2D>("Wave_8x8");
            Wave_8x100_Asset = content.Load<Texture2D>("Wave_8x100");
            CloudMajorAsset = content.Load<Texture2D>("CloudMajor");
            CloudMinorAsset = content.Load<Texture2D>("CloudMinor");
            WaveBackgroundAsset = content.Load<Texture2D>("WaveBackground");
        }
    }
}
