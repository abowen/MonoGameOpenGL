﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Infrastructure;

namespace MonoGame.Game.Common.Managers
{
    public class BulletManager
    {        
        private readonly Texture2D _texture2D;
        private readonly GameLayer _gameLayer;

        public BulletManager(Texture2D texture, GameLayer gameLayer)
        {
            _texture2D = texture;
            _gameLayer = gameLayer;
        }
        
        public void Fire(Sprite owner)
        {
            var bullet = new Bullet(_texture2D, new Vector2(owner.Centre.X, owner.BoundingBox.Bottom), owner.FaceDirection, _gameLayer);
            _gameLayer.GameEntities.Add(bullet);
        }
    }
}