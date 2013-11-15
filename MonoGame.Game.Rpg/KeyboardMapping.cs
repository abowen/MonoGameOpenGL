using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using MonoGame.Game.Common.Helpers;
using MonoGame.Game.Common.Networking;

namespace MonoGame.Game.Rpg
{
    public class KeyboardMapping
    {
        public static IEnumerable<byte> ConvertKeysToBytes(IEnumerable<Keys> keys)
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
