using Microsoft.Xna.Framework;
using MonoGameOpenGL.Enums;

namespace MonoGameOpenGL.Extensions
{
    public static class FaceDirectionExtensions
    {
        public static Vector2 GetVector2(this FaceDirection faceDirection)
        {
            var direction = Vector2.Zero;

            if (faceDirection.HasFlag(FaceDirection.Up))
            {
                direction += new Vector2(0, -1);
            }
            if (faceDirection.HasFlag(FaceDirection.Down))
            {
                direction += new Vector2(0, 1);
            }
            if (faceDirection.HasFlag(FaceDirection.Left))
            {
                direction += new Vector2(-1, 0);
            }
            if (faceDirection.HasFlag(FaceDirection.Right))
            {
                direction += new Vector2(1, 0);
            }
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
            return direction;
        }
    }
}
