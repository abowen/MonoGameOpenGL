using Microsoft.Xna.Framework;
using MonoGame.Common.Components.Graphics;
using MonoGame.Common.Entities;
using MonoGame.Common.Infrastructure;
using MonoGame.Graphics.Rpg;

namespace MonoGame.Game.Twitch.Screens
{
    public class GrassLevel : GameLevel
    {
        protected override void LoadBackground()
        {
            
        }      

        protected override void LoadDisplay()
        {
            
        }
        

        protected override void LoadForeground()
        {
            var yStartPosition = 400;
            var xStartOffset = 50;
            CreatePlayer("Player1", xStartOffset, yStartPosition);
            CreatePlayer("Player2", GameConstants.ScreenBoundary.Width - xStartOffset, yStartPosition);
        }

        private void CreatePlayer(string playerCharacter, int xStartPosition, int yStartPosition)
        {
            var player = new GameObject(playerCharacter, new Vector2(xStartPosition, yStartPosition));
            // TODO: Pass in the actual player selected character
            var sprites = new SpriteMappingComponent(RpgGraphics.GameboySpriteMapping, playerCharacter);
            player.AddComponent(sprites);
            ForegroundLayer.AddGameObject(player);            
        }          
    }
}
