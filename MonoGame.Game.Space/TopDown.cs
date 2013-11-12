using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Game.Common.Infrastructure;
using MonoGame.Graphics.Space;

namespace MonoGame.Game.Space
{
    public class TopDown
    {
        protected readonly Stack<GameLevel> Levels = new Stack<GameLevel>();
        private readonly Stopwatch _stopWatch = new Stopwatch();

        public TopDown(GameWindow window, ContentManager contentManager)
        {
            _stopWatch.Start();
            SpaceGraphics.LoadSpaceContent(contentManager);
            GameConstants.ScreenBoundary = new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height);
            var newLevel = new SpaceLevel();
            Levels.Push(newLevel);
        }

        protected GameLevel ActiveGameLevel
        {
            get
            {
                return Levels.First();
            }
        }

        public void Update(GameTime gameTime)
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();
            if (keysPressed.Contains(Keys.Tab) && _stopWatch.ElapsedMilliseconds > 200)
            {
                _stopWatch.Restart();
                if (ActiveGameLevel.GetType() == typeof (SpaceLevel))
                {
                    Levels.Push(new InventoryLevel());
                }
                else
                {
                    Levels.Pop();
                }
            }
            ActiveGameLevel.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ActiveGameLevel.Draw(spriteBatch);
        }
    }
}
