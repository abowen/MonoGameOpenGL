using Microsoft.Xna.Framework.Content;
using MonoGame.Common.Infrastructure;
using Microsoft.Xna.Framework;
using MonoGame.Game.Rpg.Screens;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Rpg;

namespace MonoGame.Game.Rpg
{
    public class RpgGame : SimpleGame
    {                

        public RpgGame(GameWindow window, ContentManager contentManager) : base(window, contentManager)
        {            
            FontGraphics.LoadContent(contentManager);            
            RpgGraphics.LoadContent(contentManager);
            
            var startScreen = new StartScreen();
            Levels.Push(startScreen);
        }    
    }
}
