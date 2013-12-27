using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Components;
using MonoGame.Common.Entities;
using MonoGame.Common.Helpers;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Managers;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Surfing;
using MonoGame.Common.Enums;

namespace MonoGame.Game.Surfing
{
    public class SurfLevel : GameLevel
    {
        protected override void LoadBackground()
        {

        }

        protected override void LoadDisplay()
        {
            var text = new GameObject("Text", DisplayLayer, new Vector2(50, 50));
            var textComponent = new TextComponent(FontGraphics.BloxxitFont8X8, "SURFING");
            text.AddComponent(textComponent);
            DisplayLayer.GameObjects.Add(text);

            var foam = new[] {
                CommonGraphics.TransparentCubeAsset, 
                CommonGraphics.WhiteCubeAsset, 
                CommonGraphics.BlueCubeAsset, 
                CommonGraphics.LightBlueCubeAsset};
            var waveManager = new WaveManager(foam, new Texture2D[0], ForegroundLayer, 200, 100);

            DisplayLayer.Managers.Add(waveManager);
        }

        protected override void LoadForeground()
        {            
            var player = new GameObject("Player", ForegroundLayer, new Vector2(100,100));
            var playerTexture = SurfingGraphics.SurfboardAsset;
            var playerSpriteComponent = new SpriteComponent(playerTexture);
            var playerMovementComponent = new MovementComponent(2, FaceDirection.Up, Vector2.Zero);
            var playerLocalKeyboardComponent = new LocalKeyboardComponent(player);
            var playerInputComponent = new InputComponent(InputHelper.KeyboardMappedKey(), null, playerMovementComponent, playerLocalKeyboardComponent);

            player.AddComponent(playerSpriteComponent);
            player.AddComponent(playerMovementComponent);
            player.AddComponent(playerLocalKeyboardComponent);
            player.AddComponent(playerInputComponent);

            ForegroundLayer.GameObjects.Add(player);
        }
    }
}
