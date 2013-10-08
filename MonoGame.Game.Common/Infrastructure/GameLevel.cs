using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameOpenGL.Infrastructure
{
    public class GameLevel
    {
        private readonly GameLayer _game = new GameLayer(GameLayerDepth.Game);
        private readonly GameLayer _background = new GameLayer(GameLayerDepth.Background);

        public void Update(GameTime gameTime)
        {
            _game.Update(gameTime);
            _background.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

    }
}
