﻿#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Graphics.Space;
using MonoGameOpenGL.Entities;
using MonoGameOpenGL.Enums;
using MonoGameOpenGL.Helpers;
using MonoGameOpenGL.Managers;
using System.Linq;


#endregion

namespace MonoGameOpenGL
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TopDownSpaceShooter : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private GameLayer _game;
        private GameLayer _background;
        private AsteroidManager asteroidManager;
        private CollisionManager collisionManager;
        private EnemyManager enemyManager;
        private EnemyManager backgroundEnemyManager;
        private BackgroundManager backgroundManager;

        public TopDownSpaceShooter()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            SpaceGraphics.LoadSpaceContent(Content);

            // Create a new SpriteBatch, which can be used to draw textures.
            GameConstants.ScreenBoundary = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = new GameLayer(GameLayerDepth.Background);
            backgroundManager = new BackgroundManager(SpaceGraphics.PlanetAsset, SpaceGraphics.StarAsset, _background);

            _game = new GameLayer(GameLayerDepth.Game);

            collisionManager = new CollisionManager(_game);

            // TODO: Refactor into generic collision manager into more event driven / composition manner
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


            asteroidManager = new AsteroidManager(SpaceGraphics.AsteroidAsset, SpaceGraphics.MiniAsteroidAsset, _game);

            enemyManager = new EnemyManager(SpaceGraphics.EnemyShipAsset.First(), SpaceGraphics.BulletAsset.First(), 1500, 2000, _game, 1);
            backgroundEnemyManager = new EnemyManager(SpaceGraphics.MiniEnemyShipAsset.First(), SpaceGraphics.MiniBulletAsset.First(), 5000, 0, _background, 2);

            var playerStartPosition = new Vector2(GameConstants.ScreenBoundary.Width / 2, GameConstants.ScreenBoundary.Height - 50);
            var playerShip = new PlayerShip(SpaceGraphics.PlayerShipAsset.First(), playerStartPosition, SpaceGraphics.BulletAsset.First(), SpaceGraphics.HealthAsset.First(), 5, FaceDirection.Up, _game, InputHelper.KeyboardMappedKey());
            _game.GameEntities.Add(playerShip);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Actually draw these based off a Z-Index instead of coding artifacts
            _background.Update(gameTime);
            _game.Update(gameTime);
            asteroidManager.Update(gameTime);
            collisionManager.Update(gameTime);
            enemyManager.Update(gameTime);
            backgroundEnemyManager.Update(gameTime);
            backgroundManager.Update(gameTime);

            base.Update(gameTime);
        }

        private int frames;
        private double timeElapsedMilliseconds;        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            _background.Draw(spriteBatch);
            _game.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);

            frames++;
            timeElapsedMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeElapsedMilliseconds > 2000)
            {
                var fps = frames / (timeElapsedMilliseconds / 1000);
                System.Diagnostics.Debug.WriteLine("FPS {0:N0}", fps);
                timeElapsedMilliseconds = 0;
                frames = 0;
            }
        }
    }
}
