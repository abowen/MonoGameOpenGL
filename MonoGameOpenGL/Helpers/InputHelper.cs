using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace MonoGameOpenGL.Helpers
{
    // Could be refactored into extension method
    public static class InputHelper
    {
        public static Vector2 KeyboardDirection(Keys[] keys, bool isNormalized = true)
        {
            Contract.Assert(keys != null, "Failed to pass input parameter");

            var direction = Vector2.Zero;
            foreach (var key in keys)
            {
                switch (key)
                {
                    case Keys.Up: direction += new Vector2(0, -1);
                        break;
                    case Keys.Down: direction += new Vector2(0, 1);
                        break;
                    case Keys.Right: direction += new Vector2(1, 0);
                        break;
                    case Keys.Left: direction += new Vector2(-1, 0);
                        break;
                }                    
            }
            if (direction != Vector2.Zero && isNormalized)
            {
                direction.Normalize();
            }
            return direction;
        }
    }
}
