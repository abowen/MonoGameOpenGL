using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Surfing;

namespace MonoGame.Game.Surfing
{
    public class SurfingGame : SimpleGame, ISimpleNetworking
    {
        public SurfingGame(GameWindow window, ContentManager contentManager)
            : base(window, contentManager)
        {
            FontGraphics.LoadContent(contentManager);
            CommonGraphics.LoadContent(contentManager);
            SurfingGraphics.LoadContent(contentManager);

            var level = new SurfLevel();
            NextLevel(level);
        }        
    }
}
