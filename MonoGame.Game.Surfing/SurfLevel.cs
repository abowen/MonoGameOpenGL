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
            var waveManager = new WaveManager(foam, BackgroundLayer, ForegroundLayer, 200, 100);

            DisplayLayer.Managers.Add(waveManager);
        }

        protected override void LoadForeground()
        {
            var player = new GameObject("Player", new Vector2(100, 100));
            var playerTexture = SurfingGraphics.SurfboardAsset;
            var playerGravity = new GravityComponent();
            var playerMovementComponent = new AngularMovementComponent(2, 0, 20, 180, Vector2.Zero, ObjectEvent.OnWave, ObjectEvent.InAir);
            var playerLocalKeyboardComponent = new LocalKeyboardComponent();
            var playerInputComponent = new InputComponent(InputHelper.KeyboardMappedKey(), null, playerMovementComponent, playerLocalKeyboardComponent);
            var playerSpriteComponent = new SpriteComponent(playerTexture, playerMovementComponent);
            var playerBoundaryEventComponent = new BoundaryEventComponent(CommonGraphics.WhiteCubeAsset, new Rectangle(0, 100, GameConstants.ScreenBoundary.Width, 200), ObjectEvent.OnWave, ObjectEvent.InAir);

            player.AddComponent(playerSpriteComponent);
            player.AddComponent(playerMovementComponent);
            player.AddComponent(playerLocalKeyboardComponent);
            player.AddComponent(playerInputComponent);
            player.AddComponent(playerGravity);
            player.AddComponent(playerBoundaryEventComponent);

            ForegroundLayer.AddGameObject(player);
        }
    }
}
