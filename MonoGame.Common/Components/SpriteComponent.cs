using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class SpriteComponent : SimpleComponent, ISimpleDrawable
    {
        internal Texture2D Texture;
        private readonly Vector2 _relativeLocation;
        private readonly bool _test;
        private readonly Vector2 _scale = Vector2.Zero;
        private readonly Vector2 _rotationOrigin;

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }

        private readonly Color _color = Color.White;

        public SpriteComponent(Texture2D texture, Color? color = null)
        {
            _color = color ?? Color.White;
            if (texture == null)
            {
                Contract.Assert(texture != null, "Texture cannot be null");
            }
            Texture = texture;
            _relativeLocation = Vector2.Zero;
            _rotationOrigin = Vector2.Zero;
            _scale = new Vector2(1, 1);
        }

        public SpriteComponent(Texture2D texture, Vector2 relativeLocation)
        {
            Texture = texture;
            _relativeLocation = relativeLocation;
            _rotationOrigin = Vector2.Zero;
            _scale = new Vector2(1,1);
        }

        public SpriteComponent(Texture2D texture, Vector2 relativeLocation, Vector2 scale, Vector2 rotationOrigin, Color? color = null)
        {
            Texture = texture;
            _relativeLocation = relativeLocation;
            _scale = scale;
            _rotationOrigin = rotationOrigin;

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
