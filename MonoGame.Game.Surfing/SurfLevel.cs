﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Components.Boundary;
using MonoGame.Common.Components.Graphics;
using MonoGame.Common.Components.Input;
using MonoGame.Common.Components.Movement;
using MonoGame.Common.Components.States;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Extensions;
using MonoGame.Common.Helpers;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Managers;
using MonoGame.Common.Presets;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Surfing;

namespace MonoGame.Game.Surfing
{
    public class SurfLevel : GameLevel
    {
        public SurfLevel() : base(2f) { }

        protected override void LoadBackground()
        {
            var backgroundManager = new BackgroundManager(new[] { SurfingGraphics.CloudMajorAsset },
                new[] { SurfingGraphics.CloudMinorAsset }, BackgroundLayer, new Vector2(-1, 0), new Vector2(-2, 0), 3000);
            backgroundManager.HorizontalBoundary(GameConstants.ScreenBoundary.Right, GameConstants.ScreenBoundary.Right);

            backgroundManager.VerticalBoundary(0, 50);
            BackgroundLayer.Managers.Add(backgroundManager);
        }

        private float _waveSpeed = -0.5f;

        protected override void LoadDisplay()
        {
            var textX = GameHelper.GetRelativeScaleX(0.2f);
            var textY = GameHelper.GetRelativeScaleY(0.9f);

            var playerOne = new GameObject("Text", new Vector2(textX, textY));
            var textOne = new TextComponent(FontGraphics.BloxxitFont_8X8, "PRESS SPACE");
            var actionOne = new KeyboardActionComponent(Keys.Space, CreatePlayerOne);
            playerOne.AddComponent(textOne);
            playerOne.AddComponent(actionOne);
            DisplayLayer.AddGameObject(playerOne);

            var playerTwo = new GameObject("Text", new Vector2(textX * 2, textY));
            var textTwo = new TextComponent(FontGraphics.BloxxitFont_8X8, "PRESS A GAMEPAD");
            var actionTwo = new ButtonActionComponent(Buttons.A, CreatePlayerTwo);
            playerTwo.AddComponent(textTwo);
            playerTwo.AddComponent(actionTwo);
            DisplayLayer.AddGameObject(playerTwo);


            var playerThree = new GameObject("Text", new Vector2(textX * 3, textY));
            var textThree = new TextComponent(FontGraphics.BloxxitFont_8X8, "PRESS ENTER");
            var actionThree = new KeyboardActionComponent(Keys.Enter, CreatePlayerThree);
            playerThree.AddComponent(textThree);
            playerThree.AddComponent(actionThree);
            DisplayLayer.AddGameObject(playerThree);

            var foam = new[] {
                CommonGraphics.TransparentCubeAsset, 
                CommonGraphics.WhiteCubeAsset, 
                CommonGraphics.BlueCubeAsset, 
                CommonGraphics.LightBlueCubeAsset};
            var waveManager = new WaveManager(foam, BackgroundLayer, ForegroundLayer, _waveHeight, _waveTopY, _waveSpeed);

            DisplayLayer.Managers.Add(waveManager);
        }

        protected override void LoadForeground()
        {
        }

        private void CreatePlayerOne(GameObject gameObject)
        {
            gameObject.RemoveGameObject();

            var player = CreatePlayer(1, Color.Red);
            var localKeyboard = new LocalKeyboardComponent();
            var input = new InputComponent(InputHelper.KeyboardMappedKey(), localKeyboard);

            player.AddComponent(localKeyboard);
            player.AddComponent(input);
        }

        private void CreatePlayerTwo(GameObject gameObject)
        {
            gameObject.RemoveGameObject();

            var player = CreatePlayer(2, Color.LightGreen);
            var listenerComponent = new LocalButtonComponent();
            var input = new InputComponent(InputHelper.GamepadMappedKey(), listenerComponent);

            player.AddComponent(listenerComponent);
            player.AddComponent(input);
        }

        private void CreatePlayerThree(GameObject gameObject)
        {
            gameObject.RemoveGameObject();

            var player = CreatePlayer(3, Color.Orange);
            var listenerComponent = new NetworkKeyboardComponent(KeyboardPresets.BasicReverseKeyboardMapping);
            var input = new InputComponent(InputHelper.KeyboardMappedKey(), listenerComponent);

            player.AddComponent(listenerComponent);
            player.AddComponent(input);


        }

        private int _waveTopY
        {
            get { return GameHelper.GetRelativeScaleY(0.2f); }
        }

        private int _waveBottomY
        {
            get { return GameHelper.GetRelativeScaleY(0.5f); }
        }

        private int _waveHeight
        {
            get { return _waveBottomY - _waveTopY; }
        }


        private GameObject CreatePlayer(int playerNumber, Color color)
        {
            // MonoGame works in Radians
            // http://msdn.microsoft.com/en-us/library/system.math.sin%28v=vs.110%29.aspx
            var startX = GameHelper.GetRelativeScaleX(0.1f);
            var startLocation = new Vector2(startX, _waveTopY) + new Vector2(playerNumber * startX, 0);
            var startRotation = (float)(90 * (Math.PI / 180));

            var texture = SurfingGraphics.Surfboard_Large_White_Asset;
            var player = new GameObject("Player", startLocation, startRotation);
            var airGravityMovement = new AccelerateMovementComponent(new Vector2(0, 0.05f));
            var outOfBoundary = new OutOfBoundsComponent(ObjectEvent.ResetEntity);
            var boundary = new BoundaryComponent(texture, texture.Width, texture.Height);
            var rotationMovement = new RotationMovementComponent(2, Vector2.Zero);
            var rotation = new RotationComponent(Vector2.Zero);
            var sprite = new SpriteComponent(texture, rotationOrigin: texture.Centre(), color: color);

            player.AddComponent(airGravityMovement);
            player.AddComponent(sprite);
            player.AddComponent(rotationMovement);
            player.AddComponent(boundary);
            player.AddComponent(outOfBoundary);
            player.AddComponent(rotation);

            var onWave = "OnWave";
            var inAir = "InAir";

            var state = new StateStringComponent();
            state.AddComponentState(rotationMovement, onWave, inAir);
            state.AddComponentState(rotation, inAir, onWave);
            state.AddComponentState(airGravityMovement, inAir, onWave);

            player.AddComponent(state);

            var boundaryState = new BoundaryStateComponent(new Rectangle(0, _waveTopY, GameHelper.GetRelativeScaleX(1f), (int) (_waveHeight * 1.3f)), onWave, inAir);
            player.AddComponent(boundaryState);

            ForegroundLayer.AddGameObject(player);

            return player;
        }
    }
}
