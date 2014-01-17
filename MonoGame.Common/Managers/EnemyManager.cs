using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Components;
using MonoGame.Common.Components.Audio;
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

        public EnemyManager(Texture2D shipTexture, double spawnDelayMilliseconds, double bulletDelayMilliseconds, GameLayer gameLayer, int speed, SoundEffect deathSound, int currentMaximumEnemies, int totalMaximumEnemies)
        {
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

        private const string EnemyGameType = "Enemy";

        public void Update(GameTime gameTime)
        {            
            var enemyCount = _gameLayer.GameObjects.Count(gameObject => gameObject.GameType == EnemyGameType);
            _elapsedTimeMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;            

            if (_elapsedTimeMilliseconds > _spawnDelayMilliseconds &&
                enemyCount < _currentMaximumEnemies &&
                enemyCount < _totalMaximumEnemies)
            {
                _elapsedTimeMilliseconds = 0;
                var xLocation = _random.Next(0, GameConstants.ScreenBoundary.Right - _shipTexture.Width);
                var gameObject = new GameObject(EnemyGameType, new Vector2(xLocation, 0));
                var sprite = new SpriteComponent(_shipTexture);
                var movement = new MovementComponent(1, FaceDirection.Down, new Vector2(0, _speed));
                var bullet = new BulletComponent(SpaceGraphics.BulletAsset, movement, ignoreCollisionTypes: EnemyGameType);
                var boundary = new BoundaryComponent(SpaceGraphics.BoundaryAsset.First(), _shipTexture.Width, _shipTexture.Height, true, EnemyGameType);
                var instance = new InstanceComponent();
                var timedAction = new TimedActionComponent(ObjectEvent.Fire, _bulletDelayMilliseconds);
                var outOfBounds = new OutOfBoundsComponent(ObjectEvent.RemoveEntity);
                var score = new ObjectEventComponent(ObjectEvent.CollisionEnter, IncreaseScore);
                var deathSound = new EventSoundComponent(_deathSound, ObjectEvent.CollisionEnter);
                var collisionAction = new ObjectEventComponent(ObjectEvent.CollisionEnter, CollisionAction);
                gameObject.AddComponent(sprite);
                gameObject.AddComponent(movement);
                gameObject.AddComponent(bullet);
                gameObject.AddComponent(boundary);
                gameObject.AddComponent(instance);
                gameObject.AddComponent(outOfBounds);
                gameObject.AddComponent(timedAction);
                gameObject.AddComponent(score);
                gameObject.AddComponent(deathSound);
                gameObject.AddComponent(collisionAction);
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
