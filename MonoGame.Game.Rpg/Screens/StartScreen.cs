using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Components.Graphics;
using MonoGame.Common.Components.Input;
using MonoGame.Common.Entities;
using MonoGame.Common.Infrastructure;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Rpg;

namespace MonoGame.Game.Rpg.Screens
{
    public class StartScreen : GameLevel
    {
        protected override void LoadBackground()
        {

        }

        protected override void LoadDisplay()
        {
            var xCentre = GameConstants.ScreenBoundary.Width / 2;
            var yCentre = GameConstants.ScreenBoundary.Height / 2;

            var display = new GameObject("Text", new Vector2(xCentre, yCentre));
            var font = FontGraphics.PropertialFont_8X8;
            var textSimpleRpg = new TextComponent(font, "SIMPLE RPG", new Vector2(-75, -25), 2);
            var textEnter = new TextComponent(font, "PRESS ENTER", new Vector2(-80, 25), 2);
            var shield = new SpriteMappingComponent(RpgGraphics.GameboySpriteMapping, "Sword10", new Vector2(-250, 0), 3f);
            var sword = new SpriteMappingComponent(RpgGraphics.GameboySpriteMapping, "Shield10", new Vector2(200, 0), 3f);
            var action = new KeyboardActionComponent(Keys.Enter, Action);
            display.AddComponent(textSimpleRpg);
            display.AddComponent(textEnter);
            display.AddComponent(shield);
            display.AddComponent(sword);
            display.AddComponent(action);
            DisplayLayer.AddGameObject(display);
        }

        private void Action(GameObject gameObject)
        {
            var playerSelection = new PlayerSelectScreen();
            NextLevel(playerSelection);            
        }

        protected override void LoadForeground()
        {

        }
    }
}
