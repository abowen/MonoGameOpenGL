using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Components.Logic;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Events;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.Graphics
{
    public class SpriteRepeaterComponent : SimpleComponent, ISimpleDrawable
    {
        internal Texture2D Texture;
        private readonly Vector2 _relativeLocation;
        private int _currentValue;
        private readonly bool _isVertical;
        private readonly ObjectEvent _subscribeEvent;
        private readonly CounterIncrementComponent _counterIncrementComponent;        
        private readonly bool _isReverse;

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }

        public override void SetOwner(GameObject owner)
        {
            Owner = owner;
            owner.ObjectEvent += OwnerOnObjectEvent;
        }

        public SpriteRepeaterComponent(Texture2D texture, int currentValue, bool isVertical)
        {
            Texture = texture;
            _currentValue = currentValue;
            _isVertical = isVertical;
            _relativeLocation = Vector2.Zero;
        }

        private Color _color = Color.White;

        // TODO: Use optional parameters, introduce gap
        public SpriteRepeaterComponent(Texture2D texture, Vector2 relativeLocation, int currentValue, bool isVertical)
        {
            Texture = texture;
            _relativeLocation = relativeLocation;
            _currentValue = currentValue;
            _isVertical = isVertical;
        }

        public SpriteRepeaterComponent(Texture2D texture, Vector2 relativeLocation, bool isVertical, ObjectEvent subscribeEvent, CounterIncrementComponent counterIncrementComponent, bool isReverse = false, Color? color = null)
        {
            Texture = texture;
            _relativeLocation = relativeLocation;
            _currentValue = counterIncrementComponent.CurrentValue;
            _isVertical = isVertical;
            _subscribeEvent = subscribeEvent;
            _counterIncrementComponent = counterIncrementComponent;            
            _isReverse = isReverse;
            _color = color ?? Color.White;
        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs objectEventArgs)
        {
            if (objectEventArgs.Action == _subscribeEvent)
            {
                _currentValue = _counterIncrementComponent.CurrentValue;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var count = 0; count < _currentValue; count++)
            {
                var expansion = _isVertical ? new Vector2(0, 1) : new Vector2(1, 0);
                var newLocation = Owner.TopLeft;
                newLocation += _relativeLocation;

                var locationScaled = newLocation*GameConstants.Scale;

                if (!_isReverse)
                {
                    locationScaled += count * new Vector2(Texture.Width, Texture.Height) * expansion;
                }
                else
                {
                    locationScaled -= count * new Vector2(Texture.Width, Texture.Height) * expansion;
                }
                spriteBatch.Draw(Texture, locationScaled, _color);
            }
        }
   }
}
