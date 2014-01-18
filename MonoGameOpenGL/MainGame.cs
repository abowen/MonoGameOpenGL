#region Using Statements
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Common.Interfaces;
using MonoGame.Game.Rpg;
using MonoGame.Game.Space;
using MonoGame.Game.Surfing;
using MonoGame.Game.Twitch;
using MonoGame.Server;
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
        private BroadcastClient _broadcastClient;
        
        private ISimpleGame _game;

        private ISimpleNetworking NetworkGame
        {
            get
            {
                return _game as ISimpleNetworking;
            }
        }

        private bool IsNetworkGame 
        {
            get
            {
                return NetworkGame != null;
            }
        }

        public MainGame()
            : base()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                // Optimize for TV's and 24" computers.
                PreferredBackBufferWidth = 1920,
                PreferredBackBufferHeight = 1080,
                IsFullScreen = true,
            };
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
            
            _game = new TopDown(Window, Content);            
            //_game = new SurfingGame(Window, Content);
            //_game = new RpgGame(Window, Content);
            //_game = new TwitchGame(Window, Content);
                                                          
            if (IsNetworkGame)
            {
                _broadcastClient = new BroadcastClient();
            }
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

            if (IsNetworkGame)
            {
                while (_broadcastClient.MessagesReceived.Any())
                {
                    NetworkGame.Update(_broadcastClient.MessagesReceived.Dequeue());
                }
            }

            base.Update(gameTime);
        }

        private int _frames;
        private double _timeElapsedMilliseconds;

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // Point clamp removes smoothness :)
            // MSDN: 	Begins a sprite batch operation using deferred sort and default state objects 
            // (BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise). 

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            _spriteBatch.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            _game.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);

            _frames++;
            _timeElapsedMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_timeElapsedMilliseconds > 2000)
            {
                var fps = _frames / (_timeElapsedMilliseconds / 1000);
                System.Diagnostics.Debug.WriteLine("FPS {0:N0}", fps);
                _timeElapsedMilliseconds = 0;
                _frames = 0;
            }
        }
    }
}
