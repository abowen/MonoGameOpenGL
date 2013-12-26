using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Graphics.Common
{
    public class GeneralGraphics
    {

        public static Texture2D WhiteCubeAsset { get; private set; }
        public static Texture2D TransparentCubeAsset { get; private set; }
        public static Texture2D BlueCubeAsset { get; private set; }
        public static Texture2D LightBlueCubeAsset { get; private set; }
 
        public static void LoadContent(ContentManager content)
        {
            content.RootDirectory = @".\General";

            WhiteCubeAsset = content.Load<Texture2D>("WhiteCube_8x8");
            TransparentCubeAsset = content.Load<Texture2D>("TransparentCube_8x8");
            BlueCubeAsset = content.Load<Texture2D>("BlueCube_8x8");
            LightBlueCubeAsset = content.Load<Texture2D>("LightBlueCube_8x8");
        }
    }
}
