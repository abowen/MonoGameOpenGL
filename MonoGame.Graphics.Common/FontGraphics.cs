using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Graphics.Common
{
    public class FontGraphics
    {
        public static CharacterMapping BloxxitFont_8X8;
        public static CharacterMapping DigifontFont_16X16;
        public static CharacterMapping PropertialFont_8X8;

        public static void LoadContent(ContentManager content)
        {
            content.RootDirectory = @".\Fonts";

            var bloxxitTexture = content.Load<Texture2D>("bloxxit_8x8");
            BloxxitFont_8X8 = new CharacterMapping(bloxxitTexture, 8, 8, StringHelper.Base26UpperChars);

            var digifontTexture = content.Load<Texture2D>("digifont_16x16");
            var characterString = @" !""© % '() +,-./0123456789:;<=>?@" + StringHelper.Base26UpperChars;
            DigifontFont_16X16 = new CharacterMapping(digifontTexture, 16, 16, characterString);

            var propertialTexture = content.Load<Texture2D>("proportional_8x8");
            var propertialCharacters = @" !""#$%&'()*+,-./0123456789:;{=}?@" + StringHelper.Base26UpperChars + "[\\]^_'{|}";
            PropertialFont_8X8 = new CharacterMapping(propertialTexture, 8, 8, propertialCharacters, true);
        }
    }
}
