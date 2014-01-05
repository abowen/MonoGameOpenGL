using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Common.Infrastructure;
using MonoGame.Graphics.Space;

namespace MonoGame.Game.Space
{
    public class TopDown : SimpleGame
    {                
        public TopDown(GameWindow window, ContentManager contentManager)
            : base(window, contentManager)
        {                        
            SpaceGraphics.LoadSpaceContent(contentManager);
            
            var level = new SpaceLevel();
            NextLevel(level);
        }    
    }
}
