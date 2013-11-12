using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Infrastructure;
using Microsoft.Xna.Framework;
using MonoGame.Graphics.Space;
using MonoGame.Networking;
using MonoGame.Server;
using MonoGame.Server.Common;

namespace MonoGame.Game.Rpg
{
    public class RpgGame
    {
        protected readonly Stack<GameLevel> Levels = new Stack<GameLevel>();

        public RpgGame(GameWindow window, ContentManager contentManager)
        {
            SpaceGraphics.LoadSpaceContent(contentManager);
            GameConstants.ScreenBoundary = new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height);

            Initialize();
        }

        private void Initialize()
        {
            // Let the server fire up
            Thread.Sleep(1000);
            var broadcastClient = new BroadcastClient();

            broadcastClient.Send(Message.RequestClientId());
            while (broadcastClient.IsListening)
            {
                if (broadcastClient.MessagesReceived.Any())
                {
                    var message = broadcastClient.MessagesReceived.Dequeue();
                    if (message.MessageContent.CommandId == Message.CommandSendClientId)
                    {
                        broadcastClient.IsListening = false;
                    }
                }
            }
        }

        protected GameLevel ActiveGameLevel
        {
            get
            {
                return Levels.FirstOrDefault();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (ActiveGameLevel != null)
            {
                ActiveGameLevel.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (ActiveGameLevel != null)
            {
                ActiveGameLevel.Draw(spriteBatch);
            }
        }
    }
}
