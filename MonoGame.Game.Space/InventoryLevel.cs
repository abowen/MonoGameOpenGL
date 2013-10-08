using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Infrastructure;
using MonoGame.Game.Common.Managers;
using MonoGame.Graphics.Space;

namespace MonoGame.Game.Space
{
    public class InventoryLevel : GameLevel
    {
        protected override void LoadBackground()
        {            
        }

        protected override void LoadForeground()
        {
            
        }

        protected override void LoadDisplay()
        {
            var healthManager = new HealthManager(SpaceGraphics.AsteroidAsset.First(), new Vector2(20, 20), GameConstants.Score, ForegroundLayer);
            ForegroundLayer.Managers.Add(healthManager);
        }
    }
}
