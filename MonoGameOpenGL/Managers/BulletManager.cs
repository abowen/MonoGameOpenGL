using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameOpenGL.Entities;

namespace MonoGameOpenGL.Managers
{
    public class BulletManager
    {        
        private readonly Texture2D _texture2D;
        private readonly GameState _gameState;

        public BulletManager(Texture2D texture, GameState gameState)
        {
            _texture2D = texture;
            _gameState = gameState;
        }
        
        public void Fire(Sprite owner)
        {
            var bullet = new Bullet(_texture2D, new Vector2(owner.Centre.X, owner.Centre.Y), owner.FaceDirection);
            _gameState.GameEntities.Add(bullet);
        }
    }
}
