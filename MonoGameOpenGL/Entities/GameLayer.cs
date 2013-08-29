using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameOpenGL.Entities
{
    /// <summary>
    /// GameLayer represents a single environment
    /// e.g. Shop, world map, fighting arena
    /// </summary>
    public class GameLayer 
    {
        public readonly GameLayerDepth GameLayerDepth;
        public readonly List<Sprite> GameEntities = new List<Sprite>();


        public GameLayer(GameLayerDepth gameLayerDepth)
        {
            GameLayerDepth = gameLayerDepth;
        }

        public void Update(GameTime gameTime)
        {
            GameEntities.ForEach(s => s.Update(gameTime));            
            GameEntities.RemoveAll(s => s.IsRemoved);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GameEntities.ForEach(s => s.Draw(spriteBatch));
        }
    }

    public enum GameLayerDepth
    {
        Display = 0,
        Game = 1,
        Background = 10,
    }
}
