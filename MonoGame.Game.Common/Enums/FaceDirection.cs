using System;

namespace MonoGame.Game.Common.Enums
{    
    [Flags]
    public enum FaceDirection
    {
        None = 0,
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8,
        UpLeft = 5,
        UpRight = 9,
        DownLeft = 6,
        DownRight = 10
    }
}
