using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Game.Common.Components;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Events;
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
            var backgroundEnemyManager = new EnemyManager(SpaceGraphics.MiniEnemyShipAsset.First(), SpaceGraphics.MiniBulletAsset.First(), 5000, 10000, BackgroundLayer, 2);
            BackgroundLayer.Managers.Add(backgroundManager);
            BackgroundLayer.Managers.Add(backgroundEnemyManager);
        }

        protected override void LoadDisplay()
        {

        }

        protected override void LoadForeground()
        {
            var asteroidManager = new AsteroidManager(SpaceGraphics.AsteroidAsset, SpaceGraphics.MiniAsteroidAsset, ForegroundLayer);

            var xCentre = GameConstants.ScreenBoundary.Width / 2;
            var yCentre = GameConstants.ScreenBoundary.Height / 2;

            var enemyManager = new EnemyManager(SpaceGraphics.EnemyShipAsset.First(), SpaceGraphics.BulletAsset.First(), 1500, 2000, ForegroundLayer, 1);

            var playerStartPosition = new Vector2(xCentre, yCentre + 50);

            // TODO: Refactor this into BuilderPattern
            var player = new GameObject(ForegroundLayer, playerStartPosition);
            player.GameType = "Player";
            var playerTexture = SpaceGraphics.PlayerShipAsset.First();
            var playerSpriteComponent = new SpriteComponent(playerTexture);
            var playerMovementComponent = new MovementComponent(2, FaceDirection.Up, Vector2.Zero);
            var playerInputComponent = new InputComponent(InputHelper.KeyboardMappedKey(), null, playerMovementComponent);

            //var playerHealthComponent = new HealthComponent(player, SpaceGraphics.HealthAsset.First(), new Vector2(10, 10), 5,
            //    DisplayLayer);
            var playerBoundaryComponent = new BoundaryComponent(player, SpaceGraphics.BoundaryAsset.First(), playerTexture.Width, playerTexture.Height);
            player.AddPhysicsComponent(playerBoundaryComponent);

            var playerHealthCounterComponent = new CounterComponent(player, ObjectEvent.Collision, ObjectEvent.HealthRemoved, ObjectEvent.HealthEmpty, ObjectEvent.HealthReset, 5, 0);
            var playerHealthBarComponent = new SpriteRepeaterComponent(SpaceGraphics.HealthBarAsset.First(), new Vector2(0, 25), false, player, ObjectEvent.HealthRemoved, playerHealthCounterComponent);

            var playerBulletComponent = new BulletComponent(player, SpaceGraphics.BulletAsset, playerMovementComponent, ObjectEvent.AmmoRemoved, ObjectEvent.AmmoEmpty, ObjectEvent.AmmoReset);
            var playerAmmoCounterComponent = new CounterComponent(player, ObjectEvent.Fire, ObjectEvent.AmmoRemoved, ObjectEvent.AmmoEmpty, ObjectEvent.AmmoReset, 10, 0);
            var playerAmmoBarComponent = new SpriteRepeaterComponent(SpaceGraphics.AmmoBarAsset.First(), new Vector2(-25, 25), true, player, ObjectEvent.AmmoRemoved, playerAmmoCounterComponent, true);

            var playerFireCounterComponent = new CounterComponent(player, ObjectEvent.Collision, ObjectEvent.WoodFire, ObjectEvent.Ignore, ObjectEvent.Ignore, 0, 5, false);
            var playerWoodFireComponent = new SpriteGenericComponent(SpaceGraphics.FireAsset, player.CentreLocal, player, ObjectEvent.WoodFire, playerFireCounterComponent, RandomDrawMethod);

            var playerEventComponent = new ObjectEventComponent(player, ObjectEvent.HealthEmpty, PlayerDeath);
            // player.ObjectEvent += PlayerOnObjectEvent;

            player.AddGraphicsComponent(playerSpriteComponent);
            player.AddPhysicsComponent(playerMovementComponent);
            player.AddInputComponent(playerInputComponent);
            player.AddInputComponent(playerBulletComponent);
            player.AddPhysicsComponent(playerHealthCounterComponent);
            player.AddGraphicsComponent(playerHealthBarComponent);
            player.AddPhysicsComponent(playerAmmoCounterComponent);
            player.AddGraphicsComponent(playerAmmoBarComponent);
            player.AddPhysicsComponent(playerFireCounterComponent);
            player.AddGraphicsComponent(playerWoodFireComponent);
            player.AddPhysicsComponent(playerEventComponent);

            ForegroundLayer.GameObjects.Add(player);

            // TODO: Move scoring to use global space and per player
            var score = new GameObject(DisplayLayer, new Vector2(10, 10));
            score.GameType = "Score";
            var scoreCounterComponent = new CounterComponent(score, ObjectEvent.ScoreIncrease, ObjectEvent.ScoreIncreaseDisplay, ObjectEvent.Ignore, ObjectEvent.Ignore, 0, 20, false);
            var scoreComponent = new SpriteGenericComponent(SpaceGraphics.HealthAsset, score.CentreLocal, score, ObjectEvent.ScoreIncreaseDisplay, scoreCounterComponent, VerticalDrawMethod);
            score.AddPhysicsComponent(scoreCounterComponent);
            score.AddGraphicsComponent(scoreComponent);
            _scoreGameObject = score;

            DisplayLayer.GameObjects.Add(score);

            ForegroundLayer.Managers.Add(asteroidManager);
            ForegroundLayer.Managers.Add(enemyManager);

            GameConstants.GameInstance.ScoreEventHandler += ScoreMilestones;
        }

        private GameObject _scoreGameObject;

        private void ScoreMilestones(object sender, ScoreEventArgs scoreEventArgs)
        {
            if (_scoreGameObject != null)
            {
                _scoreGameObject.Event(ObjectEvent.ScoreIncrease);
            }
            if (scoreEventArgs.Score == 5)
            {
                var enemy = new GameObject(ForegroundLayer, new Vector2(GameConstants.ScreenBoundary.Center.X, 0));
                enemy.GameType = "Boss";
                var shipTexture = SpaceGraphics.BossAAsset.First();
                var enemySprite = new SpriteComponent(shipTexture);
                var enemyMovement = new MovementComponent(0.1f, FaceDirection.Down, new Vector2(0, 1));
                var enemyBullet = new BulletComponent(enemy, SpaceGraphics.BulletAsset, enemyMovement);
                var enemyBoundary = new BoundaryComponent(enemy, SpaceGraphics.BoundaryAsset.First(), shipTexture.Width,
                    shipTexture.Height);
                var enemyTimed = new TimedActionComponent(enemy, ObjectEvent.Fire, 500);
                var enemyOutOfBounds = new OutOfBoundsComponent(enemy);

                var healthCounterComponent = new CounterComponent(enemy, ObjectEvent.Collision, ObjectEvent.HealthRemoved, ObjectEvent.HealthEmpty, ObjectEvent.HealthReset, 5, 0);
                var healthBarComponent = new SpriteRepeaterComponent(SpaceGraphics.HealthBarAsset[1], new Vector2(0, 25), false, enemy, ObjectEvent.HealthRemoved, healthCounterComponent);

                enemy.AddGraphicsComponent(enemySprite);
                enemy.AddPhysicsComponent(enemyMovement);
                enemy.AddPhysicsComponent(enemyBullet);
                enemy.AddPhysicsComponent(enemyBoundary);
                enemy.AddPhysicsComponent(enemyOutOfBounds);
                enemy.AddInputComponent(enemyTimed);
                enemy.AddPhysicsComponent(healthCounterComponent);
                enemy.AddGraphicsComponent(healthBarComponent);

                ForegroundLayer.GameObjects.Add(enemy);
            }
        }

        private void PlayerDeath(GameObject gameObject)
        {
            // Could be moved into the component instead of action?
            // Or maybe set a flag on game object that other components respond to?
            gameObject.RemoveGameObject();
            var death = new GameObject(ForegroundLayer, gameObject.TopLeft);
            var deathSprite = new SpriteComponent(SpaceGraphics.PlanetAsset[3]);
            death.AddGraphicsComponent(deathSprite);
            ForegroundLayer.GameObjects.Add(death);
        }


        private readonly Random _random = new Random();

        private IEnumerable<Vector2> RandomDrawMethod(int requiredValues, int width)
        {
            for (var i = 0; i < requiredValues; i++)
            {
                var xValue = _random.Next(-width, width);
                var yValue = _random.Next(-width, width);
                yield return new Vector2(xValue, yValue);
            }
        }

        private IEnumerable<Vector2> VerticalDrawMethod(int requiredValues, int height)
        {
            for (var i = 0; i < requiredValues; i++)
            {
                var yValue = height * i;
                yield return new Vector2(0, yValue);
            }
        }
    }
}
