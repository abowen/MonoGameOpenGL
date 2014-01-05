using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Enums;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.Input
{
    // Seems silly to add this component.
    public class KeyboardEventComponent : SimpleComponent, ISimpleUpdateable
    {
        private readonly Keys _key;
        private readonly ObjectEvent _objectEvent;
        private readonly int _keyboardDelay;
        private readonly Stopwatch _stopwatch;

        public KeyboardEventComponent(Keys key, ObjectEvent objectEvent, int keyboardDelay = 100)
        {
            _stopwatch = Stopwatch.StartNew();
            _key = key;
            _objectEvent = objectEvent;
            _keyboardDelay = keyboardDelay;
        }

        public void Update(GameTime gameTime)
        {
            if (_stopwatch.ElapsedMilliseconds > _keyboardDelay)
            {
                var keysPressed = Keyboard.GetState().GetPressedKeys();
                if (keysPressed.Contains(_key))
                {
                    Owner.Event(_objectEvent);
                }
                _stopwatch.Restart();
            }
        }
    }
}
