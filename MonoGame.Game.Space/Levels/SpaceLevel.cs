﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Common.Components;
using MonoGame.Common.Components.Animation;
using MonoGame.Common.Components.Audio;
using MonoGame.Common.Components.Graphics;
using MonoGame.Common.Components.Logic;
using MonoGame.Common.Components.Movement;
using MonoGame.Common.Components.States;
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

            var backgroundEnemyManager = new EnemyManager(TopDown.EnemyName, TopDown.EnemyBulletName, SpaceGraphics.MiniEnemyShipAsset.First(), 5000, 10000, BackgroundLayer, 2, SpaceSounds.Sound_Explosion01, 10, 20);
            BackgroundLayer.Managers.Add(backgroundEnemyManager);
        }

        protected override void LoadDisplay()
        {

        }

        protected override void LoadForeground()
        {
            var enemyManager = new EnemyManager(TopDown.EnemyName, TopDown.EnemyBulletName, SpaceGraphics.EnemyShipAsset.First(), 100, 1000, ForegroundLayer, 2, SpaceSounds.Sound_Explosion01, 5, 50);
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

            // PLAYER
            var player = new GameObject(TopDown.PlayerName, new Vector2(xPosition, yPosition));

            // GENERAL
            var texture = SpaceGraphics.PlayerShipAsset.First();
            var sprite = new SpriteComponent(texture);
            var boundary = new BoundaryComponent(SpaceGraphics.BoundaryAsset.First(), texture.Width, texture.Height);
            player.AddComponent(boundary);
            player.AddComponent(sprite);            

            // MOVEMENT
            var movement = new MovementComponent(5, FaceDirection.Up, Vector2.Zero);
            var localKeyboard = new LocalKeyboardComponent();
            var input = new InputComponent(InputHelper.KeyboardMappedKey(), localKeyboard, movement);
            player.AddComponent(movement);
            player.AddComponent(input);

            // THRUSTER            
            var thrusterLeft = new SpriteComponent(SpaceGraphics.FlameAsset, new Vector2(2, 16), color: Color.DarkRed);
            var thrusterRight = new SpriteComponent(SpaceGraphics.FlameAsset, new Vector2(11, 16), color: Color.DarkRed);
            player.AddComponent(thrusterLeft);
            player.AddComponent(thrusterRight);

            // STATE
            var state = new StateComponent();
            var isMoving = "IsMoving";
            var notMoving = "NotMoving";
            state.AddComponentState(thrusterLeft, isMoving, notMoving);
            state.AddComponentState(thrusterRight, isMoving, notMoving);
            var movementState = new MovementStateComponent(movement, isMoving, notMoving);
            player.AddComponent(movementState);
            player.AddComponent(state);
                                    
            // HEALTH
            var healthCounter = new CounterIncrementComponent(ObjectEvent.CollisionEnter, ObjectEvent.HealthRemoved, ObjectEvent.HealthEmpty, ObjectEvent.HealthReset, 5, 0);
            var healthBar = new SpriteRepeaterComponent(SpaceGraphics.HealthBarAsset.First(), new Vector2(0, 35), false, ObjectEvent.HealthRemoved, healthCounter, false, Color.ForestGreen);
            //healthBar.SetDynamicColors(Color.DarkRed, Color.Red, Color.Orange, Color.Yellow, Color.ForestGreen);
            healthBar.SetColorEvent(ObjectEvent.HealthRemoved, HealthColourFunc);
            player.AddComponent(healthCounter);
            player.AddComponent(healthBar); 

            // FIRING
            var bullet = new BulletComponent(TopDown.PlayerBulletName, SpaceGraphics.LargeBulletAsset, movement, ObjectEvent.AmmoRemoved, ObjectEvent.AmmoEmpty, ObjectEvent.AmmoReset, 10, Color.DarkOrange);
            var ammoCounter = new CounterIncrementComponent(ObjectEvent.Fire, ObjectEvent.AmmoRemoved, ObjectEvent.AmmoEmpty, ObjectEvent.AmmoReset, 50, 0);
            var ammoBar = new SpriteRepeaterComponent(SpaceGraphics.OnePixelBarAsset.First(), new Vector2(-25, 25), true, ObjectEvent.AmmoRemoved, ammoCounter, true, Color.Gray);
            var ammoMovement = new EventMovementComponent(new Vector2(0, 5), ObjectEvent.AmmoRemoved);
            var ammoSound = new EventSoundComponent(SpaceSounds.Sound_ShortFire01, ObjectEvent.AmmoRemoved);
            player.AddComponent(bullet);
            player.AddComponent(ammoCounter);
            player.AddComponent(ammoMovement);                        
            player.AddComponent(ammoBar);
            player.AddComponent(ammoSound);

            // DAMAGE
            var fireCounter = new CounterIncrementComponent(ObjectEvent.CollisionEnter, ObjectEvent.ElectricalFire, ObjectEvent.Ignore, ObjectEvent.Ignore, 0, 5, false);
            var fireSprite = new SpriteGenericComponent(SpaceGraphics.FireAsset, Vector2.Zero, ObjectEvent.ElectricalFire, fireCounter, RandomDrawMethod);
            player.AddComponent(fireCounter);
            player.AddComponent(fireSprite);

            // DEATH
            var deathEvent = new ObjectEventComponent(ObjectEvent.HealthEmpty, PlayerDeath);
            player.AddComponent(deathEvent);
                                                                                               
            ForegroundLayer.AddGameObject(player);
        }

        private readonly Stack<Color> _playerColourStack = new Stack<Color>(new[] { Color.Gray, Color.DarkRed, Color.Red, Color.Yellow, Color.Orange });

        private Color HealthColourFunc(GameObject gameObject)
        {
            var colour = _playerColourStack.Pop();
            return colour;
        }

        private void PlayerDeath(GameObject gameObject)
        {            
            gameObject.RemoveGameObject();
            var death = new GameObject("Death", gameObject.TopLeft);
            var deathAnimation = new ScaleAnimationComponent(SpaceGraphics.LargeExpolosionAsset[0], 0, 50, 3000, Color.DarkRed, null, ObjectEvent.RemoveEntity);
            var exitLevel = new ObjectEventComponent(ObjectEvent.RemoveEntity, ExitLevelAction);
            death.AddComponent(deathAnimation);
            death.AddComponent(exitLevel);
            DisplayLayer.AddGameObject(death);
        }

        private void ExitLevelAction(GameObject gameObject)
        {
            BackLevel();
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
            if (scoreEventArgs.Score == 5)
            {
                CreateBossOne(750);
            }
            if (scoreEventArgs.Score == 10)
            {                
                CreateBossOne(500, 2);
            }
        }

        #endregion

        #region Boss

        private void CreateBossOne(int enemyBulletDelay, int scale = 1)
        {
            var xPosition = GameHelper.GetRelativeX(0.5f);
            var enemy = new GameObject("Boss", new Vector2(xPosition, 0)) {Scale = scale};

            var shipTexture = SpaceGraphics.BossAAsset.First();
            var enemySprite = new SpriteComponent(shipTexture);
            var enemyMovement = new MovementComponent(0.1f, FaceDirection.Down, new Vector2(0, 1));
            var enemyBullet = new BulletComponent(TopDown.EnemyBulletName, SpaceGraphics.BulletAsset, enemyMovement);
            var enemyBoundary = new BoundaryComponent(SpaceGraphics.BoundaryAsset.First(), shipTexture.Width, shipTexture.Height);
            var enemyTimed = new TimedActionComponent(ObjectEvent.Fire, enemyBulletDelay);
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
            var deathAnimation = new ScaleAnimationComponent(SpaceGraphics.PlanetAsset[3], 0, 10, 500, animationCompleteEvent: ObjectEvent.RemoveEntity);
            death.AddComponent(deathAnimation);
            ForegroundLayer.AddGameObject(death);
        }

        #endregion


    }
}
