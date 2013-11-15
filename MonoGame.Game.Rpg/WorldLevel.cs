using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Common.Components;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Helpers;
using MonoGame.Common.Infrastructure;
using MonoGame.Graphics.Space;
using MonoGame.Server;

namespace MonoGame.Game.Rpg
{
    public class WorldLevel : GameLevel,  INetworkGame
    {
        protected override void LoadBackground()
        {
            
        }

        protected override void LoadDisplay()
        {

        }

        protected override void LoadForeground()
        {            
            var xCentre = GameConstants.ScreenBoundary.Width / 2;
            var yCentre = GameConstants.ScreenBoundary.Height / 2;

            var playerStartPosition = new Vector2(xCentre, yCentre + 50);
            
            var player = new GameObject(ForegroundLayer, playerStartPosition);
            player.GameType = "Player";
            var playerTexture = SpaceGraphics.PlayerShipAsset.First();
            var playerSpriteComponent = new SpriteComponent(playerTexture);
            var playerMovementComponent = new MovementComponent(2, FaceDirection.Up, Vector2.Zero);
            // Refactor to listen to network events
            //var playerInputComponent = new InputNetworkComponent(15234, , , null, playerMovementComponent);
                        
            player.AddGraphicsComponent(playerSpriteComponent);
            player.AddPhysicsComponent(playerMovementComponent);
            //player.AddInputComponent(playerInputComponent);
            
            ForegroundLayer.GameObjects.Add(player);
        }

        public void UpdateNetwork(NetworkMessage message)
        {
            // Send message to any networked components
        }
    }
}
