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
            var textPlayerOne = new TextComponent(FontGraphics.PropertialFont_8X8, "PLAYER ONE");
            var textPlayerOneStart = new TextComponent(FontGraphics.PropertialFont_8X8, "PRESS SPACE TO START", new Vector2(0, 100));
            var counterComponent = new CounterComponent(ObjectEvent.PreviousCharacter, ObjectEvent.NextCharacter, 
                0, _playerCharacters.Count() - 1);
            var sprites = new SpriteMappingsComponent(RpgGraphics.GameboySpriteMapping, _playerCharacters, new Vector2(10, 50), counterComponent);
            var nextCharacter = new KeyboardEventComponent(Keys.Right, ObjectEvent.NextCharacter);
            var previousCharacter = new KeyboardEventComponent(Keys.Left, ObjectEvent.PreviousCharacter);

            player.AddComponent(textPlayerOne);
            player.AddComponent(textPlayerOneStart);
            player.AddComponent(counterComponent);
            player.AddComponent(sprites);
            player.AddComponent(nextCharacter);
            player.AddComponent(previousCharacter);
            DisplayLayer.AddGameObject(player);
        }

        protected override void LoadForeground()
        {

        }
    }
}
