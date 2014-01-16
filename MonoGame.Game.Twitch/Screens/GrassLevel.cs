using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Components;
using MonoGame.Common.Components.Graphics;
using MonoGame.Common.Components.Input;
using MonoGame.Common.Components.Movement;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Infrastructure;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Rpg;

namespace MonoGame.Game.Twitch.Screens
{
    public class GrassLevel : GameLevel
    {
        public GrassLevel() : base(2f)
        {            
        }

        protected override void LoadBackground()
        {

        }

        protected override void LoadDisplay()
        {
            //var xPosition = GameConstants.ScreenBoundaryDividedByScale.Width / 2;            
            var xPosition = GameHelper.GetRelativeX(0.5f);
            var yPosition = GameHelper.GetRelativeY(0.1f);
            var gameCounter = new GameObject("GameCounter", new Vector2(xPosition - 20, yPosition));
            var text = new TextComponent(FontGraphics.DigifontFont_16X16, StringFunc);
            gameCounter.AddComponent(text);
            DisplayLayer.AddGameObject(gameCounter);

            var debugHelper = new GameObject("DebugHelper", new Vector2(xPosition - 100, yPosition * 2));
            var collisionDetection = new TextComponent(FontGraphics.DigifontFont_16X16, DebugFunc);
            debugHelper.AddComponent(collisionDetection);
            DisplayLayer.AddGameObject(debugHelper);
        }

        private string StringFunc(GameObject gameObject)
        {
            return lastActionWonByPlayerName;
        }

        private string DebugFunc(GameObject gameObject)
        {
            return playersInCollision ? "COLLISION: TRUE" : "COLLISION: FALSE";
        }


        protected override void LoadForeground()
        {
            var yPosition = GameHelper.GetRelativeY(0.5f);
            var xOffsetOne = GameHelper.GetRelativeX(0.1f);
            var xOffsetTwo = GameHelper.GetRelativeX(0.9f);
            var speed = 0.02f;
            playerOne = CreatePlayer("Player1", xOffsetOne, yPosition, speed, Keys.Enter);
            playerTwo = CreatePlayer("Player2", xOffsetTwo, yPosition, -speed, Keys.Space);
        }

        private GameObject CreatePlayer(string playerCharacter, int xStartPosition, int yStartPosition, float xDirection, Keys key)
        {
            var player = new GameObject(playerCharacter, new Vector2(xStartPosition, yStartPosition));
            // TODO: Pass in the actual player selected character through constructor
            var sprites = new SpriteMappingComponent(RpgGraphics.GameboySpriteMapping, playerCharacter);
            var bounday = new BoundaryComponent(null, 16, 16);
            var constantMovement = new AccelerateMovementComponent(new Vector2(xDirection, 0));
            //var boundaryEvent = new BoundaryEventComponent()
            var collisionEvent = new ObjectEventComponent(ObjectEvent.Collision, Collision);
            var keyAction = new KeyboardActionComponent(key, PlayerAttacked);
            player.AddComponent(sprites);
            player.AddComponent(bounday);
            player.AddComponent(constantMovement);
            player.AddComponent(collisionEvent);
            player.AddComponent(keyAction);
            ForegroundLayer.AddGameObject(player);
            return player;
        }

        private void PlayerAttacked(GameObject player)
        {
            if (!hasRoundBeenWon)
            {
                if (player == playerOne)
                {
                    if (!playerOneAttack)
                    {
                        playerOneAttack = true;
                        CheckPlayersAttack(player);
                    }
                }
                else if (player == playerTwo)
                {
                    if (!playerTwoAttack)
                    {
                        playerTwoAttack = true;
                        CheckPlayersAttack(player);
                    }
                }
                else
                {
                    throw new Exception("Whoops");
                }
            }
        }

        private void CheckPlayersAttack(GameObject gameObject)
        {
            if (playersInCollision)
            {
                lastActionWonByPlayerName = gameObject.GameType.ToUpperInvariant();
                hasRoundBeenWon = true;
            }
        }

        private GameObject playerOne;
        private GameObject playerTwo;

        private bool hasRoundBeenWon = false;
        private bool playerOneAttack = false;
        private bool playerTwoAttack = false;

        private string lastActionWonByPlayerName = string.Empty;
        private bool playersInCollision = false;

        private void Collision(GameObject gameObject)
        {
            playersInCollision = true;
        }
    }
}
