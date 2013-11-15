using System;
using Microsoft.Xna.Framework;
using MonoGame.Game.Common.Events;

namespace MonoGame.Game.Common.Infrastructure
{
    public static class GameConstants
    {
        public static Rectangle ScreenBoundary;
        public static bool ShowObjectBoundary = false;
        public static readonly int MaximumEnemies = 1;
        public static GameInstance GameInstance = new GameInstance();

    }

    public class GameInstance
    {
        private static int _score;

        public void UpdateScore()
        {
            _score++;
            if (ScoreEventHandler != null)
            {
                ScoreEventHandler.Invoke(this, new ScoreEventArgs() { Score = _score });
            }
        }

        public EventHandler<ScoreEventArgs> ScoreEventHandler;
    }
}
