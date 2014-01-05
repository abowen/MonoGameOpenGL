using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.Input
{
    // Seems silly to add this component.
    public class KeyboardActionComponent : SimpleComponent, ISimpleUpdateable
    {
        private readonly Keys _key;
        private readonly Action<GameObject> _action;

        public KeyboardActionComponent(Keys key, Action<GameObject> action)
        {
            _key = key;
            _action = action;
        }        

        public void Update(GameTime gameTime)
        {
            var keysPressed = Keyboard.GetState().IsKeyDown(_key);
            if (keysPressed)
            {
                _action.Invoke(Owner);
            }
        }
    }
}
