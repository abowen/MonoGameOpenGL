using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;
using MonoGame.Common.Networking;

namespace MonoGame.Common.Components
{
    public class NetworkKeyboardComponent : ISimpleComponent, ISimpleNetworking, IKeyboardInput
    {
        public NetworkKeyboardComponent(Func<IEnumerable<byte>, IEnumerable<Keys>> networkMessageEncoding)
        {
            _networkMessageEncoding = networkMessageEncoding;
        }

        private readonly Func<IEnumerable<byte>, IEnumerable<Keys>> _networkMessageEncoding;
      
        private IEnumerable<Keys> _lastKnownKeys = new List<Keys>();

        public GameObject Owner { get; private set; }

        public void SetOwner(GameObject owner)
        {
            Owner = owner;
        }

        public void Update(NetworkMessage message)
        {
            _lastKnownKeys = _networkMessageEncoding(message.Bytes);
        }

        public IEnumerable<Keys> PressedKeys
        {
            get
            {
                return _lastKnownKeys;
            }
        }
    }
}
