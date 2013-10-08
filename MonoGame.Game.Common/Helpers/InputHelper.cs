using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Game.Common.Enums;

namespace MonoGame.Game.Common.Helpers
{
    public static class InputHelper
    {
        public static Vector2 DirectionFromMapping(Keys[] keys, Dictionary<Keys, InputAction> mapping)
        {
            Contract.Assert(keys != null, "Failed to pass input parameter");

            var direction = Vector2.Zero;
            foreach (var key in keys)
            {
                if (mapping.ContainsKey(key))
                {
                    var faceDirection = mapping[key];
                    direction = Direction(faceDirection, direction);
                }
            }
            return direction;
        }

        public static Vector2 DirectionFromMapping(Buttons[] keys, Dictionary<Buttons, InputAction> mapping)
        {
            Contract.Assert(keys != null, "Failed to pass input parameter");

            var direction = Vector2.Zero;
            foreach (var key in keys)
            {
                if (mapping.ContainsKey(key))
                {
                    var faceDirection = mapping[key];
                    direction = Direction(faceDirection, direction);
                }
            }
            return direction;
        }

        private static Vector2 Direction(InputAction inputAction, Vector2 direction)
        {            
            switch (inputAction)
            {
                case InputAction.Up:
                    direction += new Vector2(0, -1);
                    break;
                case InputAction.Down:
                    direction += new Vector2(0, 1);
                    break;
                case InputAction.Right:
                    direction += new Vector2(1, 0);
                    break;
                case InputAction.Left:
                    direction += new Vector2(-1, 0);
                    break;
            }
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
            return direction;
        }

        public static Dictionary<Keys, InputAction> KeyboardMappedKey()
        {
            var dictionary = new Dictionary<Keys, InputAction>
            {
                {Keys.Left, InputAction.Left},
                {Keys.Right, InputAction.Right},
                {Keys.Up, InputAction.Up},
                {Keys.Down, InputAction.Down},
                {Keys.Space, InputAction.Fire}
            };
            return dictionary;
        }

        public static Dictionary<Buttons, InputAction> GamepadMappedKey()
        {
            var dictionary = new Dictionary<Buttons, InputAction>
            {
                {Buttons.LeftThumbstickLeft, InputAction.Left},
                {Buttons.LeftThumbstickRight, InputAction.Right},
                {Buttons.LeftThumbstickUp, InputAction.Up},
                {Buttons.LeftThumbstickDown, InputAction.Down},
                {Buttons.RightTrigger, InputAction.Fire}
            };
            return dictionary;
        }
    }
}
