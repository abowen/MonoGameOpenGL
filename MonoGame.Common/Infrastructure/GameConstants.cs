using Microsoft.Xna.Framework;

namespace MonoGame.Common.Infrastructure
{
    public static class GameConstants
    {
        public static Rectangle ScreenBoundary;
        public static bool ShowObjectBoundary = false;
        public static readonly int MaximumEnemies = 3;
        public static GameInstance GameInstance = new GameInstance();

        public static int GameScale = 1;
        public static float CameraScale = 1f;

        public static float Scale
        {
            get
            {
                return GameScale * CameraScale;
            }
        }
    }
}
