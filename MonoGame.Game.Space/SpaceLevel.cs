using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Game.Common.Components;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Helpers;
using MonoGame.Game.Common.Infrastructure;
using MonoGame.Game.Common.Managers;
using MonoGame.Graphics.Space;

namespace MonoGame.Game.Space
{
    public class SpaceLevel : GameLevel
    {
        protected override void LoadBackground()
        {
            var backgroundManager = new BackgroundManager(SpaceGraphics.PlanetAsset, SpaceGraphics.StarAsset, BackgroundLayer);
            var backgroundEnemyManager = new EnemyManager(SpaceGraphics.MiniEnemyShipAsset.First(), SpaceGraphics.MiniBulletAsset.First(), 5000, 0, BackgroundLayer, 2);
            BackgroundLayer.Managers.Add(backgroundManager);
            BackgroundLayer.Managers.Add(backgroundEnemyManager);
        }

        protected override void LoadDisplay()
        {
            
        }

        protected override void LoadForeground()
        {
            var asteroidManager = new AsteroidManager(SpaceGraphics.AsteroidAsset, SpaceGraphics.MiniAsteroidAsset, ForegroundLayer);

            var xCentre = GameConstants.ScreenBoundary.Width/2;
            var yCentre = GameConstants.ScreenBoundary.Height/2;

            var enemyManager = new EnemyManager(SpaceGraphics.EnemyShipAsset.First(), SpaceGraphics.BulletAsset.First(), 1500, 2000, ForegroundLayer, 1);

            var playerStartPosition = new Vector2(xCentre, yCentre + 50);

            // TODO: Refactor this into BuilderPattern
            var player = new GameObject(ForegroundLayer, playerStartPosition);
            var playerTexture = SpaceGraphics.PlayerShipAsset.First();
            var playerSpriteComponent = new SpriteComponent(playerTexture);
            var playerMovementComponent = new MovementComponent(2, FaceDirection.Up, Vector2.Zero);
            var playerInputComponent = new InputComponent(InputHelper.KeyboardMappedKey(), null, playerMovementComponent);
            var playerBulletComponent = new BulletComponent(player, SpaceGraphics.BulletAsset, playerMovementComponent);
            var playerHealthComponent = new HealthComponent(player, SpaceGraphics.HealthAsset.First(), new Vector2(10, 10), 5,
                DisplayLayer);
            var playerBoundaryComponent = new BoundaryComponent(player, SpaceGraphics.BoundaryAsset.First(), playerTexture.Width, playerTexture.Height);
            var playerHealthBarComponent = new SpriteRepeaterComponent(SpaceGraphics.HealthBarAsset.First(), new Vector2(0, 25), 5, false);
 
            player.AddGraphicsComponent(playerSpriteComponent);
            player.AddPhysicsComponent(playerMovementComponent);
            player.AddInputComponent(playerInputComponent);
            player.AddInputComponent(playerBulletComponent);
            player.AddGraphicsComponent(playerHealthComponent);
            player.AddPhysicsComponent(playerBoundaryComponent);
            player.AddGraphicsComponent(playerHealthBarComponent);
            
            ForegroundLayer.GameObjects.Add(player);

            ForegroundLayer.Managers.Add(asteroidManager);
            ForegroundLayer.Managers.Add(enemyManager);
        }

    }
}
