using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Components.Graphics;
using MonoGame.Common.Components.Input;
using MonoGame.Common.Components.Movement;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Infrastructure;
using MonoGame.Graphics.Rpg;

namespace MonoGame.Game.Rpg.Screens
{
    public class FirstLevel : GameLevel
    {
        protected override void LoadBackground()
        {
            
        }        

        protected override void LoadForeground()
        {
            var player = new GameObject("PlayerOne", new Vector2(0, 0));                                    
            var sprites = new SpriteMappingComponent(RpgGraphics.GameboySpriteMapping, "Player1");
            var nextItem = new KeyboardEventComponent(Keys.X, ObjectEvent.NextItem);
            var previousItem = new KeyboardEventComponent(Keys.Z, ObjectEvent.PreviousItem);
            var useItem = new KeyboardActionComponent(Keys.Enter, UseItem);
            var useAction = new KeyboardActionComponent(Keys.Space, UseAction);
            // TODO: Could be refactored into single class.
            // Need to apply movement constraints based off grid
            var moveUp = new KeyboardEventComponent(Keys.Up, ObjectEvent.MoveUp);
            var moveDown = new KeyboardEventComponent(Keys.Down, ObjectEvent.MoveDown);
            var moveLeft = new KeyboardEventComponent(Keys.Left, ObjectEvent.MoveLeft);
            var moveRight = new KeyboardEventComponent(Keys.Right, ObjectEvent.MoveRight);
            var gridMovement = new GridMovementComponent(16);
            
            player.AddComponent(sprites);
            player.AddComponent(nextItem);
            player.AddComponent(previousItem);
            player.AddComponent(useItem);
            player.AddComponent(useAction);
            player.AddComponent(moveUp);
            player.AddComponent(moveDown);
            player.AddComponent(moveLeft);
            player.AddComponent(moveRight);
            player.AddComponent(gridMovement);
            ForegroundLayer.AddGameObject(player);
        }

        private void UseItem(GameObject gameObject)
        {
            // TODO: Consume Item
        }

        private void UseAction(GameObject gameObject)
        {
            // TODO: Consume Item
        }


        protected override void LoadDisplay()
        {
            
        }
    }
}
