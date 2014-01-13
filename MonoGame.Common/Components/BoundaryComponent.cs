using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class BoundaryComponent : SimpleComponent, ISimpleDrawable
    {
        private readonly Texture2D _texture;
        public readonly int Width;
        public readonly int Height;

        public BoundaryComponent(Texture2D texture, int? width = null, int? height = null)
        {
            _texture = texture;
            Width = width ?? _texture.Width;
            Height = height ?? _texture.Height;
        }

        public BoundaryComponent(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameConstants.ShowObjectBoundary && _texture != null)
            {
                spriteBatch.Draw(_texture, new Vector2(Owner.TopLeft.X, Owner.TopLeft.Y), null, Color.White, 0, Vector2.Zero, new Vector2(Height, Width), SpriteEffects.None, 0);
            }
        }
    }
}
