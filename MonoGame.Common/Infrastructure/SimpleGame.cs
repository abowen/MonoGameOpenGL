using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Interfaces;
using MonoGame.Common.Networking;

namespace MonoGame.Common.Infrastructure
{
    public abstract class SimpleGame : ISimpleGame
    {
        public SimpleGame(GameWindow window, ContentManager contentManager)
        {
            GameConstants.ScreenBoundary = new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height);
        }

        protected readonly Stack<GameLevel> Levels = new Stack<GameLevel>();

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

        public void Update(NetworkMessage message)
        {
            var networkUpdate = ActiveGameLevel as ISimpleNetworking;
            if (networkUpdate != null)
            {
                networkUpdate.Update(message);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (ActiveGameLevel != null)
            {
                ActiveGameLevel.Draw(spriteBatch);
            }
        }

        public void Enable()
        {
            IsEnabled = true;
        }

        public void Disable()
        {
            IsEnabled = false;
        }

        public bool IsEnabled { get; private set; }

        public void ChangeLevel(GameLevel level)
        {
            throw new System.NotImplementedException();
        }

        public void LeaveLevel()
        {
            throw new System.NotImplementedException();
        }
    }
}
