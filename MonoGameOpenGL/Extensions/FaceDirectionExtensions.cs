using Microsoft.Xna.Framework;
using MonoGameOpenGL.Enums;

namespace MonoGameOpenGL.Extensions
{
    public static class FaceDirectionExtensions
    {
        public static Vector2 GetVector2(this FaceDirection faceDirection)
        {
            switch (faceDirection)
            {
                case FaceDirection.Top:
                    return new Vector2(0, -1);
                case FaceDirection.Bottom:
                    return new Vector2(0, 1);
                case FaceDirection.Left:
                    return new Vector2(-1, 0);
                case FaceDirection.Right:
                    return new Vector2(1, 0);
                default:
                    return Vector2.Zero;
            }
        }
    }
}
