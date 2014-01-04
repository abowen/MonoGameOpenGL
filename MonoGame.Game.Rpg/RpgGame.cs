using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;
using Microsoft.Xna.Framework;
using MonoGame.Game.Rpg.Screens;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Rpg;

namespace MonoGame.Game.Rpg
{
    public class RpgGame : ISimpleGame
    {        
        protected readonly Stack<GameLevel> Levels = new Stack<GameLevel>();

        public RpgGame(GameWindow window, ContentManager contentManager)
        {
            FontGraphics.LoadContent(contentManager);            
            RpgGraphics.LoadContent(contentManager);

            GameConstants.ScreenBoundary = new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height);

            var startScreen = new StartScreen();
            Levels.Push(startScreen);
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

        public void Enable()
        {
            IsEnabled = true;
        }

        public void Disable()
        {
            IsEnabled = false;
        }

        public bool IsEnabled { get; private set; }
    }
}
