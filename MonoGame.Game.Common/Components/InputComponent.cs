using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Helpers;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Components
{
    public class InputComponent : IMonoGameComponent
    {
        public InputComponent(GameObject owner, Dictionary<Keys, InputAction> keyboardMappings, Dictionary<Keys, InputAction> buttonMappings, MovementComponent movementComponent)
        {
            Owner = owner;
            _keyboardMappings = keyboardMappings;
            _buttonMappings = buttonMappings;
            
            Contract.Assert(movementComponent != null, "InputComponent has a dependency on the MovementComponent");
            _movementComponent = movementComponent;
        }



        private readonly Dictionary<Keys, InputAction> _keyboardMappings;
        private readonly Dictionary<Keys, InputAction> _buttonMappings;
        private readonly MovementComponent _movementComponent;

        public GameObject Owner { get; set; }



        public void Update(GameTime gameTime)
        {
            if (_keyboardMappings != null || _buttonMappings != null)
            {
                var keysPressed = Keyboard.GetState().GetPressedKeys();
                if (_keyboardMappings != null)
                {
                    _movementComponent.Direction = InputHelper.DirectionFromMapping(keysPressed, _keyboardMappings);
                }
                else if (_buttonMappings != null)
                {
                    _movementComponent.Direction = InputHelper.DirectionFromMapping(keysPressed, _buttonMappings);
                }
            }
        }

        public void Draw(SpriteBatch gameTime)
        {
            
        }                
    }
}
