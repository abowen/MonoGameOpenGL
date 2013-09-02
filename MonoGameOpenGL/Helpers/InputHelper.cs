using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameOpenGL.Enums;

namespace MonoGameOpenGL.Helpers
{    
    public static class InputHelper
    {        
        public static Vector2 DirectionFromMapping(Keys[] keys, Dictionary<Keys, FaceDirection> mapping)
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

        public static Vector2 DirectionFromMapping(Buttons[] keys, Dictionary<Buttons, FaceDirection> mapping)
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

        private static Vector2 Direction(FaceDirection faceDirection, Vector2 direction)
        {
            switch (faceDirection)
            {
                case FaceDirection.Up:
                    direction += new Vector2(0, -1);
                    break;
                case FaceDirection.Down:
                    direction += new Vector2(0, 1);
                    break;
                case FaceDirection.Right:
                    direction += new Vector2(1, 0);
                    break;
                case FaceDirection.Left:
                    direction += new Vector2(-1, 0);
                    break;
            }
            direction.Normalize();
            return direction;
        }

        public static Dictionary<Keys, FaceDirection> KeyboardMappedKey()
        {
            var dictionary = new Dictionary<Keys, FaceDirection>
            {
                {Keys.Left, FaceDirection.Left},
                {Keys.Right, FaceDirection.Right},
                {Keys.Up, FaceDirection.Up},
                {Keys.Down, FaceDirection.Down}
            };
            return dictionary;
        }
        
        public static Dictionary<Buttons, FaceDirection> GamepadMappedKey()
        {            
            var dictionary = new Dictionary<Buttons, FaceDirection>
            {
                {Buttons.LeftThumbstickLeft, FaceDirection.Left},
                {Buttons.LeftThumbstickRight, FaceDirection.Right},
                {Buttons.LeftThumbstickUp, FaceDirection.Up},
                {Buttons.LeftThumbstickDown, FaceDirection.Down}
            };
            return dictionary;
        }
    }
}
