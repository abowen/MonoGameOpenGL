using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class LocalKeyboardComponent : SimpleComponent, IKeyboardInput
    {
        public IEnumerable<Keys> PressedKeys
        {
            get
            {
                return Keyboard.GetState().GetPressedKeys();
            }
        }
    }
}
