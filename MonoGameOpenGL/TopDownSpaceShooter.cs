#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using MonoGameOpenGL.Entities;
using MonoGameOpenGL.Enums;
using MonoGameOpenGL.Managers;

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

        public TopDownSpaceShooter()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            // Create a new SpriteBatch, which can be used to draw textures.
            GameConstants.ScreenBoundary = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = new GameLayer(GameLayerDepth.Background);
            var moon = new Planet(Content.Load<Texture2D>("Moon01"), new Vector2(200, 50), FaceDirection.Bottom, _background);
            var planet01 = new Planet(Content.Load<Texture2D>("Planet01"), new Vector2(600, 250), FaceDirection.Bottom, _background);
            var planet02 = new Planet(Content.Load<Texture2D>("Planet02"), new Vector2(100, 300), FaceDirection.Bottom, _background);
            _background.GameEntities.Add(moon);
            _background.GameEntities.Add(planet01);
            _background.GameEntities.Add(planet02);

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

            var asteroids = new[]
            {
                Content.Load<Texture2D>("Asteroid01"),
                Content.Load<Texture2D>("Asteroid02"),
                Content.Load<Texture2D>("Asteroid03"),
                Content.Load<Texture2D>("Asteroid04"),
            };
            var miniAsteroids = new[]
            {
                Content.Load<Texture2D>("MiniAsteroid01"),
                Content.Load<Texture2D>("MiniAsteroid02"),
                Content.Load<Texture2D>("MiniAsteroid03"),
                Content.Load<Texture2D>("MiniAsteroid04"),
            };
            asteroidManager = new AsteroidManager(asteroids, miniAsteroids, _game);

            enemyManager = new EnemyManager(Content.Load<Texture2D>("EnemyShip"), Content.Load<Texture2D>("Bullet"), _game, 1500, 2000);
            backgroundEnemyManager = new EnemyManager(Content.Load<Texture2D>("MiniEnemyShip"), Content.Load<Texture2D>("MiniBullet"), _background, 5000, 0);

            var playerStartPosition = new Vector2(GameConstants.ScreenBoundary.Width / 2, GameConstants.ScreenBoundary.Height - 50);
            var playerShip = new PlayerShip(Content.Load<Texture2D>("PlayerShip"), playerStartPosition, Content.Load<Texture2D>("Bullet"), Content.Load<Texture2D>("Health"), 5, _game);
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
