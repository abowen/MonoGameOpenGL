﻿namespace MonoGame.Common.Infrastructure
{
    public static class GameHelper
    {
        public static int GetRelativeX(float percentage)
        {
            return (int)(GameConstants.ScreenBoundary.Width / GameConstants.Scale * percentage);
        }

        public static int GetRelativeY(float percentage)
        {
            return (int)(GameConstants.ScreenBoundary.Height / GameConstants.Scale * percentage);
        }
    }
}