using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Components.Graphics;
using MonoGame.Common.Components.Input;
using MonoGame.Common.Entities;
using MonoGame.Common.Infrastructure;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Space;

namespace MonoGame.Game.Space.Levels
{
    public class StartScreen : GameLevel
    {
        public StartScreen() : base(2f)
        {            
        }

        protected override void LoadBackground()
        {

        }

        protected override void LoadDisplay()
        {
            var xCentre = GameHelper.GetRelativeScaleX(0.5f);
            var yCentre = GameHelper.GetRelativeScaleY(0.5f);
            var xOffset = GameHelper.GetRelativeScaleX(0.1f);
            var yOffset = GameHelper.GetRelativeScaleX(0.1f);

            var display = new GameObject("Title", new Vector2(xCentre, yCentre));
            var font = FontGraphics.PropertialFont_8X8;
            
            var textSimpleRpg = new TextComponent(font, "DEFENDER", new Vector2(-xOffset, -yOffset), 2);
            var textEnter = new TextComponent(font, "PRESS ENTER", new Vector2(-xOffset - 10, yOffset), 2);
            var icon1 = new SpriteComponent(SpaceGraphics.PlayerShipAsset.First(), new Vector2(-xOffset * 2, 0));
            var icon2 = new SpriteComponent(SpaceGraphics.EnemyShipAsset.First(), new Vector2(xOffset * 2, 0));            
            var action = new KeyboardActionComponent(Keys.Enter, Action);
            display.AddComponent(textSimpleRpg);
            display.AddComponent(textEnter);
            display.AddComponent(icon1);
            display.AddComponent(icon2);
            display.AddComponent(action);
            DisplayLayer.AddGameObject(display);
        }

        private void Action(GameObject gameObject)
        {
            var playerSelection = new SpaceLevel();
            NextLevel(playerSelection);            
        }

        protected override void LoadForeground()
        {

        }
    }
}
