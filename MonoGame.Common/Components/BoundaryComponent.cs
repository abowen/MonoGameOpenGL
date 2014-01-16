using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class BoundaryComponent : SimpleComponent, ISimpleDrawable
    {
        private readonly Texture2D _texture;
        public readonly bool IsCameraScaled;
        public readonly int WidthUnscaled;
        public readonly int HeightUnscaled;

        public int Width
        {
            get
            {
                return (int)(WidthUnscaled * GameConstants.GameScale * GameConstants.CameraScale);
            }
        }

        public int Height
        {
            get
            {
                return (int)(HeightUnscaled * GameConstants.GameScale * GameConstants.CameraScale);
            }
        }

        public BoundaryComponent(Texture2D texture, int? width = null, int? height = null, bool cameraScale = true)
        {
            _texture = texture;
            WidthUnscaled = width ?? _texture.Width;
            HeightUnscaled = height ?? _texture.Height;
            IsCameraScaled = cameraScale;
        }

        public BoundaryComponent(int width, int height)
        {
            WidthUnscaled = width;
            HeightUnscaled = height;
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
