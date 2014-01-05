using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Components.Graphics;
using MonoGame.Common.Components.Input;
using MonoGame.Common.Components.Logic;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
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

        private readonly string[] _playerCharacters =
        {
            "Player1",
            "Player2",
            "Player3",
            "Player4",
            "Player5",
            "Player6",
            "Player7",
            "Player8",
            "Player9",
            "Player10"
        };

        protected override void LoadDisplay()
        {                        
            var player = new GameObject("PlayerOneSelection", new Vector2(10, 10));
            var playerText = new TextComponent(FontGraphics.PropertialFont_8X8, "PLAYER ONE");
            var startText = new TextComponent(FontGraphics.PropertialFont_8X8, "PRESS SPACE TO START", new Vector2(0, 100));
            var counter = new CounterComponent(ObjectEvent.PreviousCharacter, ObjectEvent.NextCharacter, 0, _playerCharacters.Count() - 1);
            var sprites = new SpriteMappingsComponent(RpgGraphics.GameboySpriteMapping, _playerCharacters, new Vector2(10, 50), counter);
            var nextCharacter = new KeyboardEventComponent(Keys.Right, ObjectEvent.NextCharacter);
            var previousCharacter = new KeyboardEventComponent(Keys.Left, ObjectEvent.PreviousCharacter);
            var action = new KeyboardActionComponent(Keys.Space, Action);

            player.AddComponent(playerText);
            player.AddComponent(startText);
            player.AddComponent(counter);
            player.AddComponent(sprites);
            player.AddComponent(nextCharacter);
            player.AddComponent(previousCharacter);
            player.AddComponent(action);
            DisplayLayer.AddGameObject(player);
        }

        private void Action(GameObject gameObject)
        {
            NextLevel(new FirstLevel());
        }

        protected override void LoadForeground()
        {

        }
    }
}
