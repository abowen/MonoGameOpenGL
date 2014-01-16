using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Enums;
using MonoGame.Common.Interfaces;
using MonoGame.Common.Networking;

namespace MonoGame.Common.Infrastructure
{
    public abstract class GameLevel : ISimpleDrawable, ISimpleUpdateable, ISimpleNetworking
    {
        protected GameLevel(float cameraScale = 1f)
        {
            GameConstants.CameraScale = cameraScale;
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

        public void Enable()
        {
            IsEnabled = true;
        }

        public void Disable()
        {
            IsEnabled = false;
        }

        public bool IsEnabled { get; private set; }

        public event EventHandler<LevelEventArgs> ChangeLevelEvent;

        public void NextLevel(GameLevel gameLevel)
        {
            if (ChangeLevelEvent == null) return;

            var eventArgs = new LevelEventArgs() {GameLevel = gameLevel};
            ChangeLevelEvent.Invoke(this, eventArgs);
        }

        public void BackLevel()
        {
            if (ChangeLevelEvent == null) return;

            var eventArgs = new LevelEventArgs() { GameLevel = null };
            ChangeLevelEvent.Invoke(this, eventArgs);
        }
    }

    public class LevelEventArgs : EventArgs
    {
        public GameLevel GameLevel;
    }
}
