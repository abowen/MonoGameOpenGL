using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Networking;

namespace MonoGame.Server
{
    public class UdpController : Game
    {
        public UdpController(int serverPort, Func<IEnumerable<Keys>, IEnumerable<byte>> keyboardMappingsFunc)
        {
            _graphics = new GraphicsDeviceManager(this);            
            
            _serverPort = serverPort;
            _keyboardMappingsFunc = keyboardMappingsFunc;
            _broadcastClient = new BroadcastClient();            
        }        

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly BroadcastClient _broadcastClient;
        private readonly int _serverPort;
        private readonly Func<IEnumerable<Keys>, IEnumerable<byte>> _keyboardMappingsFunc;

        public bool IsListening
        {
            get
            {
                return _broadcastClient.IsListening;
            }
        }

        private void SendUpdate(params byte[] items)
        {
            _broadcastClient.Send(_serverPort, items);
        }

        public void BroadcastInputs()
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();
            var bytes = _keyboardMappingsFunc(keysPressed);
            SendUpdate(bytes.ToArray());

            _broadcastClient.IsListening = !keysPressed.Contains(Keys.Escape);
        }

        protected override void Update(GameTime gameTime)
        {
            BroadcastInputs();
            if (!IsListening)
            {
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();            
            _spriteBatch.End();
        }

        protected override void LoadContent()
        {            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }
    }
}
