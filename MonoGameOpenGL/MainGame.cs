#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Game.Rpg;
using MonoGame.Game.Space;

#endregion

namespace MonoGameOpenGL
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // TODO: Refactor this to common Interface for a common
        private RpgGame _game;
        
        public MainGame()
            : base()
        {
            _graphics = new GraphicsDeviceManager(this);            
        }

      
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {            
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
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _game = new RpgGame(Window, Content);                                                
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

            _game.Update(gameTime);
            
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

            _spriteBatch.Begin();
            _game.Draw(_spriteBatch);
            _spriteBatch.End();

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
