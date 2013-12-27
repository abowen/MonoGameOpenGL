using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class SpriteComponent : ISimpleComponent, ISimpleDrawable
    {
        internal Texture2D Texture;
        private readonly IRotationComponent _rotationComponent;
        private readonly Vector2 _relativeLocation;

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }

        public GameObject Owner { get; set; }

        public SpriteComponent(Texture2D texture, IRotationComponent rotationComponent = null)
        {
            if (texture == null)
            {
                Contract.Assert(texture != null, "Texture cannot be null");
            }
            Texture = texture;
            _rotationComponent = rotationComponent;
            _relativeLocation = Vector2.Zero;
        }

        public SpriteComponent(Texture2D texture, Vector2 relativeLocation)
        {
            Texture = texture;
            _relativeLocation = relativeLocation;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_rotationComponent != null)
            {
                var rotationOrigin = new Vector2(Texture.Width/2, Texture.Height/2);
                spriteBatch.Draw(Texture, Owner.TopLeft + _relativeLocation, null, Color.White,
                    _rotationComponent.Rotation, rotationOrigin, new Vector2(1, 1), SpriteEffects.None, 1);
            }
            else
            {
                spriteBatch.Draw(Texture, Owner.TopLeft + _relativeLocation, Color.White);
            }
        }
    }
}
