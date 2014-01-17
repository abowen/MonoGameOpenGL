using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Components;
using MonoGame.Common.Components.Graphics;
using MonoGame.Common.Components.Input;
using MonoGame.Common.Components.Movement;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Managers;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Rpg;

namespace MonoGame.Game.Twitch.Screens
{
    public class GrassLevel : GameLevel
    {
        public GrassLevel()
            : base(4f)
        {
        }

        protected override void LoadBackground()
        {
            TileGenerator.GenerateFromCsv(@".\Maps\Grass.csv", RpgGraphics.GameboySpriteMapping, BackgroundLayer);
        }

        protected override void LoadDisplay()
        {            
            var xPosition = GameHelper.GetRelativeX(0.5f);
            var yPosition = GameHelper.GetRelativeY(0.1f);
            var gameCounter = new GameObject("GameCounter", new Vector2(xPosition - 20, yPosition));
            var text = new TextComponent(FontGraphics.DigifontFont_16X16, StringFunc);
            var playerOneOutput = new TextComponent(FontGraphics.DigifontFont_16X16, PlayerOneFunc, new Vector2(-100, 50));
            var playerTwoOutput = new TextComponent(FontGraphics.DigifontFont_16X16, PlayerTwoFunc, new Vector2(100, 50));
            gameCounter.AddComponent(text);
            gameCounter.AddComponent(playerOneOutput);
            gameCounter.AddComponent(playerTwoOutput);
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

        private string PlayerOneFunc(GameObject gameObject)
        {
            return lastActionByPlayerOne;
        }

        private string PlayerTwoFunc(GameObject gameObject)
        {
            return lastActionByPlayerTwo;
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

            playerOne = CreatePlayer("Player1", xOffsetOne, yPosition, Keys.Enter);
            playerTwo = CreatePlayer("Player2", xOffsetTwo, yPosition, Keys.Space);
        }



        private GameObject CreatePlayer(string playerCharacter, int xStartPosition, int yStartPosition, Keys key)
        {
            var player = new GameObject(playerCharacter, new Vector2(xStartPosition, yStartPosition));
            // TODO: Pass in the actual player selected character through constructor
            var sprites = new SpriteMappingComponent(RpgGraphics.GameboySpriteMapping, playerCharacter);
            var bounday = new BoundaryComponent(null, 16, 16);
            var constantMovement = new AccelerateMovementComponent(GetMovement);
            var collisionEvent = new ObjectEventComponent(ObjectEvent.CollisionEnter, CollisionEnter);
            var collisionExit = new ObjectEventComponent(ObjectEvent.CollisionExit, CollisionExit);
            var keyAction = new KeyboardActionComponent(key, PlayerAttacked);
            player.AddComponent(sprites);
            player.AddComponent(bounday);
            player.AddComponent(constantMovement);
            player.AddComponent(collisionEvent);
            player.AddComponent(collisionExit);
            player.AddComponent(keyAction);
            ForegroundLayer.AddGameObject(player);
            return player;
        }

        private Vector2 GetMovement(GameObject player)
        {
            var inReversePolarity = _isReverse ? -1 : 1;
            var playerPolarity = player == playerOne ? 1 : -1;
            return _defaultSpeed * inReversePolarity * playerPolarity;
        }

        private float _speed = 0.005f;
        private float _speedIncrease = 0.005f;

        private Vector2 _defaultSpeed
        {
            get
            {
                return new Vector2(_speed, 0);
            }
        }

        private bool _isReverse = false;

        private void PlayerAttacked(GameObject player)
        {
            //if (!hasRoundBeenWon)
            //{
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
            //}
        }

        private void CheckPlayersAttack(GameObject player)
        {
            UpdateAction(player);
            if (playersInCollision)
            {
                lastActionWonByPlayerName = player.GameType.ToUpperInvariant();
                //hasRoundBeenWon = true;
            }
        }

        private void UpdateAction(GameObject player)
        {
            // Maybe use memory references?
            if (player == playerOne)
            {
                if (playersInCollision)
                {
                    lastActionByPlayerOne = "HIT";
                }
                else
                {
                    lastActionByPlayerOne = "MISSED";
                }
            }
            if (player == playerTwo)
            {
                if (playersInCollision)
                {
                    lastActionByPlayerTwo = "HIT";
                }
                else
                {
                    lastActionByPlayerTwo = "MISSED";
                }
            }
        }

        private GameObject playerOne;
        private GameObject playerTwo;

        //private bool hasRoundBeenWon = false;
        private bool playerOneAttack = false;
        private bool playerTwoAttack = false;

        private string lastActionWonByPlayerName = string.Empty;
        private string lastActionByPlayerOne = "READY";
        private string lastActionByPlayerTwo = "READY";
        private bool playersInCollision = false;


        private void CollisionEnter(GameObject gameObject)
        {
            // Event happens twice for each player
            if (!playersInCollision)
            {
                playersInCollision = true;
            }
        }

        private void CollisionExit(GameObject gameObject)
        {
            // Event happens twice for each player
            if (playersInCollision)
            {
                playersInCollision = false;
                _isReverse = !_isReverse;
                _speed += _speedIncrease;

                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(1000);
                    playerOneAttack = false;
                    playerTwoAttack = false;
                    lastActionByPlayerOne = "READY";
                    lastActionByPlayerTwo = "READY";
                });

            }
        }
    }
}
