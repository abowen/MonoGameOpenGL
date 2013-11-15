using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Data;
using MonoGame.Common.Helpers;

namespace MonoGame.Game.Rpg
{
    public class KeyboardMapping
    {
        // TODO: Can Offers a better experience for controllers.
        [Obsolete("Use Keyboard Presets until start including controllers")]
        public static IEnumerable<byte> ConvertKeysToCustomBytes(IEnumerable<Keys> keys)
        {
            var keyboardMappings = InputHelper.KeyboardMappedKey();
            var direction = InputHelper.DirectionFromMapping(keys, keyboardMappings);
            var isSpacePressed = keys.Contains(Keys.Space);

            var bytes = new List<byte>
            {
                DataConvertHelper.ConvertFloatToByte(direction.X),
                DataConvertHelper.ConvertFloatToByte(direction.Y),
                DataConvertHelper.ConvertBoolToByte(isSpacePressed)
            };

            return bytes.ToArray();
        }
    }
}
