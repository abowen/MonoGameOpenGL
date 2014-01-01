﻿using System;
using Microsoft.Xna.Framework;
using MonoGame.Common.Components;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Helpers;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Managers;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Surfing;

namespace MonoGame.Game.Surfing
{
    public class SurfLevel : GameLevel
    {
        protected override void LoadBackground()
        {
            var backgroundManager = new BackgroundManager(new[] {SurfingGraphics.CloudMajorAsset},
                new[] {SurfingGraphics.CloudMinorAsset}, BackgroundLayer, new Vector2(-1, 0));
            backgroundManager.HorizontalBoundary(GameConstants.ScreenBoundary.Right, GameConstants.ScreenBoundary.Right);
            backgroundManager.VerticalBoundary(0, 50);
            BackgroundLayer.Managers.Add(backgroundManager);
        }

        private float _waveSpeed = -0.5f;

        protected override void LoadDisplay()
        {
            var text = new GameObject("Text", new Vector2(50, 50));
            var textComponent = new TextComponent(FontGraphics.BloxxitFont8X8, "SURFING");
            text.AddComponent(textComponent);
            DisplayLayer.AddGameObject(text);

            var foam = new[] {
                CommonGraphics.TransparentCubeAsset, 
                CommonGraphics.WhiteCubeAsset, 
                CommonGraphics.BlueCubeAsset, 
                CommonGraphics.LightBlueCubeAsset};
            var waveManager = new WaveManager(foam, BackgroundLayer, ForegroundLayer, 200, 100, _waveSpeed);

            DisplayLayer.Managers.Add(waveManager);
        }

        protected override void LoadForeground()
        {
            // MonoGame works in Radians
            // http://msdn.microsoft.com/en-us/library/system.math.sin%28v=vs.110%29.aspx
            var startRotation = (float)(90 * (Math.PI / 180));
            var texture = SurfingGraphics.Surfboard_White_Asset;
            var playerOne = new GameObject("PlayerOne", new Vector2(250, 100), startRotation);
         //   var waveMovement = new ConstantMovementComponent(new Vector2(_waveSpeed, 0.25f));
            var airGravityMovement = new ConstantMovementComponent(new Vector2(0, 0.05f));
            var outOfBoundary = new OutOfBoundsComponent(ObjectEvent.ResetEntity);
            var boundary = new BoundaryComponent(texture, texture.Width, texture.Height);           
            var rotationMovement = new RotationMovementComponent(2, Vector2.Zero);
            var rotation = new RotationComponent(Vector2.Zero);
            var localKeyboard = new LocalKeyboardComponent();
            var input = new InputComponent(InputHelper.KeyboardMappedKey(), localKeyboard, rotationMovement, rotation);
            var sprite = new SpriteComponent(texture, Color.Red);

            playerOne.AddComponent(airGravityMovement);
            playerOne.AddComponent(sprite);
            playerOne.AddComponent(rotationMovement);
            playerOne.AddComponent(boundary);
            playerOne.AddComponent(localKeyboard);
            playerOne.AddComponent(input);
          //  playerOne.AddComponent(waveMovement);          
            playerOne.AddComponent(outOfBoundary);
            playerOne.AddComponent(rotation);

            var onWave = "OnWave";
            var inAir = "InAir";
         

            var state = new StateComponent();
            state.AddComponentState(rotationMovement, onWave, inAir);
            state.AddComponentState(rotation, inAir, onWave);
            state.AddComponentState(airGravityMovement, inAir, onWave);

            playerOne.AddComponent(state);

            var boundaryState = new BoundaryStateComponent(new Rectangle(0, 100, GameConstants.ScreenBoundary.Width, 200), onWave, inAir);
            playerOne.AddComponent(boundaryState);

            ForegroundLayer.AddGameObject(playerOne);

            //var playerTwo = new GameObject("PlayerTwo", new Vector2(300, 100), startRotationTwo);            
            //var waveMovementTwo = new ConstantMovementComponent(new Vector2(_waveSpeed, 0.25f));
            //var outOfBoundaryTwo = new OutOfBoundsComponent(ObjectEvent.ResetEntity);
            //var boundaryTwo = new BoundaryComponent(texture, texture.Width, texture.Height);
            //var angularMovementTwo = new AngularMovementComponent(2, Vector2.Zero, ObjectEvent.OnWave, ObjectEvent.InAir);
            //var localButtonTwo = new LocalButtonComponent();
            //var inputTwo = new InputComponent(InputHelper.GamepadMappedKey(), localButtonTwo, angularMovementTwo);
            //var spriteTwo = new SpriteComponent(texture, Color.LightGreen);
            //var boundaryEventTwo = new BoundaryEventComponent(CommonGraphics.WhiteCubeAsset, new Rectangle(0, 100, GameConstants.ScreenBoundary.Width, 200), ObjectEvent.OnWave, ObjectEvent.InAir);

            //playerTwo.AddComponent(spriteTwo);
            //playerTwo.AddComponent(angularMovementTwo);
            //playerTwo.AddComponent(boundaryTwo);
            //playerTwo.AddComponent(localButtonTwo);
            //playerTwo.AddComponent(inputTwo);
            //playerTwo.AddComponent(waveMovementTwo);
            //playerTwo.AddComponent(boundaryEventTwo);
            //playerTwo.AddComponent(outOfBoundaryTwo);

            //ForegroundLayer.AddGameObject(playerTwo);
        }
    }
}
