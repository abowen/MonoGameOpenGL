﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;
using Microsoft.Xna.Framework;
using MonoGame.Common.Networking;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Rpg;
using MonoGame.Graphics.Space;

namespace MonoGame.Game.Rpg
{
    public class RpgGame : ISimpleNetworking, ISimpleGame
    {        
        protected readonly Stack<GameLevel> Levels = new Stack<GameLevel>();

        public RpgGame(GameWindow window, ContentManager contentManager)
        {
            FontGraphics.LoadContent(contentManager);
            SpaceGraphics.LoadSpaceContent(contentManager);
            RpgGraphics.LoadContent(contentManager);

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
