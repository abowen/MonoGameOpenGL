namespace MonoGame.Common.Infrastructure
{
    public static class GameHelper
    {
        // TODO: Refactor this
        public static int GetRelativeScaleX(float percentage)
        {
            return (int)(GameConstants.ScreenBoundary.Width / GameConstants.Scale * percentage);
        }

        public static int GetRelativeScaleY(float percentage)
        {
            return (int)(GameConstants.ScreenBoundary.Height / GameConstants.Scale * percentage);
        }

        public static int GetRelativeY(float percentage)
        {
            return (int)(GameConstants.ScreenBoundary.Height / percentage);
        }

        public static int GetRelativeX(float percentage)
        {
            return (int)(GameConstants.ScreenBoundary.Width / percentage);
        }
    }
}