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
        private readonly Vector2 _scale = Vector2.Zero;

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
        }

        public SpriteComponent(Texture2D texture, Vector2 relativeLocation)
        {
            Texture = texture;
            _relativeLocation = relativeLocation;
        }

        public SpriteComponent(Texture2D texture, Vector2 relativeLocation, Vector2 scale, Color? color = null)
        {
            Texture = texture;
            _relativeLocation = relativeLocation;
            _scale = scale;

            _color = color ?? Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_scale != Vector2.Zero)
            {
                var origin = new Vector2(0, 0);
                spriteBatch.Draw(Texture, Owner.TopLeft + _relativeLocation, null, _color, 0, origin, _scale, SpriteEffects.None, 1);
            }
            else
            {
                var rotationOrigin = new Vector2(Texture.Width / 2, Texture.Height / 2);
                spriteBatch.Draw(Texture, Owner.TopLeft + _relativeLocation, null, _color,
                    Owner.Rotation, rotationOrigin, new Vector2(1, 1), SpriteEffects.None, 1);
            }            
        }
    }
}
