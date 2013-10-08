using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Game.Common.Infrastructure
{
    public abstract class GameLevel
    {
        protected GameLevel()
        {
            LoadForeground();
            LoadBackground();
        }

        protected readonly GameLayer ForegroundLayer = new GameLayer(GameLayerDepth.Foreground);
        protected readonly GameLayer BackgroundLayer = new GameLayer(GameLayerDepth.Background);

        public void Update(GameTime gameTime)
        {
            ForegroundLayer.Update(gameTime);
            BackgroundLayer.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ForegroundLayer.Draw(spriteBatch);
            BackgroundLayer.Draw(spriteBatch);
        }

        protected abstract void LoadForeground();


        protected abstract void LoadBackground();
    }
}
