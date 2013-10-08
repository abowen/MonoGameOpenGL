using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Graphics.Space;
using MonoGameOpenGL.Entities;
using MonoGameOpenGL.Enums;
using MonoGameOpenGL.Helpers;
using MonoGameOpenGL.Infrastructure;
using MonoGameOpenGL.Managers;

namespace MonoGameOpenGL
{
    public class TopDownGame
    {
        //protected readonly List<GameLevel> _levels = new List<GameLevel>();

        private readonly GameLayer _game = new GameLayer(GameLayerDepth.Game);
        private readonly GameLayer _background = new GameLayer(GameLayerDepth.Background);

        public void Update(GameTime gameTime)
        {
            _game.Update(gameTime);
            _background.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _game.Draw(spriteBatch);
            _background.Draw(spriteBatch);
        }

        public TopDownGame(GameWindow window, ContentManager contentManager)
        {
            SpaceGraphics.LoadSpaceContent(contentManager);
            GameConstants.ScreenBoundary = new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height);            
            LoadBackground(_background);
            LoadGame(_game);
        }

        public void LoadBackground(GameLayer gameLayer)
        {
            var backgroundManager = new BackgroundManager(SpaceGraphics.PlanetAsset, SpaceGraphics.StarAsset, gameLayer);
            var backgroundEnemyManager = new EnemyManager(SpaceGraphics.MiniEnemyShipAsset.First(), SpaceGraphics.MiniBulletAsset.First(), 5000, 0, gameLayer, 2);
            gameLayer.Managers.Add(backgroundManager);
            gameLayer.Managers.Add(backgroundEnemyManager);
        }

        public void LoadGame(GameLayer gameLayer)
        {
            // TODO: Refactor into generic collision manager into more event driven / composition manner
            var collisionManager = new CollisionManager(gameLayer);
            
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

            collisionManager.CollisionTypes.Add(bulletAsteroidCollision);
            collisionManager.CollisionTypes.Add(playerAsteroidCollision);

            var asteroidManager = new AsteroidManager(SpaceGraphics.AsteroidAsset, SpaceGraphics.MiniAsteroidAsset, gameLayer);


            var enemyManager = new EnemyManager(SpaceGraphics.EnemyShipAsset.First(), SpaceGraphics.BulletAsset.First(), 1500, 2000, gameLayer, 1);
            

            var playerStartPosition = new Vector2(GameConstants.ScreenBoundary.Width / 2, GameConstants.ScreenBoundary.Height - 50);
            var playerShip = new PlayerShip(SpaceGraphics.PlayerShipAsset.First(), playerStartPosition, SpaceGraphics.BulletAsset.First(), SpaceGraphics.HealthAsset.First(), 5, FaceDirection.Up, gameLayer, InputHelper.KeyboardMappedKey());
            gameLayer.GameEntities.Add(playerShip);

            gameLayer.Managers.Add(collisionManager);
            gameLayer.Managers.Add(asteroidManager);
            gameLayer.Managers.Add(enemyManager);
        }
    }
}
