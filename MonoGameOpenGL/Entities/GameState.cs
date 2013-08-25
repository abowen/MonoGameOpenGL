using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameOpenGL.Entities
{
    /// <summary>
    /// GameState represents a single environment
    /// e.g. Shop, world map, fighting arena
    /// </summary>
    public class GameState 
    {                
        public readonly List<Sprite> GameEntities = new List<Sprite>();

        public GameState()
        {
            
        }
        
        public void Update(GameTime gameTime)
        {
            GameEntities.ForEach(s => s.Update(gameTime));            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GameEntities.ForEach(s => s.Draw(spriteBatch));
        }

    }
}
