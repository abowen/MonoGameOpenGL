using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Enums;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.Input
{    
    public class KeyboardEventComponent : SimpleComponent, ISimpleUpdateable
    {
        private readonly Keys _key;
        private readonly ObjectEvent _objectEvent;

        public KeyboardEventComponent(Keys key, ObjectEvent objectEvent)
        {
            _key = key;
            _objectEvent = objectEvent;
        }

        private bool _isPreviouslyPressed;

        public void Update(GameTime gameTime)
        {
            var keyPressed = Keyboard.GetState().IsKeyDown(_key);

            if (keyPressed && !_isPreviouslyPressed)
            {
                Owner.Event(_objectEvent);
            }

            _isPreviouslyPressed = keyPressed;
        }
    }
}
