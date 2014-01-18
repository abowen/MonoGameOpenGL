using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Common.Infrastructure;
using MonoGame.Game.Space.Levels;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Space;
using MonoGame.Sounds.Space;

namespace MonoGame.Game.Space
{
    public class TopDown : SimpleGame
    {                
        public TopDown(GameWindow window, ContentManager contentManager)
            : base(window, contentManager)
        {
            FontGraphics.LoadContent(contentManager);         
            SpaceGraphics.LoadSpaceContent(contentManager);
            SpaceSounds.LoadSpaceSounds(contentManager);
            
            var level = new StartScreen();
            NextLevel(level);
        }

        #region Game Constants

        public const string EnemyBulletName = "EnemyBullet";
        public const string EnemyName = "Enemy";
        public const string PlayerName = "Player";
        public const string PlayerBulletName = "PlayerBullet";

        #endregion
    }
}
