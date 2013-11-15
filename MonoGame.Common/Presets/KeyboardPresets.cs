using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Helpers;

namespace MonoGame.Common.Presets
{
    public class KeyboardPresets
    {        
        public static IEnumerable<byte> BasicKeyboardMapping(IEnumerable<Keys> pressedKeys)
        {
            return InputHelper.MapKeysToBytes(BasicKeyboardKeys, pressedKeys);
        }

        public static IEnumerable<Keys> BasicReverseKeyboardMapping(IEnumerable<byte> returnedBytes)
        {
            return InputHelper.MapBytesToKeys(BasicKeyboardKeys, returnedBytes);
        }

        // TODO: Could make this Dynamic but including the ASCII in the byte
        public static IEnumerable<Keys> BasicKeyboardKeys = new List<Keys>
        {
            Keys.Left,
            Keys.Up,
            Keys.Right,
            Keys.Down,
            Keys.Space
        };
    }
}
