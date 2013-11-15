using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;
using Microsoft.Xna.Framework;
using MonoGame.Graphics.Space;
using MonoGame.Server;

namespace MonoGame.Game.Rpg
{
    public class RpgGame : INetworkGame, IGame
    {
        private readonly SpriteBatch _spriteBatch;
        protected readonly Stack<GameLevel> Levels = new Stack<GameLevel>();


        public RpgGame(GameWindow window, ContentManager contentManager, SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            SpaceGraphics.LoadSpaceContent(contentManager);
            GameConstants.ScreenBoundary = new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height);

            var worldLevel = new WorldLevel();
            Levels.Push(worldLevel);
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

        public void UpdateNetwork(NetworkMessage message)
        {
            var networkUpdate = ActiveGameLevel as INetworkGame;
            if (networkUpdate != null)
            {
                networkUpdate.UpdateNetwork(message);
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (ActiveGameLevel != null)
            {
                ActiveGameLevel.Draw(_spriteBatch);
            }
        }
    }
}
