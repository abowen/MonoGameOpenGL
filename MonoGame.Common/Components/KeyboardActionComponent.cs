using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    // Seems silly to add this component.
    public class KeyboardActionComponent : SimpleComponent, ISimpleUpdateable
    {
        private readonly Keys _key;
        private readonly Action _action;


        public KeyboardActionComponent(Keys key, Action action)
        {
            _key = key;
            _action = action;
        }        

        public void Update(GameTime gameTime)
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();
            if (keysPressed.Contains(_key))
            {
                _action.Invoke();
            }
        }
    }
}
