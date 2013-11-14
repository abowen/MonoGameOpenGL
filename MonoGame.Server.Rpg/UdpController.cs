using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Helpers;
using MonoGame.Networking;

namespace MonoGame.Server.Rpg
{
    public class UdpController : Microsoft.Xna.Framework.Game
    {
        private readonly Dictionary<Keys, InputAction> _keyboardMappings;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public UdpController(Dictionary<Keys, InputAction> keyboardMappings, int serverPort)
        {
            _graphics = new GraphicsDeviceManager(this);            

            _keyboardMappings = keyboardMappings;
            _serverPort = serverPort;
            _broadcastClient = new BroadcastClient();
            IsListening = true;
        }

        public bool IsListening = false;
        private readonly BroadcastClient _broadcastClient;
        private readonly int _serverPort;

        private void SendUpdate(params byte[] items)
        {
            _broadcastClient.Send(_serverPort, items);
        }

        public void BroadcastInputs()
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();
            var direction = InputHelper.DirectionFromMapping(keysPressed, _keyboardMappings);
            var bytes = ConvertInputToBytes(direction, keysPressed.Contains(Keys.Space));            
            SendUpdate(bytes);

            IsListening = !keysPressed.Contains(Keys.Escape);
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
            // Create a new SpriteBatch, which can be used to draw textures.            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }


        private static byte[] ConvertInputToBytes(Vector2 direction, bool spaceBar)
        {
            var bytes = new List<byte>
            {
                ConvertFloatToByte(direction.X),
                ConvertFloatToByte(direction.Y),
                ConvertBoolToByte(spaceBar)
            };

            return bytes.ToArray();
        }

        private static byte ConvertBoolToByte(bool value)
        {
            return value ? (byte)255 : (byte)0;
        }

        // TODO: Complete this method
        private static byte ConvertFloatToByte(float value, float minimumRange = -1, float maximumRange = 1)
        {
            Contract.Assert(value >= minimumRange && value <= maximumRange);
            var output = (byte)((value + 1) * 255);
            return output;
        }
    }
}
