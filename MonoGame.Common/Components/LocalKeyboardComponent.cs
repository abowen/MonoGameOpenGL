using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class LocalKeyboardComponent : ISimpleComponent, IKeyboardInput
    {        
        public GameObject Owner { get; set; }

        public IEnumerable<Keys> PressedKeys
        {
            get
            {
                return Keyboard.GetState().GetPressedKeys();
            }
        }
    }
}
