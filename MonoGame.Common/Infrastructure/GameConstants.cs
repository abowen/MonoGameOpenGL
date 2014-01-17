using Microsoft.Xna.Framework;

namespace MonoGame.Common.Infrastructure
{
    public static class GameConstants
    {
        public static Rectangle ScreenBoundary;

        public static Rectangle ScreenBoundaryDividedByScale
        {
            get
            {
                return new Rectangle(0, 0, (int)(ScreenBoundary.X / Scale), (int)(ScreenBoundary.Y / Scale));
            }
        }


        public static bool ShowObjectBoundary = false;
        
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

        // TODO: Refactor into own class / GameInstance?
        public static int CurrentMaximumEnemies { get; set; }
        public static int TotalMaximumEnemies { get; set; }
        public static int EnemyDelay { get; set; }
        public static double RepeaterDelay = 100;
    }
}
