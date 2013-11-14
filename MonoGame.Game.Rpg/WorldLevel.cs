﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Game.Common.Components;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Events;
using MonoGame.Game.Common.Helpers;
using MonoGame.Game.Common.Infrastructure;
using MonoGame.Game.Common.Managers;
using MonoGame.Graphics.Space;

namespace MonoGame.Game.Space
{
    public class WorldLevel : GameLevel
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
            // TODO: Refactor to listen to network
            var playerInputComponent = new InputComponent(InputHelper.KeyboardMappedKey(), null, playerMovementComponent);


            player.AddGraphicsComponent(playerSpriteComponent);
            player.AddPhysicsComponent(playerMovementComponent);
            player.AddInputComponent(playerInputComponent);
            
            ForegroundLayer.GameObjects.Add(player);
        }        
    }
}
