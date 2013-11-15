using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;

namespace MonoGame.Server
{
    // TODO: Remove ISimpleComponent aspect
    public class NetworkKeyboardComponent : ISimpleComponent, INetworkComponent, IKeyboardInput
    {
        public NetworkKeyboardComponent(Func<IEnumerable<byte>, IEnumerable<Keys>> networkMessageEncoding)
        {
            _networkMessageEncoding = networkMessageEncoding;
        }

        private readonly Func<IEnumerable<byte>, IEnumerable<Keys>> _networkMessageEncoding;
      
        private IEnumerable<Keys> _lastKnownKeys = new List<Keys>();

        public GameObject Owner { get; set; }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch gameTime)
        {
            
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
