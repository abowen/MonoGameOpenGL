using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Graphics.Common
{
    public class FontGraphics
    {
        private static string BloxxitFont = "bloxxit_8x8";

        public static Dictionary<string, CharacterMapping> Fonts = new Dictionary<string, CharacterMapping>();

        public static CharacterMapping BloxxitFont8X8;
       
        public static void LoadContent(ContentManager content)
        {
            content.RootDirectory = @".\Fonts";

            var texture2D = content.Load<Texture2D>(BloxxitFont);
            var bloxxitCharacterMapping = new CharacterMapping(texture2D, 8, 8, CharHelper.Base26UpperChars);
            Fonts.Add(BloxxitFont, bloxxitCharacterMapping);   
  
            BloxxitFont8X8 = Fonts[BloxxitFont];
        }
    }
}
