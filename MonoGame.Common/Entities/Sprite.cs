using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Extensions;
using MonoGame.Game.Common.Helpers;
using MonoGame.Game.Common.Infrastructure;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Entities
{
    abstract public class Sprite
    {
        internal Texture2D _texture;
        private readonly GameLayer _gameLayer;
        private readonly Dictionary<Keys, InputAction> _keyboardMappings;
        private readonly Dictionary<Keys, InputAction> _buttonMappings;        

        public virtual void RemoveEntity()
        {
            IsRemoved = true;
        }

        public bool IsRemoved { get; protected set; }

        /// <summary>
        /// Up-Left co-ordinates
        /// </summary>
        public Vector2 Location { get; protected set; }

        /// <summary>
        /// Velocity = Speed * Direction
        /// </summary>
        public int Speed { get; protected set; }

        /// <summary>
        /// Movement direction of sprite
        /// </summary>
        public Vector2 MovementDirection { get; protected set; }

        /// <summary>
        /// Face direction of sprite
        /// </summary>
        /// <remarks>
        /// It's important to keep FaceDirection separate 
        /// from Direction to allow for strafing
        /// </remarks>
        public FaceDirection FaceDirection { get; protected set; }

        public int Width
        {
            get { return _texture.Width; }
        }

        public int Height
        {
            get { return _texture.Height; }
        }

        public Vector2 Centre
        {
            get
            {
                return new Vector2(BoundingBox.Center.X, BoundingBox.Center.Y);
            }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Location.X, (int)Location.Y, Width, Height);
            }
        }

        protected Sprite(Texture2D texture, Vector2 location, FaceDirection faceDirection, GameLayer gameLayer, Dictionary<Keys, InputAction> keyboardMappings = null, Dictionary<Keys, InputAction> buttonMappings = null)
        {
            FaceDirection = faceDirection;
            MovementDirection = FaceDirection.GetVector2();
            Speed = 1;
            _texture = texture;
            _gameLayer = gameLayer;
            _keyboardMappings = keyboardMappings;
            _buttonMappings = buttonMappings;
            Location = location;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Location, Color.White);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (_keyboardMappings != null || _buttonMappings != null)
            {
                var keysPressed = Keyboard.GetState().GetPressedKeys();
                if (_keyboardMappings != null)
                {
                    MovementDirection = InputHelper.DirectionFromMapping(keysPressed, _keyboardMappings);
                }
                else if (_buttonMappings != null)
                {
                    MovementDirection = InputHelper.DirectionFromMapping(keysPressed, _buttonMappings);
                }
            }

            if (_gameLayer.GameLayerDepth != GameLayerDepth.Display)
            {
                Location += MovementDirection * Speed / (int)_gameLayer.GameLayerDepth;
                if (float.IsNaN(Location.X) || float.IsNaN(Location.Y))
                {
                    // WTF!
                    throw new Exception("Invalid Sprite Location");
                }
            }
            CheckBounds();
        }

        protected abstract void CheckBounds();
    }
}
