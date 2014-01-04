using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class SpriteComponent : SimpleComponent, ISimpleDrawable
    {
        internal Texture2D Texture;
        private readonly Vector2 _relativeLocation;
        private readonly Vector2 _scale;
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
                
        public SpriteComponent(Texture2D texture, Vector2? relativeLocation = null, Vector2? scale = null, Vector2? rotationOrigin = null, Color? color = null)
        {
            if (texture == null)
            {
                throw new ArgumentNullException("texture");
            }

            Texture = texture;
            _relativeLocation = relativeLocation ?? Vector2.Zero;
            _scale = scale ?? new Vector2(1,1);
            _rotationOrigin = rotationOrigin ?? Vector2.Zero;
            _color = color ?? Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_rotationOrigin != Vector2.Zero)
            {
                
                spriteBatch.Draw(Texture, Owner.TopLeft + _relativeLocation, null, _color, Owner.Rotation, _rotationOrigin, _scale, SpriteEffects.None, 1);
            }
            else
            {
                spriteBatch.Draw(Texture, Owner.TopLeft + _relativeLocation, null, _color, Owner.Rotation, Vector2.Zero, _scale, SpriteEffects.None, 1);
            }                     
        }
    }
}
