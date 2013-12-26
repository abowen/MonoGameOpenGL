using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Components;
using MonoGame.Common.Entities;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Managers;
using MonoGame.Graphics.Common;

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
        }

        protected override void LoadForeground()
        {
            var foam = new[] {
                GeneralGraphics.TransparentCubeAsset, 
                GeneralGraphics.WhiteCubeAsset, 
                GeneralGraphics.BlueCubeAsset, 
                GeneralGraphics.LightBlueCubeAsset};
            var waveManager = new WaveManager(foam, new Texture2D[0], ForegroundLayer, 200, 100);

            DisplayLayer.Managers.Add(waveManager);
        }
    }
}
