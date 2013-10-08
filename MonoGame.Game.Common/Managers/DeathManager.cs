using System;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.Entities;
using MonoGameOpenGL.Enums;
using MonoGameOpenGL.Infrastructure;

namespace MonoGameOpenGL.Managers
{
    public class DeathManager
    {
        private readonly Texture2D[] _texture2D;
        private readonly Sprite _owner;
        private readonly GameLayer _gameLayer;
        private readonly Random _random;        

        public DeathManager(Texture2D[] textures, Sprite owner, GameLayer gameLayer)
        {
            _texture2D = textures;
            _owner = owner;
            _gameLayer = gameLayer;
            _random = new Random();
        }

        public void Fire()
        {          
            // TODO: Could use calculated method to take into account the owner's direction and use perpendicular
            var directions = new[] { FaceDirection.UpLeft, FaceDirection.UpRight, FaceDirection.DownLeft, FaceDirection.DownRight };
            foreach (var direction in directions)
            {
                var deathTexture = GetRandomTexture();
                var deathSprite = new TimedSprite(deathTexture, _owner.Location, direction, 1500, _gameLayer);
                _gameLayer.GameEntities.Add(deathSprite);
            }
        }

        public Texture2D GetRandomTexture()
        {
            var randomValue = _random.Next(0, _texture2D.Count());
            return _texture2D[randomValue];
        }
    }
}
