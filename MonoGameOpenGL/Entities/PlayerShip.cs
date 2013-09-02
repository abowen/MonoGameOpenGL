﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameOpenGL.Enums;
using MonoGameOpenGL.Managers;

namespace MonoGameOpenGL.Entities
{
    public class PlayerShip : Sprite
    {

        public PlayerShip(Texture2D texture, Vector2 location, Texture2D bulletTexture, Texture2D healthTexture, int lives, GameLayer gameLayer, Dictionary<Keys, FaceDirection> keyboardMappings = null, Dictionary<Keys, FaceDirection> buttonMappings = null)
            : base(texture, location, gameLayer, keyboardMappings, buttonMappings)
        {
            Speed = 2;
            FaceDirection = FaceDirection.Up;

            _bulletManager = new BulletManager(bulletTexture, gameLayer);
            HealthManager = new HealthManager(healthTexture, new Vector2(20, 20), lives, gameLayer);
        }

        private BulletManager _bulletManager;
        public HealthManager HealthManager { get; private set; }
        private double _elapsedTimeMilliseconds;

        public override void Update(GameTime gameTime)
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();
            _elapsedTimeMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;
            
            if (keysPressed.Any(k => k == Keys.Space) && _elapsedTimeMilliseconds > 500)
            {
                _elapsedTimeMilliseconds = 0;
                _bulletManager.Fire(this);
            }

            base.Update(gameTime);
        }

        protected override void CheckBounds()
        {
            var topLeft = new Vector2(0, 0);
            var bottomRight = new Vector2(GameConstants.ScreenBoundary.Width - _texture.Width, GameConstants.ScreenBoundary.Height - _texture.Height);
            Location = Vector2.Clamp(Location, topLeft, bottomRight);
        }
    }
}
