using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Common.Components;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Helpers;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Presets;
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
            var playerInputComponent = new InputNetworkComponent(KeyboardPresets.BasicReverseKeyboardMapping, InputHelper.KeyboardMappedKey(), null, playerMovementComponent);
                        
            player.AddGraphicsComponent(playerSpriteComponent);
            player.AddPhysicsComponent(playerMovementComponent);
            player.AddInputComponent(playerInputComponent);
            
            ForegroundLayer.GameObjects.Add(player);

            // TODO: Refactor this out, similar to AddGraphicsComponent
            _networkComponents.Add(playerInputComponent);
        }

        private readonly List<INetworkComponent> _networkComponents = new List<INetworkComponent>();

        public void UpdateNetwork(NetworkMessage message)
        {
            _networkComponents.ForEach(nc => nc.Update(message));
        }
    }
}
