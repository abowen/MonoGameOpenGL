using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;
using MonoGame.Common.Networking;
using MonoGame.Graphics.Common;

namespace MonoGame.Game.Surfing
{
    public class SurfingGame : ISimpleGame
    {        
        protected readonly Stack<GameLevel> Levels = new Stack<GameLevel>();

        public SurfingGame(GameWindow window, ContentManager contentManager)
        {
            FontGraphics.LoadContent(contentManager);
            GeneralGraphics.LoadContent(contentManager);

            GameConstants.ScreenBoundary = new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height);

            var level = new SurfLevel();
            Levels.Push(level);
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
    }
}
