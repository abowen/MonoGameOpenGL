using Microsoft.Xna.Framework;
using MonoGameOpenGL.Enums;

namespace MonoGameOpenGL.Extensions
{
    public static class FaceDirectionExtensions
    {
        public static Vector2 GetVector2(this FaceDirection faceDirection)
        {
            var direction = Vector2.Zero;
            switch (faceDirection)
            {
                case FaceDirection.Up:
                    direction = new Vector2(0, -1);
                    break;
                case FaceDirection.Down:
                    direction = new Vector2(0, 1);
                    break;
                case FaceDirection.Left:
                    direction = new Vector2(-1, 0);
                    break;
                case FaceDirection.Right:
                    direction = new Vector2(1, 0);
                    break;                
            }
            return direction;
        }
    }
}
