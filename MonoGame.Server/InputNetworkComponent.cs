using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Components;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Helpers;
using MonoGame.Common.Interfaces;

namespace MonoGame.Server
{
    public class InputNetworkComponent : IComponent, INetworkGame
    {
        public InputNetworkComponent(int listeningPort, Func<NetworkMessage, IEnumerable<Keys>> networkMessageEncoding, Dictionary<Keys, InputAction> keyboardMapping, Dictionary<Keys, InputAction> buttonMapping, MovementComponent movementComponent)
        {
            _keyboardMappings = keyboardMapping;
            _buttonMappings = buttonMapping;
            
            Contract.Assert(movementComponent != null, "InputComponent has a dependency on the MovementComponent");
            _listeningPort = listeningPort;
            _networkMessageEncoding = networkMessageEncoding;
            _movementComponent = movementComponent;
        }

        private readonly Dictionary<Keys, InputAction> _keyboardMappings;
        private readonly Dictionary<Keys, InputAction> _buttonMappings;
        private readonly int _listeningPort;
        private readonly Func<NetworkMessage, IEnumerable<Keys>> _networkMessageEncoding;
        private readonly MovementComponent _movementComponent;
        private IEnumerable<Keys> _lastKnownKeys;

        public GameObject Owner { get; set; }

        private double _elapsedTimeMilliseconds;

        public void Update(GameTime gameTime)
        {
            if (_keyboardMappings != null || _buttonMappings != null)
            {
                var keysPressed = _lastKnownKeys;
                if (_keyboardMappings != null)
                {
                    _movementComponent.Direction = InputHelper.DirectionFromMapping(keysPressed, _keyboardMappings);
                }
                else if (_buttonMappings != null)
                {
                    _movementComponent.Direction = InputHelper.DirectionFromMapping(keysPressed, _buttonMappings);
                }

                _elapsedTimeMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;

                if (keysPressed.Any(k => k == Keys.Space) && _elapsedTimeMilliseconds > 500)
                {
                    _elapsedTimeMilliseconds = 0;
                    Owner.Event(ObjectEvent.Fire);
                }
            }
        }

        public void Draw(SpriteBatch gameTime)
        {
            
        }

        public void UpdateNetwork(NetworkMessage message)
        {
            _lastKnownKeys = _networkMessageEncoding(message);
        }
    }
}
