﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Game.Common.Infrastructure
{
    // Could be renamed to GameScreen
    public abstract class GameLevel
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
            ForegroundLayer.Draw(spriteBatch);
            BackgroundLayer.Draw(spriteBatch);
            DisplayLayer.Draw(spriteBatch);
        }

        protected abstract void LoadForeground();

        protected abstract void LoadBackground();

        protected abstract void LoadDisplay();
    }
}
