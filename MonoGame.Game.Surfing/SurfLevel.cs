using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
            var backgroundManager = new BackgroundManager(new[] { SurfingGraphics.CloudMajorAsset },
                new[] { SurfingGraphics.CloudMinorAsset }, BackgroundLayer, new Vector2(-1, 0));
            backgroundManager.HorizontalBoundary(GameConstants.ScreenBoundary.Right, GameConstants.ScreenBoundary.Right);
            backgroundManager.VerticalBoundary(0, 50);
            BackgroundLayer.Managers.Add(backgroundManager);
        }

        private float _waveSpeed = -0.5f;

        private GameObject _playerOneStartText;
        private GameObject _playerTwoStartText;

        protected override void LoadDisplay()
        {
            _playerOneStartText = new GameObject("Text", new Vector2(50, 450));
            var textComponent = new TextComponent(FontGraphics.BloxxitFont8X8, "PRESS SPACE");
            _playerOneStartText.AddComponent(textComponent);
            DisplayLayer.AddGameObject(_playerOneStartText);

            _playerTwoStartText = new GameObject("Text", new Vector2(250, 450));
            var playerTwoTextComponent = new TextComponent(FontGraphics.BloxxitFont8X8, "PRESS A GAMEPAD");
            _playerTwoStartText.AddComponent(playerTwoTextComponent);
            DisplayLayer.AddGameObject(_playerTwoStartText);

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
            var playerManager = new PlayerManager();
            playerManager.AddPlayerListener(Keys.Space, CreatePlayerOne);
            playerManager.AddPlayerListener(Buttons.A, CreatePlayerTwo);
            ForegroundLayer.Managers.Add(playerManager);
        }

        private void CreatePlayerOne()
        {
            _playerOneStartText.RemoveGameObject();
            _playerOneStartText = null;

            var player = CreatePlayer(Vector2.Zero, Color.Red);
            var localKeyboard = new LocalKeyboardComponent();
            var input = new InputComponent(InputHelper.KeyboardMappedKey(), localKeyboard);

            player.AddComponent(localKeyboard);
            player.AddComponent(input);
        }

        private void CreatePlayerTwo()
        {
            _playerTwoStartText.RemoveGameObject();
            _playerTwoStartText = null;

            var player = CreatePlayer(new Vector2(50,0), Color.LightGreen);
            var listenerComponent = new LocalButtonComponent();
            var input = new InputComponent(InputHelper.GamepadMappedKey(), listenerComponent);

            player.AddComponent(listenerComponent);
            player.AddComponent(input);
        }

        private GameObject CreatePlayer(Vector2 startOffset, Color color)
        {
            // MonoGame works in Radians
            // http://msdn.microsoft.com/en-us/library/system.math.sin%28v=vs.110%29.aspx
            var startLocation = new Vector2(250, 100) + startOffset;
            var startRotation = (float)(90 * (Math.PI / 180));

            var texture = SurfingGraphics.Surfboard_White_Asset;
            var player = new GameObject("Player", startLocation, startRotation);
            var airGravityMovement = new ConstantMovementComponent(new Vector2(0, 0.05f));
            var outOfBoundary = new OutOfBoundsComponent(ObjectEvent.ResetEntity);
            var boundary = new BoundaryComponent(texture, texture.Width, texture.Height);
            var rotationMovement = new RotationMovementComponent(2, Vector2.Zero);
            var rotation = new RotationComponent(Vector2.Zero);
            var sprite = new SpriteComponent(texture, color);

            player.AddComponent(airGravityMovement);
            player.AddComponent(sprite);
            player.AddComponent(rotationMovement);
            player.AddComponent(boundary);            
            player.AddComponent(outOfBoundary);
            player.AddComponent(rotation);

            var onWave = "OnWave";
            var inAir = "InAir";

            var state = new StateComponent();
            state.AddComponentState(rotationMovement, onWave, inAir);
            state.AddComponentState(rotation, inAir, onWave);
            state.AddComponentState(airGravityMovement, inAir, onWave);

            player.AddComponent(state);

            var boundaryState = new BoundaryStateComponent(new Rectangle(0, 100, GameConstants.ScreenBoundary.Width, 200),
                onWave, inAir);
            player.AddComponent(boundaryState);

            ForegroundLayer.AddGameObject(player);

            return player;
        }
    }
}
