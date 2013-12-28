using System;
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

            var player = new GameObject("Player", new Vector2(250, 100));
            var texture = SurfingGraphics.SurfboardAsset;
            var waveMovement = new ConstantMovementComponent(new Vector2(_waveSpeed, 0.25f));
            var outOfBoundary = new OutOfBoundsComponent(ObjectEvent.ResetEntity);
            var boundary = new BoundaryComponent(texture, texture.Width, texture.Height);            
            var angularMovement = new AngularMovementComponent(2, startRotation, Vector2.Zero, ObjectEvent.OnWave, ObjectEvent.InAir);
            var localKeyboard = new LocalKeyboardComponent();
            var input = new InputComponent(InputHelper.KeyboardMappedKey(), null, angularMovement, localKeyboard);
            var sprite = new SpriteComponent(texture, angularMovement);
            var boundaryEvent = new BoundaryEventComponent(CommonGraphics.WhiteCubeAsset, new Rectangle(0, 100, GameConstants.ScreenBoundary.Width, 200), ObjectEvent.OnWave, ObjectEvent.InAir);

            player.AddComponent(sprite);
            player.AddComponent(angularMovement);
            player.AddComponent(boundary);
            player.AddComponent(localKeyboard);
            player.AddComponent(input);
            player.AddComponent(waveMovement);
            player.AddComponent(boundaryEvent);
            player.AddComponent(outOfBoundary);

            ForegroundLayer.AddGameObject(player);
        }
    }
}
