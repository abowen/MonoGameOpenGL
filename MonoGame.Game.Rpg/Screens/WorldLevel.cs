using Microsoft.Xna.Framework;
using MonoGame.Common.Components.Graphics;
using MonoGame.Common.Entities;
using MonoGame.Common.Infrastructure;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Rpg;

namespace MonoGame.Game.Rpg.Screens
{
    public class WorldLevel : GameLevel
    {
        protected override void LoadBackground()
        {
            
        }

        protected override void LoadDisplay()
        {
            var xCentre = GameConstants.ScreenBoundary.Width / 2;
            var yCentre = GameConstants.ScreenBoundary.Height / 2;

            var display = new GameObject("Text", new Vector2(xCentre, yCentre));
            var textComponent = new TextComponent(FontGraphics.DigifontFont_16X16, "Simple RPG");
            var shield = new SpriteMappingComponent(RpgGraphics.GameboySpriteMapping, "Sword10", new Vector2(-100, 0), 2f);
            var sword = new SpriteMappingComponent(RpgGraphics.GameboySpriteMapping, "Shield10", new Vector2(100, 0), 2f);
            display.AddComponent(textComponent);
            display.AddComponent(shield);
            display.AddComponent(sword);
            DisplayLayer.AddGameObject(display);
        }

        protected override void LoadForeground()
        {            
            
        }
    }
}
