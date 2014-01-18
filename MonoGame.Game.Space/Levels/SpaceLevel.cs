using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Common.Components;
using MonoGame.Common.Components.Animation;
using MonoGame.Common.Components.Audio;
using MonoGame.Common.Components.Graphics;
using MonoGame.Common.Components.Logic;
using MonoGame.Common.Components.Movement;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Events;
using MonoGame.Common.Helpers;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Managers;
using MonoGame.Graphics.Common;
using MonoGame.Graphics.Space;
using MonoGame.Sounds.Space;

namespace MonoGame.Game.Space.Levels
{
    public class SpaceLevel : GameLevel
    {
        private GameObject _scoreGameObject;
        private readonly Random _random = new Random();

        public SpaceLevel() : base(2f) { }

        protected override void LoadBackground()
        {
            var backgroundManager = new BackgroundManager(SpaceGraphics.PlanetAsset, SpaceGraphics.StarAsset, BackgroundLayer, new Vector2(0, 0.1f), new Vector2(0, 0.5f), 3000, 30);
            BackgroundLayer.Managers.Add(backgroundManager);
            backgroundManager.VerticalBoundary(0, 0);

            var backgroundEnemyManager = new EnemyManager(SpaceGraphics.MiniEnemyShipAsset.First(), 5000, 10000, BackgroundLayer, 2, SpaceSounds.Sound_Explosion01, 10, 20);
            BackgroundLayer.Managers.Add(backgroundEnemyManager);
        }

        protected override void LoadDisplay()
        {

        }

        protected override void LoadForeground()
        {            
            var enemyManager = new EnemyManager(SpaceGraphics.EnemyShipAsset.First(), 100, 1000, ForegroundLayer, 2, SpaceSounds.Sound_Explosion01, 5, 50);
            ForegroundLayer.Managers.Add(enemyManager);

            var asteroidManager = new AsteroidManager(SpaceGraphics.AsteroidAsset, SpaceGraphics.MiniAsteroidAsset, ForegroundLayer);
            ForegroundLayer.Managers.Add(asteroidManager);

            CreatePlayer();
            CreateScoreDisplay();
        }

        #region Player


        private void CreatePlayer()
        {
            var xPosition = GameHelper.GetRelativeX(0.5f);
            var yPosition = GameHelper.GetRelativeY(0.8f);

            var player = new GameObject("Player", new Vector2(xPosition, yPosition));
            var playerTexture = SpaceGraphics.PlayerShipAsset.First();
            var playerSpriteComponent = new SpriteComponent(playerTexture);
            var playerMovementComponent = new MovementComponent(5, FaceDirection.Up, Vector2.Zero);
            var playerLocalKeyboardComponent = new LocalKeyboardComponent();
            var playerInputComponent = new InputComponent(InputHelper.KeyboardMappedKey(), playerLocalKeyboardComponent, playerMovementComponent);

            var playerBoundaryComponent = new BoundaryComponent(SpaceGraphics.BoundaryAsset.First(), playerTexture.Width, playerTexture.Height);

            var playerHealthCounterComponent = new CounterIncrementComponent(ObjectEvent.CollisionEnter,
                ObjectEvent.HealthRemoved, ObjectEvent.HealthEmpty, ObjectEvent.HealthReset, 5, 0);
            var playerHealthBarComponent = new SpriteRepeaterComponent(SpaceGraphics.HealthBarAsset.First(), new Vector2(0, 25),
                false, ObjectEvent.HealthRemoved, playerHealthCounterComponent);

            var playerBulletComponent = new BulletComponent(SpaceGraphics.LargeBulletAsset, playerMovementComponent,
                ObjectEvent.AmmoRemoved, ObjectEvent.AmmoEmpty, ObjectEvent.AmmoReset, 10);
            var playerAmmoCounterComponent = new CounterIncrementComponent(ObjectEvent.Fire, ObjectEvent.AmmoRemoved,
                ObjectEvent.AmmoEmpty, ObjectEvent.AmmoReset, 50, 0);
            var playerAmmoBarComponent = new SpriteRepeaterComponent(SpaceGraphics.OnePixelBarAsset.First(),
                new Vector2(-25, 25), true, ObjectEvent.AmmoRemoved, playerAmmoCounterComponent, true, Color.Gray);
            var playerEventMovement = new EventMovementComponent(new Vector2(0, 5), ObjectEvent.AmmoRemoved);
            var playerEventSound = new EventSoundComponent(SpaceSounds.Sound_ShortFire01, ObjectEvent.AmmoRemoved);

            var playerFireCounterComponent = new CounterIncrementComponent(ObjectEvent.CollisionEnter, ObjectEvent.WoodFire,
                ObjectEvent.Ignore, ObjectEvent.Ignore, 0, 5, false);
            var playerWoodFireComponent = new SpriteGenericComponent(SpaceGraphics.FireAsset, player.CentreLocal,
                ObjectEvent.WoodFire, playerFireCounterComponent, RandomDrawMethod);

            var playerEventComponent = new ObjectEventComponent(ObjectEvent.HealthEmpty, PlayerDeath);

            player.AddComponent(playerBoundaryComponent);
            player.AddComponent(playerSpriteComponent);
            player.AddComponent(playerEventSound);
            player.AddComponent(playerMovementComponent);
            player.AddComponent(playerInputComponent);
            player.AddComponent(playerEventMovement);
            player.AddComponent(playerBulletComponent);
            player.AddComponent(playerHealthCounterComponent);
            player.AddComponent(playerHealthBarComponent);
            player.AddComponent(playerAmmoCounterComponent);
            player.AddComponent(playerAmmoBarComponent);
            player.AddComponent(playerFireCounterComponent);
            player.AddComponent(playerWoodFireComponent);
            player.AddComponent(playerEventComponent);
            ForegroundLayer.AddGameObject(player);
        }

