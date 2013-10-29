﻿using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Components;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Infrastructure;
using MonoGame.Graphics.Space;
using MonoGameOpenGL.Interfaces;

namespace MonoGame.Game.Common.Managers
{
    public class EnemyManager : IManager
    {
        private readonly Texture2D _shipTexture;
        private readonly Texture2D _bulletTexture;
        private readonly GameLayer _gameLayer;
        private readonly int _speed;
        private double _elapsedTimeMilliseconds;
        private readonly Random _random;
        private readonly double _spawnDelayMilliseconds;
        private readonly double _bulletDelayMilliseconds;

        public EnemyManager(Texture2D shipTexture, Texture2D bulletTexture, double spawnDelayMilliseconds, double bulletDelayMilliseconds, GameLayer gameLayer, int speed)
        {
            _shipTexture = shipTexture;
            _bulletTexture = bulletTexture;
            _gameLayer = gameLayer;
            _speed = speed;
            _random = new Random();
            _spawnDelayMilliseconds = spawnDelayMilliseconds;
            _bulletDelayMilliseconds = bulletDelayMilliseconds;
        }


        public void Update(GameTime gameTime)
        {
            var enemyCount = _gameLayer.GameObjects.Count(gameObject => gameObject.GameType == "Enemy");

            _elapsedTimeMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_elapsedTimeMilliseconds > _spawnDelayMilliseconds && enemyCount < GameConstants.MaximumEnemies)
            {
                _elapsedTimeMilliseconds = 0;
                var xLocation = _random.Next(0, GameConstants.ScreenBoundary.Right - _shipTexture.Width);

                var enemy = new GameObject(_gameLayer, new Vector2(xLocation, 0));
                enemy.GameType = "Enemy";
                var enemySprite = new SpriteComponent(_shipTexture);
                var enemyMovement = new MovementComponent(1, FaceDirection.Down, new Vector2(0, 1));
                var enemyBullet = new BulletComponent(enemy, SpaceGraphics.BulletAsset, enemyMovement);
                var enemyBoundary = new BoundaryComponent(enemy, SpaceGraphics.BoundaryAsset.First(), _shipTexture.Width,
                    _shipTexture.Height);
                var enemyInstance = new InstanceComponent(enemy);
                var enemyTimed = new TimedActionComponent(enemy, ObjectEvent.Fire, 1000);
                enemy.AddGraphicsComponent(enemySprite);
                enemy.AddPhysicsComponent(enemyMovement);
                enemy.AddPhysicsComponent(enemyBullet);
                enemy.AddPhysicsComponent(enemyBoundary);
                enemy.AddPhysicsComponent(enemyInstance);
                enemy.AddInputComponent(enemyTimed);
                _gameLayer.GameObjects.Add(enemy);
            }
        }
    }
}
