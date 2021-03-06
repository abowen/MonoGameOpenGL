﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Components;
using MonoGame.Common.Components.Audio;
using MonoGame.Common.Components.Boundary;
using MonoGame.Common.Components.Graphics;
using MonoGame.Common.Components.Movement;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;
using MonoGame.Graphics.Space;

namespace MonoGame.Common.Managers
{
    public class EnemyManager : IManager
    {
        private readonly string _enemyName;
        private readonly string _bulletName;
        private readonly Texture2D _shipTexture;        
        private readonly GameLayer _gameLayer;
        private readonly int _speed;
        private readonly SoundEffect _deathSound;
        private double _elapsedTimeMilliseconds;
        private readonly Random _random;
        private readonly double _spawnDelayMilliseconds;
        private readonly double _bulletDelayMilliseconds;
        private int _currentMaximumEnemies;
        private readonly int _totalMaximumEnemies;

        public EnemyManager(string enemyName, string bulletName, Texture2D shipTexture, double spawnDelayMilliseconds, double bulletDelayMilliseconds, GameLayer gameLayer, int speed, SoundEffect deathSound, int currentMaximumEnemies, int totalMaximumEnemies)
        {
            _enemyName = enemyName;
            _bulletName = bulletName;
            _shipTexture = shipTexture;            
            _gameLayer = gameLayer;
            _speed = speed;
            _deathSound = deathSound;
            _random = new Random();
            _spawnDelayMilliseconds = spawnDelayMilliseconds;
            _bulletDelayMilliseconds = bulletDelayMilliseconds;
            _currentMaximumEnemies = currentMaximumEnemies;
            _totalMaximumEnemies = totalMaximumEnemies;
        }

        public void AddObjectEventAction(ObjectEvent objectEvent, Action<GameObject> action)
        {
           _eventActions.Add(new Tuple<ObjectEvent, Action<GameObject>>(objectEvent, action));
        }

        private readonly List<Tuple<ObjectEvent, Action<GameObject>>> _eventActions = new List<Tuple<ObjectEvent, Action<GameObject>>>(); 

        public void Update(GameTime gameTime)
        {
            var enemyCount = _gameLayer.GameObjects.Count(gameObject => gameObject.GameType == _enemyName);
            _elapsedTimeMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;            

            if (_elapsedTimeMilliseconds > _spawnDelayMilliseconds &&
                enemyCount < _currentMaximumEnemies &&
                enemyCount < _totalMaximumEnemies)
            {
                _elapsedTimeMilliseconds = 0;
                var xLocation = _random.Next(0, GameConstants.ScreenBoundary.Right - _shipTexture.Width);

                var gameObject = new GameObject(_enemyName, new Vector2(xLocation, 0));
                var sprite = new SpriteComponent(_shipTexture);
                var movement = new MovementComponent(1, FaceDirection.Down, new Vector2(0, _speed));
                gameObject.AddComponent(sprite);
                gameObject.AddComponent(movement);

                var bullet = new BulletComponent(_bulletName, SpaceGraphics.BulletAsset[0], movement, ignoreCollisionTypes: _enemyName);
                var timedAction = new TimedActionComponent(ObjectEvent.Fire, _bulletDelayMilliseconds);
                gameObject.AddComponent(bullet);
                gameObject.AddComponent(timedAction);

                var boundary = new BoundaryComponent(SpaceGraphics.BoundaryAsset.First(), _shipTexture.Width, _shipTexture.Height, true, false, _enemyName);
                var instance = new InstanceComponent();
                var outOfBounds = new OutOfBoundsComponent(ObjectEvent.RemoveEntity);
                gameObject.AddComponent(boundary);
                gameObject.AddComponent(instance);
                gameObject.AddComponent(outOfBounds);
                
                var score = new ObjectEventComponent(ObjectEvent.CollisionEnter, IncreaseScore);
                var deathSound = new EventSoundComponent(_deathSound, ObjectEvent.CollisionEnter);
                var collisionAction = new ObjectEventComponent(ObjectEvent.CollisionEnter, CollisionAction);
                gameObject.AddComponent(score);
                gameObject.AddComponent(deathSound);
                gameObject.AddComponent(collisionAction);

                foreach (var objectEventAction in _eventActions)
                {
                    var objectEvent = new ObjectEventComponent(objectEventAction.Item1, objectEventAction.Item2);
                    gameObject.AddComponent(objectEvent);
                }

                _gameLayer.AddGameObject(gameObject);
            }
        }

        private void CollisionAction(GameObject gameObject)
        {
            _currentMaximumEnemies++;
        }

        private static void IncreaseScore(GameObject gameObject)
        {
            GameConstants.GameInstance.UpdateScore();            
        }
    }
}
