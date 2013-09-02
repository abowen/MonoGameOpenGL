using System;

namespace MonoGameOpenGL.Enums
{    
    [Flags]
    public enum FaceDirection
    {
        None,
        Up,
        Down,
        Left,
        Right,
        UpLeft = Up & Left,
        UpRight = Up & Right,
        DownLeft = Down & Left,
        DownRight = Down & Right
    }
}
