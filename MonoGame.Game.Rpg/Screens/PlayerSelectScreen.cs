using Microsoft.Xna.Framework;
using MonoGame.Common.Components;
using MonoGame.Common.Entities;
using MonoGame.Common.Infrastructure;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Rpg;

namespace MonoGame.Game.Rpg.Screens
{
    public class PlayerSelectScreen : GameLevel
    {
        protected override void LoadBackground()
        {

        }

        protected override void LoadDisplay()
        {
            var xCentre = GameConstants.ScreenBoundary.Width / 2;
            var yCentre = GameConstants.ScreenBoundary.Height / 2;

            var display = new GameObject("Text", new Vector2(xCentre, yCentre));
            var textSimpleRpg = new TextComponent(FontGraphics.DigifontFont16X16, "SIMPLE RPG", new Vector2(-75, -25));
            var textEnter = new TextComponent(FontGraphics.DigifontFont16X16, "PRESS ENTER", new Vector2(-80, 25));
            var shield = new SpriteMappingComponent(RpgGraphics.GameboySpriteMapping, "Sword10", new Vector2(-250, 0), 3f);
            var sword = new SpriteMappingComponent(RpgGraphics.GameboySpriteMapping, "Shield10", new Vector2(200, 0), 3f);
            display.AddComponent(textSimpleRpg);
            display.AddComponent(textEnter);
            display.AddComponent(shield);
            display.AddComponent(sword);
            DisplayLayer.AddGameObject(display);
        }

        protected override void LoadForeground()
        {

        }
    }
}
