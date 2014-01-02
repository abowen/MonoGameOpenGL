using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Components;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Managers
{
    public class PlayerManager : IManager
    {
        public PlayerManager()
        {
            _buttonComponent = new LocalButtonComponent();
        }

        private readonly LocalButtonComponent _buttonComponent;
        private readonly Dictionary<Keys, Action> KeyboardPlayerListeners = new Dictionary<Keys, Action>();
        private readonly Dictionary<Buttons, Action> ButtonPlayerListeners = new Dictionary<Buttons, Action>();

        public void AddPlayerListener(Keys input, Action action)
        {
            KeyboardPlayerListeners.Add(input, action);
        }

        public void AddPlayerListener(Buttons input, Action action)
        {
            ButtonPlayerListeners.Add(input, action);
        }
        
        public void Update(GameTime gameTime)
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();
            var tempKeyboardListener = KeyboardPlayerListeners.ToList();
            foreach (var listener in tempKeyboardListener)
            {
                if (keysPressed.Contains(listener.Key))
                {
                    listener.Value.Invoke();
                    KeyboardPlayerListeners.Remove(listener.Key);
                }
            }

            var buttonsPressed = _buttonComponent.ButtonsPressed;
            var tempButtonListener = ButtonPlayerListeners.ToList();
            foreach (var listener in tempButtonListener)
            {
                if (buttonsPressed.Contains(listener.Key))
                {
                    listener.Value.Invoke();
                    ButtonPlayerListeners.Remove(listener.Key);
                }
            }            
        }
    }
}
