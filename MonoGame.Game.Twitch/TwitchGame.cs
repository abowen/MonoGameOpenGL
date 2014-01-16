using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Common.Infrastructure;
using MonoGame.Game.Twitch.Screens;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Rpg;


namespace MonoGame.Game.Twitch
{
    public class TwitchGame : SimpleGame
    {

        public TwitchGame(GameWindow window, ContentManager contentManager)
            : base(window, contentManager)
        {
            FontGraphics.LoadContent(contentManager);
            RpgGraphics.LoadContent(contentManager);

            var startScreen = new StartScreen();
            NextLevel(startScreen);
        }
    }
}
