using System;
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
    public class InputComponent : SimpleComponent, ISimpleUpdateable
    {
        public InputComponent(Dictionary<Keys, InputAction> keyboardMappings, IKeyboardInput keyboardInput, params IMovementComponent[] movementComponent)
        {
            _keyboardMappings = keyboardMappings;            
            
            Contract.Assert(movementComponent != null, "InputComponent has a dependency on the MovementComponent");
            _movementComponent = movementComponent;
            _keyboardInput = keyboardInput;            
        }

        public InputComponent(Dictionary<Buttons, InputAction> buttonMappings, IButtonInput buttonInput, params IMovementComponent[] movementComponent)
        {            
            _buttonMappings = buttonMappings;

            Contract.Assert(movementComponent != null, "InputComponent has a dependency on the MovementComponent");
            _movementComponent = movementComponent;            
            _buttonInput = buttonInput;
        }

        public override void SetOwner(GameObject owner)
        {
            base.SetOwner(owner);
            // Delayed set in case the input methods are not known until run time
            if (_movementComponent == null || !_movementComponent.Any())
            {
                _movementComponent = Owner.MovementComponents.ToArray();
            }
        }


        private readonly Dictionary<Keys, InputAction> _keyboardMappings;
        private readonly Dictionary<Buttons, InputAction> _buttonMappings;        
        private readonly IKeyboardInput _keyboardInput;
        private readonly IButtonInput _buttonInput;
        private IMovementComponent[] _movementComponent;

        private double _elapsedTimeMilliseconds;

        public void Update(GameTime gameTime)
        {
            if (_keyboardMappings != null || _buttonMappings != null)
            {
                var firePressed = false;
                if (_keyboardMappings != null)
                {
                    var pressed = _keyboardInput.PressedKeys;
                    Array.ForEach(_movementComponent, movement => movement.InputDirection = InputHelper.DirectionFromMapping(pressed, _keyboardMappings));
                    
                    firePressed = pressed.Any(k => k == Keys.Space);
                }
                else if (_buttonMappings != null)
                {
                    var pressed = _buttonInput.ButtonsPressed;
                    Array.ForEach(_movementComponent, movement => movement.InputDirection = InputHelper.DirectionFromMapping(pressed, _buttonMappings));                    
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
