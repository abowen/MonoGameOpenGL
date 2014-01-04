using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Common.Extensions
{
    public static class Texture2DExtensions
    {
        public static Vector2 Centre(this Texture2D entity)
        {
            return new Vector2(entity.Width / 2, entity.Height / 2);
        }
    }
}
