using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.Boundary
{
    public class BoundaryComponent : SimpleComponent, ISimpleDrawable
    {
        private readonly Texture2D _texture;
        public readonly bool IsCameraScaled;
        public readonly string[] IgnoreTypes;
        public readonly int Width;
        public readonly int Height;

        public int WidthScaled
        {
            get
            {
                return (int)(Width * GameConstants.GameScale * GameConstants.CameraScale);
            }
        }

        public int HeightScaled
        {
            get
            {
                return (int)(Height * GameConstants.GameScale * GameConstants.CameraScale);
            }
        }

        public BoundaryComponent(Texture2D texture, int? width = null, int? height = null, bool cameraScale = true, bool isInvulnerable = false, params string[] ignoreTypes)
        {
            _texture = texture;
            Width = width ?? _texture.Width;
            Height = height ?? _texture.Height;
            IsCameraScaled = cameraScale;
            IsInvulnerable = isInvulnerable;
            IgnoreTypes = ignoreTypes ?? new string[0];
        }

        public bool IsInvulnerable { get; private set; }

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
