using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Helpers;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class InputComponent : ISimpleComponent, ISimpleUpdateable
    {
        public InputComponent(Dictionary<Keys, InputAction> keyboardMappings, IKeyboardInput keyboardInput, IMovementComponent movementComponent)
        {
            _keyboardMappings = keyboardMappings;            
            
            Contract.Assert(movementComponent != null, "InputComponent has a dependency on the MovementComponent");
            _movementComponent = movementComponent;
            _keyboardInput = keyboardInput;            
        }

        public InputComponent(Dictionary<Buttons, InputAction> buttonMappings, IButtonInput buttonInput, IMovementComponent movementComponent)
        {            
            _buttonMappings = buttonMappings;

            Contract.Assert(movementComponent != null, "InputComponent has a dependency on the MovementComponent");
            _movementComponent = movementComponent;            
            _buttonInput = buttonInput;
        }

        private readonly Dictionary<Keys, InputAction> _keyboardMappings;
        private readonly Dictionary<Buttons, InputAction> _buttonMappings;
        private readonly IMovementComponent _movementComponent;
        private readonly IKeyboardInput _keyboardInput;
        private readonly IButtonInput _buttonInput;

        public GameObject Owner { get; private set; }

        public void SetOwner(GameObject owner)
        {
            Owner = owner;
        }

        private double _elapsedTimeMilliseconds;

        public void Update(GameTime gameTime)
        {
            if (_keyboardMappings != null || _buttonMappings != null)
            {
                var firePressed = false;
                if (_keyboardMappings != null)
                {
                    var keysPressed = _keyboardInput.PressedKeys;
                    _movementComponent.InputDirection = InputHelper.DirectionFromMapping(keysPressed, _keyboardMappings);
                    firePressed = keysPressed.Any(k => k == Keys.Space);
                }
                else if (_buttonMappings != null)
                {
                    var buttonsPressed = _buttonInput.ButtonsPressed;
                    _movementComponent.InputDirection = InputHelper.DirectionFromMapping(buttonsPressed, _buttonMappings);
                }

                _elapsedTimeMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;

                if (firePressed && _elapsedTimeMilliseconds > 500)
                {
                    _elapsedTimeMilliseconds = 0;
                    Owner.Event(ObjectEvent.Fire);
                }
            }
        }            
    }
}
