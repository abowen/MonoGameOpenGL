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

            var bulletAsteroidCollision = new CollisionType()
            {
                TypeA = typeof(Bullet),
                TypeB = typeof(Asteroid),
                Action = (bullet, asteroid) =>
                {
                    bullet.IsRemoved = true;
                    asteroid.IsRemoved = true;
                }
            };

            collisionManager.CollisionTypes.Add(bulletAsteroidCollision);
                                    
            var asteroids = new[]
            {
                Content.Load<Texture2D>("Asteroid01"),
                Content.Load<Texture2D>("Asteroid02"),
                Content.Load<Texture2D>("Asteroid03"),
                Content.Load<Texture2D>("Asteroid04"),
            };
            asteroidManager = new AsteroidManager(asteroids, gameState);

            var playerShip = new PlayerShip(Content.Load<Texture2D>("PlayerShip"), new Vector2(0, 50f), Content.Load<Texture2D>("Bullet"), gameState);

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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            gameState.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
