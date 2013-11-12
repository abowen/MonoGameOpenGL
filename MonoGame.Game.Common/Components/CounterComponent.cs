using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Events;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Components
{
    public class CounterComponent : IComponent
    {
        private readonly ObjectEvent _subscribeEvent;
        private readonly ObjectEvent _publishEvent;
        private readonly ObjectEvent _lastEvent;
        private readonly ObjectEvent _resetEvent;
        private readonly int _startValue;
        private readonly int _lastValue;
        public int CurrentValue { get; private set; }
        private readonly bool _isDescending;

        public GameObject Owner { get; set; }


        // TODO: Determine descending by if start > 0
        public CounterComponent(GameObject owner, ObjectEvent subscribeEvent, ObjectEvent publishEvent, ObjectEvent lastEvent, ObjectEvent resetEvent, int startValue, int lastValue, bool isDescending = true)
        {
            Owner = owner;
            _subscribeEvent = subscribeEvent;
            _publishEvent = publishEvent;
            _lastEvent = lastEvent;
            _resetEvent = resetEvent;
            _startValue = startValue;
            _lastValue = lastValue;
            CurrentValue = startValue;
            _isDescending = isDescending;
            owner.ObjectEvent += OwnerOnObjectEvent;
        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs objectEventArgs)
        {
            if (objectEventArgs.Action == _subscribeEvent)
            {
                if (CurrentValue != _lastValue)
                {
                    if (_isDescending)
                    {
                        CurrentValue--;                        
                    }
                    else
                    {
                        CurrentValue++;
                    }
                     Owner.Event(_publishEvent);
                }
                else
                {
                    Owner.Event(_lastEvent);
                }
            }
            if (objectEventArgs.Action == _resetEvent)
            {
                CurrentValue = _startValue;
                Owner.Event(_publishEvent);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