        private void PlayerDeath(GameObject gameObject)
        {
            // Could be moved into the component instead of action?
            // Or maybe set a flag on game object that other components respond to?
            gameObject.RemoveGameObject();
            var death = new GameObject("Death", gameObject.TopLeft);
            var deathSprite = new SpriteComponent(SpaceGraphics.PlanetAsset[3]);
            death.AddComponent(deathSprite);
            ForegroundLayer.AddGameObject(death);
        }

        private IEnumerable<Vector2> RandomDrawMethod(int requiredValues, int width)
        {
            for (var i = 0; i < requiredValues; i++)
            {
                var xValue = _random.Next(-width, width);
                var yValue = _random.Next(-width, width);
                yield return new Vector2(xValue, yValue);
            }
        }

        #endregion

        #region Score

        private void CreateScoreDisplay()
        {
            var score = new GameObject("Score", new Vector2(10, 10));
            var counterComponent = new CounterIncrementComponent(ObjectEvent.ScoreIncrease, ObjectEvent.ScoreIncreaseDisplay, ObjectEvent.Ignore, ObjectEvent.Ignore, 0, 100, false);
            var scoreComponent = new TextComponent(FontGraphics.PropertialFont_8X8, counterComponent);
            score.AddComponent(counterComponent);
            score.AddComponent(scoreComponent);
            DisplayLayer.AddGameObject(score);

            _scoreGameObject = score;
            GameConstants.GameInstance.ScoreEventHandler += ScoreMilestones;
        }
        
        private void ScoreMilestones(object sender, ScoreEventArgs scoreEventArgs)
        {
            if (_scoreGameObject != null)
            {
                _scoreGameObject.Event(ObjectEvent.ScoreIncrease);
            }
            if (scoreEventArgs.Score == 1)
            {
                CreateBossOne();
            }
        }

        #endregion

        #region Boss

        private void CreateBossOne()
        {
            var xPosition = GameHelper.GetRelativeX(0.5f);
            var enemy = new GameObject("BossOne", new Vector2(xPosition, 0));
            var shipTexture = SpaceGraphics.BossAAsset.First();
            var enemySprite = new SpriteComponent(shipTexture);
            var enemyMovement = new MovementComponent(0.1f, FaceDirection.Down, new Vector2(0, 1));
            var enemyBullet = new BulletComponent(SpaceGraphics.BulletAsset, enemyMovement);
            var enemyBoundary = new BoundaryComponent(SpaceGraphics.BoundaryAsset.First(), shipTexture.Width, shipTexture.Height);
            var enemyTimed = new TimedActionComponent(ObjectEvent.Fire, 500);
            var enemyOutOfBounds = new OutOfBoundsComponent(ObjectEvent.RemoveEntity);
            var healthCounterComponent = new CounterIncrementComponent(ObjectEvent.CollisionEnter, ObjectEvent.HealthRemoved, ObjectEvent.HealthEmpty, ObjectEvent.HealthReset, 5, 0);
            var healthBarComponent = new SpriteRepeaterComponent(SpaceGraphics.HealthBarAsset[1], new Vector2(0, 25), false, ObjectEvent.HealthRemoved, healthCounterComponent);
            var deathAction = new ObjectEventComponent(ObjectEvent.HealthEmpty, BossDeath);

            enemy.AddComponent(enemySprite);
            enemy.AddComponent(enemyMovement);
            enemy.AddComponent(enemyBullet);
            enemy.AddComponent(enemyBoundary);
            enemy.AddComponent(enemyOutOfBounds);
            enemy.AddComponent(enemyTimed);
            enemy.AddComponent(healthCounterComponent);
            enemy.AddComponent(healthBarComponent);
            enemy.AddComponent(deathAction);

            ForegroundLayer.AddGameObject(enemy);
        }

        private void BossDeath(GameObject gameObject)
        {            
            gameObject.RemoveGameObject();
            var death = new GameObject("BossDeath", gameObject.Centre);
            var deathSprite = new ScaleAnimationComponent(SpaceGraphics.PlanetAsset[3], 0, 10, 500, animationCompleteEvent:  ObjectEvent.RemoveEntity);
            death.AddComponent(deathSprite);
            ForegroundLayer.AddGameObject(death);
        }

        #endregion


    }
}
