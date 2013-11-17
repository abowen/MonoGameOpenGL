using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Graphics.Common
{
    public class FontGraphics
    {
        public static CharacterMapping BloxxitFont8X8;
        public static CharacterMapping DigifontFont16X16;

        public static void LoadContent(ContentManager content)
        {
            content.RootDirectory = @".\Fonts";

            var bloxxitTexture = content.Load<Texture2D>("bloxxit_8x8");
            BloxxitFont8X8 = new CharacterMapping(bloxxitTexture, 8, 8, StringHelper.Base26UpperChars);

            var digifontTexture = content.Load<Texture2D>("digifont_16x16");
            var characterString = @" !""© % '() +,-./0123456789:;<=>?@" + StringHelper.Base26UpperChars;
            DigifontFont16X16 = new CharacterMapping(digifontTexture, 16, 16, characterString);
        }
    }
}
