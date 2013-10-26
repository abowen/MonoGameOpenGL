﻿using System.Linq;
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
            // TODO: Refactor into generic collision manager into more event driven / composition manner
            var collisionManager = new CollisionManager(ForegroundLayer);

            var bulletAsteroidCollision = new CollisionType
            {
                TypeA = typeof(Bullet),
                TypeB = typeof(Asteroid),
                Action = (bullet, asteroid) =>
                {
                    bullet.RemoveEntity();
                    asteroid.RemoveEntity();
                }
            };

            var playerAsteroidCollision = new CollisionType
            {
                TypeA = typeof(PlayerShip),
                TypeB = typeof(Asteroid),
                Action = (ship, asteroid) =>
                {
                    asteroid.RemoveEntity();
                    (ship as PlayerShip).HealthManager.RemoveLife(ship);
                }
            };

            var playerBulletCollision = new CollisionType
            {
                TypeA = typeof(PlayerShip),
                TypeB = typeof(Bullet),
                Action = (ship, bullet) =>
                {
                    bullet.RemoveEntity();
                    (ship as PlayerShip).HealthManager.RemoveLife(ship);
                }
            };

            var enemyBulletCollision = new CollisionType
            {
                TypeA = typeof(EnemyShip),
                TypeB = typeof(Bullet),
                Action = (enemy, bullet) =>
                {
                    bullet.RemoveEntity();
                    enemy.RemoveEntity();
                }
            };
            

            collisionManager.CollisionTypes.Add(bulletAsteroidCollision);
            collisionManager.CollisionTypes.Add(playerAsteroidCollision);
            collisionManager.CollisionTypes.Add(enemyBulletCollision);
            collisionManager.CollisionTypes.Add(playerBulletCollision);

            var asteroidManager = new AsteroidManager(SpaceGraphics.AsteroidAsset, SpaceGraphics.MiniAsteroidAsset, ForegroundLayer);

            var xCentre = GameConstants.ScreenBoundary.Width/2;
            var yCentre = GameConstants.ScreenBoundary.Height/2;

            var asteroidGameObject = new GameObject(ForegroundLayer, new Vector2(xCentre, yCentre));

            var enemyManager = new EnemyManager(SpaceGraphics.EnemyShipAsset.First(), SpaceGraphics.BulletAsset.First(), 1500, 2000, ForegroundLayer, 1);

            var playerStartPosition = new Vector2(xCentre, yCentre - 50);
            //var playerShip = new PlayerShip(SpaceGraphics.PlayerShipAsset.First(), playerStartPosition, SpaceGraphics.BulletAsset.First(), SpaceGraphics.HealthAsset.First(), 5, FaceDirection.Up, ForegroundLayer, InputHelper.KeyboardMappedKey());

            // TODO: Refactor this
            var newPlayerShip = new GameObject(ForegroundLayer, playerStartPosition);
            var playerSpriteComponent = new SpriteComponent(SpaceGraphics.PlayerShipAsset.First());
            var playerMovementComponent = new MovementComponent(1, FaceDirection.Up, Vector2.Zero);
            var playerInputComponent = new InputComponent(InputHelper.KeyboardMappedKey(), null, playerMovementComponent);
            var playerBulletComponent = new BulletComponent(newPlayerShip, SpaceGraphics.BulletAsset, playerMovementComponent);
            var playerHealthComponent = new HealthComponent(newPlayerShip, SpaceGraphics.HealthAsset.First(), new Vector2(10, 10), 5,
                DisplayLayer);
//            var playerCollisionComponent = new CollisionComponent();

            newPlayerShip.Width = 10;
            newPlayerShip.Height = 10;
            newPlayerShip.HasCollision = true;

            newPlayerShip.AddGraphicsComponent(playerSpriteComponent);
            newPlayerShip.AddPhysicsComponent(playerMovementComponent);
            newPlayerShip.AddInputComponent(playerInputComponent);
            newPlayerShip.AddInputComponent(playerBulletComponent);
            newPlayerShip.AddGraphicsComponent(playerHealthComponent);
       //     newPlayerShip.AddPhysicsComponent(playerCollisionComponent);
            
            ForegroundLayer.GameObjects.Add(newPlayerShip);
            //ForegroundLayer.GameEntities.Add(playerShip);

            ForegroundLayer.Managers.Add(collisionManager);
            ForegroundLayer.Managers.Add(asteroidManager);
            ForegroundLayer.Managers.Add(enemyManager);
        }

    }
}
