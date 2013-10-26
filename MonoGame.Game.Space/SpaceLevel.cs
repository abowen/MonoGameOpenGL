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

            var playerStartPosition = new Vector2(xCentre, yCentre - 50);

            // TODO: Refactor this into BuilderPattern
            var newPlayerShip = new GameObject(ForegroundLayer, playerStartPosition);
            var playerSpriteComponent = new SpriteComponent(SpaceGraphics.PlayerShipAsset.First());
            var playerMovementComponent = new MovementComponent(1, FaceDirection.Up, Vector2.Zero);
            var playerInputComponent = new InputComponent(InputHelper.KeyboardMappedKey(), null, playerMovementComponent);
            var playerBulletComponent = new BulletComponent(newPlayerShip, SpaceGraphics.BulletAsset, playerMovementComponent);
            var playerHealthComponent = new HealthComponent(newPlayerShip, SpaceGraphics.HealthAsset.First(), new Vector2(10, 10), 5,
                DisplayLayer);

            newPlayerShip.Width = 10;
            newPlayerShip.Height = 10;
            newPlayerShip.HasCollision = true;

            newPlayerShip.AddGraphicsComponent(playerSpriteComponent);
            newPlayerShip.AddPhysicsComponent(playerMovementComponent);
            newPlayerShip.AddInputComponent(playerInputComponent);
            newPlayerShip.AddInputComponent(playerBulletComponent);
            newPlayerShip.AddGraphicsComponent(playerHealthComponent);
            
            ForegroundLayer.GameObjects.Add(newPlayerShip);

            ForegroundLayer.Managers.Add(asteroidManager);
            ForegroundLayer.Managers.Add(enemyManager);
        }

    }
}
