using System;
using MonoGame.Common.Events;

namespace MonoGame.Common.Infrastructure
{
    public class GameInstance
    {
        private static int _score;

        // TOOD: Refactor this into TopDown specific
        public void UpdateScore()
        {
            _score++;
            if (ScoreEventHandler != null)
            {
                ScoreEventHandler.Invoke(this, new ScoreEventArgs { Score = _score });
            }
        }

        public EventHandler<ScoreEventArgs> ScoreEventHandler;
    }
}