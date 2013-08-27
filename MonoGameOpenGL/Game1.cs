﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using MonoGameOpenGL.Entities;
using MonoGameOpenGL.Managers;

#endregion

namespace MonoGameOpenGL
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private GameState gameState;
        private AsteroidManager asteroidManager;
        private CollisionManager collisionManager;
        private EnemyManager enemyManager;

        public Game1()
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
            gameState = new GameState();
            collisionManager = new CollisionManager(gameState);

            var bulletAsteroidCollision = new CollisionType
            {                
                TypeA = typeof(Bullet),
                TypeB = typeof(Asteroid),
                Action = (bullet, asteroid) =>
                {
                    bullet.IsRemoved = true;
                    asteroid.IsRemoved = true;
                }
            };

            var playerAsteroidCollision = new CollisionType
            {
                TypeA = typeof(PlayerShip),
                TypeB = typeof(Asteroid),
                Action = (ship, asteroid) =>
                {
                    asteroid.IsRemoved = true;
                    (ship as PlayerShip).HealthManager.RemoveLife();                                                                
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
            asteroidManager = new AsteroidManager(asteroids, gameState);

            enemyManager = new EnemyManager(Content.Load<Texture2D>("EnemyShip"), Content.Load<Texture2D>("Bullet"), gameState);

            var playerStartPosition = new Vector2(GameConstants.ScreenBoundary.Width/2, GameConstants.ScreenBoundary.Height-50);
            var playerShip = new PlayerShip(Content.Load<Texture2D>("PlayerShip"), playerStartPosition, Content.Load<Texture2D>("Bullet"), Content.Load<Texture2D>("Health"), 5, gameState);            
            gameState.GameEntities.Add(playerShip);
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

            gameState.Update(gameTime);
            asteroidManager.Update(gameTime);
            collisionManager.Update(gameTime);
            enemyManager.Update(gameTime);

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
            gameState.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);

            frames++;
            timeElapsedMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeElapsedMilliseconds > 2000)
            {
                var fps = frames/(timeElapsedMilliseconds/1000);
                System.Diagnostics.Debug.WriteLine("FPS {0:N0}", fps);
                timeElapsedMilliseconds = 0;
                frames = 0;
            }
        }
    }
}
