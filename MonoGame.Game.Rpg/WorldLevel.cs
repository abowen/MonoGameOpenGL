﻿using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Common.Components;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Helpers;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Presets;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Rpg;
using MonoGame.Graphics.Space;

namespace MonoGame.Game.Rpg
{
    public class WorldLevel : GameLevel
    {
        protected override void LoadBackground()
        {
            
        }

        protected override void LoadDisplay()
        {
            var text = new GameObject("Text", new Vector2(50, 50));
            var textComponent = new TextComponent(FontGraphics.DigifontFont16X16, "TEST");
            var spriteMappingComponent = new SpriteMappingComponent(RpgGraphics.GameboySpriteMapping, "Shield3", new Vector2(0, 20));
            text.AddComponent(textComponent);
            text.AddComponent(spriteMappingComponent);
            DisplayLayer.AddGameObject(text);
        }

        protected override void LoadForeground()
        {            
            var xCentre = GameConstants.ScreenBoundary.Width / 2;
            var yCentre = GameConstants.ScreenBoundary.Height / 2;

            var playerStartPosition = new Vector2(xCentre, yCentre + 50);
            
            var player = new GameObject("Player", playerStartPosition);            
            var playerTexture = SpaceGraphics.PlayerShipAsset.First();
            var playerSpriteComponent = new SpriteComponent(playerTexture);
            var playerMovementComponent = new MovementComponent(2, FaceDirection.Up, Vector2.Zero);
            var networkKeyboardComponent = new NetworkKeyboardComponent(KeyboardPresets.BasicReverseKeyboardMapping);
            var playerInputComponent = new InputComponent(InputHelper.KeyboardMappedKey(), null, playerMovementComponent, networkKeyboardComponent, null);
                        
            player.AddComponent(playerSpriteComponent);
            player.AddComponent(playerMovementComponent);
            player.AddComponent(networkKeyboardComponent);
            player.AddComponent(playerInputComponent);
            player.AddComponent(networkKeyboardComponent);
            ForegroundLayer.AddGameObject(player);
        }
    }
}
