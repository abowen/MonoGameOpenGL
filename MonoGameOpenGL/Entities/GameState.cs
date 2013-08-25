using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameOpenGL.Entities
{
    public class GameState
    {
        public GameState(SpriteBatch spriteBatch)
        {            
            _spriteBatch = spriteBatch;
        }

        private readonly SpriteBatch _spriteBatch;
        public readonly List<Sprite> GameEntities = new List<Sprite>();

        public void Update(GameTime gameTime)
        {
            GameEntities.ForEach(s => s.Update(gameTime));
        }

        public void Draw()
        {
            GameEntities.ForEach(s => s.Draw(_spriteBatch));
        }
    }
}
