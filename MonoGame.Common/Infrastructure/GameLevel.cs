using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Enums;
using MonoGame.Common.Interfaces;
using MonoGame.Common.Networking;

namespace MonoGame.Common.Infrastructure
{
    public abstract class GameLevel : ISimpleDrawable, ISimpleUpdateable, ISimpleNetworking
    {
        protected GameLevel()
        {
            LoadForeground();
            LoadBackground();
            LoadDisplay();
        }

        protected readonly GameLayer ForegroundLayer = new GameLayer(GameLayerDepth.Foreground);
        protected readonly GameLayer BackgroundLayer = new GameLayer(GameLayerDepth.Background);
        protected readonly GameLayer DisplayLayer = new GameLayer(GameLayerDepth.Display);

        public void Update(GameTime gameTime)
        {
            ForegroundLayer.Update(gameTime);
            BackgroundLayer.Update(gameTime);
            DisplayLayer.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the furthest to start with
            BackgroundLayer.Draw(spriteBatch);
            ForegroundLayer.Draw(spriteBatch);
            DisplayLayer.Draw(spriteBatch);
        }

        public void Update(NetworkMessage networkMessage)
        {
            ForegroundLayer.Update(networkMessage);
            BackgroundLayer.Update(networkMessage);
            DisplayLayer.Update(networkMessage);
        }

        protected abstract void LoadForeground();

        protected abstract void LoadBackground();

        protected abstract void LoadDisplay();
    }
}
