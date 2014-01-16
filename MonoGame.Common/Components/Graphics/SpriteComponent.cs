using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.Graphics
{
    public class SpriteComponent : SimpleComponent, ISimpleDrawable
    {
        internal Texture2D Texture;
        private readonly Vector2 _relativeLocation;
        private readonly Vector2 _drawScale;
        private readonly Vector2 _rotationOrigin;
        private readonly Color _color;

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }

        public SpriteComponent(Texture2D texture, Vector2? relativeLocation = null, Vector2? drawScale = null, Vector2? rotationOrigin = null, Color? color = null)
        {
            if (texture == null)
            {
                throw new ArgumentNullException("texture");
            }

            Texture = texture;
            _relativeLocation = relativeLocation ?? Vector2.Zero;
            _drawScale = drawScale ?? new Vector2(1, 1);
            _rotationOrigin = rotationOrigin ?? Vector2.Zero;
            _color = color ?? Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var locationScaled = (Owner.TopLeft + _relativeLocation) * GameConstants.Scale;
            var drawScale = _drawScale * GameConstants.Scale;
            if (_rotationOrigin != Vector2.Zero)
            {
                spriteBatch.Draw(Texture, locationScaled, null, _color, Owner.Rotation, _rotationOrigin, drawScale, SpriteEffects.None, 1);
            }
            else
            {
                spriteBatch.Draw(Texture, locationScaled, null, _color, Owner.Rotation, Vector2.Zero, drawScale, SpriteEffects.None, 1);
            }
        }
    }
}
