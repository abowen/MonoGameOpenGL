//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;

//namespace MonoGame.Game.Rpg
//{
//    public class RpgGame
//    {
//        protected readonly Stack<GameLevel> Levels = new Stack<GameLevel>();


//        public RpgGame(GameWindow window, ContentManager contentManager)
//        {
//            _stopWatch.Start();
//            SpaceGraphics.LoadSpaceContent(contentManager);
//            GameConstants.ScreenBoundary = new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height);
//            var newLevel = new SpaceLevel();
//            Levels.Push(newLevel);
//        }

//        protected GameLevel ActiveGameLevel
//        {
//            get
//            {
//                return Levels.First();
//            }
//        }

//        public void Update(GameTime gameTime)
//        {
//            var keysPressed = Keyboard.GetState().GetPressedKeys();
//            if (keysPressed.Contains(Keys.Tab) && _stopWatch.ElapsedMilliseconds > 200)
//            {
//                _stopWatch.Restart();
//                if (ActiveGameLevel.GetType() == typeof(SpaceLevel))
//                {
//                    Levels.Push(new InventoryLevel());
//                }
//                else
//                {
//                    Levels.Pop();
//                }
//            }
//            ActiveGameLevel.Update(gameTime);
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            ActiveGameLevel.Draw(spriteBatch);
//        }
//    }
//}
