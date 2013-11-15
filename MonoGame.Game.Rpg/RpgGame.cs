using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Infrastructure;
using Microsoft.Xna.Framework;
using MonoGame.Game.Space;
using MonoGame.Graphics.Space;
using MonoGame.Networking;
using MonoGame.Server;

namespace MonoGame.Game.Rpg
{
    public class RpgGame
    {
        protected readonly Stack<GameLevel> Levels = new Stack<GameLevel>();

        public RpgGame(GameWindow window, ContentManager contentManager)
        {
            SpaceGraphics.LoadSpaceContent(contentManager);
            GameConstants.ScreenBoundary = new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height);
            var worldLevel = new WorldLevel();
            Levels.Push(worldLevel);
            Initialize();
        }

        private void Initialize()
        {            
            var broadcastClient = new BroadcastClient();
            
            while (broadcastClient.IsListening)
            {
                if (broadcastClient.MessagesReceived.Any())
                {
                    
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
