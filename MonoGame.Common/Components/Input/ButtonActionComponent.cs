using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.Input
{    
    public class ButtonActionComponent : SimpleComponent, ISimpleUpdateable
    {
        private readonly Buttons _button;
        private readonly Action<GameObject> _action;
        private readonly LocalButtonComponent _buttonComponent;

        public ButtonActionComponent(Buttons key, Action<GameObject> action)
        {
            _button = key;
            _action = action;
            _buttonComponent = new LocalButtonComponent();
        }        

        public void Update(GameTime gameTime)
        {
            var buttons = _buttonComponent.ButtonsPressed;
            if (buttons.Contains(_button))
            {
                _action.Invoke(Owner);
            }
        }
    }
}
