using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Events;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Components
{
    public class SpriteRepeaterComponent : IMonoGameComponent
    {
        internal Texture2D Texture;
        private readonly Vector2 _relativeLocation;
        private int _currentValue;
        private readonly bool _isVertical;
        private readonly ObjectEvent _subscribeEvent;
        private readonly CounterComponent _counterComponent;        
        private readonly bool _isReverse;

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }

        public GameObject Owner { get; set; }

        public SpriteRepeaterComponent(Texture2D texture, int currentValue, bool isVertical)
        {
            Texture = texture;
            _currentValue = currentValue;
            _isVertical = isVertical;
            _relativeLocation = Vector2.Zero;
        }

        // TODO: Use optional parameters, introduce gap
        public SpriteRepeaterComponent(Texture2D texture, Vector2 relativeLocation, int currentValue, bool isVertical)
        {
            Texture = texture;
            _relativeLocation = relativeLocation;
            _currentValue = currentValue;
            _isVertical = isVertical;
        }

        public SpriteRepeaterComponent(Texture2D texture, Vector2 relativeLocation, bool isVertical, GameObject owner, ObjectEvent subscribeEvent, CounterComponent counterComponent, bool isReverse = false)
        {
            Texture = texture;
            _relativeLocation = relativeLocation;
            _currentValue = counterComponent.CurrentValue;
            _isVertical = isVertical;
            _subscribeEvent = subscribeEvent;
            _counterComponent = counterComponent;            
            _isReverse = isReverse;
            owner.ObjectEvent += OwnerOnObjectEvent;
        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs objectEventArgs)
        {
            if (objectEventArgs.Action == _subscribeEvent)
            {
                _currentValue = _counterComponent.CurrentValue;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var count = 0; count < _currentValue; count++)
            {
                var expansion = _isVertical ? new Vector2(0, 1) : new Vector2(1, 0);
                var newLocation = Owner.TopLeft;
                newLocation += _relativeLocation;

                if (!_isReverse)
                {                                        
                    newLocation += count * new Vector2(Texture.Width, Texture.Height) * expansion;
                }
                else
                {
                    newLocation -= count * new Vector2(Texture.Width, Texture.Height) * expansion;
                }
                spriteBatch.Draw(Texture, newLocation, Color.White);
            }
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
