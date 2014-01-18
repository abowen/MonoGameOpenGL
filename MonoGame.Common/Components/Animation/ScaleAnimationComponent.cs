using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Enums;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.Animation
{
    public class ScaleAnimationComponent : SimpleComponent, ISimpleDrawable, ISimpleUpdateable
    {
        internal Texture2D Texture;
        private readonly float _startScale;
        private readonly float _endScale;
        private readonly Vector2 _relativeLocation;
        private readonly Color _color;
        private readonly int _animationMilliseconds;
        private readonly ObjectEvent _animationCompleteEvent;
        private double _currentMilliseconds;

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }

        public ScaleAnimationComponent(Texture2D texture, float startScale, float endScale, int animationMilliseconds = 10000, Color? color = null, Vector2? relativeLocation = null, ObjectEvent animationCompleteEvent = ObjectEvent.Ignore)
        {
            if (texture == null)
            {
                throw new ArgumentNullException("texture");
            }

            Texture = texture;
            _startScale = startScale;
            _endScale = endScale;
            _animationMilliseconds = animationMilliseconds;
            _animationCompleteEvent = animationCompleteEvent;
            _relativeLocation = relativeLocation ?? Vector2.Zero;
            _color = color ?? Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_currentMilliseconds < _animationMilliseconds)
            {
                var timePercent = _currentMilliseconds / _animationMilliseconds;
                var scaleDifference = _endScale - _startScale;
                var scaleAmount = scaleDifference * timePercent;
                var animationScale = scaleAmount + _startScale;

                var locationScaled = (Owner.TopLeft + _relativeLocation) * GameConstants.Scale;
                var drawScale = (float)(animationScale * GameConstants.Scale);
                var centreOrigin = new Vector2(Width / 2, Height / 2);

                spriteBatch.Draw(Texture, locationScaled, null, _color, Owner.Rotation, centreOrigin, drawScale, SpriteEffects.None, 1);
            }
            else
            {
                Disable();
                Owner.Event(_animationCompleteEvent);
            }
        }

        public void Update(GameTime gameTime)
        {
            _currentMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;
        }
    }
}
