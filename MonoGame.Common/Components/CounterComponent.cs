using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Events;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class CounterComponent : SimpleComponent
    {
        private readonly ObjectEvent _subscribeEvent;
        private readonly ObjectEvent _publishEvent;
        private readonly ObjectEvent _lastEvent;
        private readonly ObjectEvent _resetEvent;
        private readonly int _startValue;
        private readonly int _lastValue;
        public int CurrentValue { get; private set; }
        private readonly bool _isDescending;

     
        public override void SetOwner(GameObject owner)
        {
            Owner = owner;
            owner.ObjectEvent += OwnerOnObjectEvent;
        }


        // TODO: Determine descending by if start > 0
        public CounterComponent(ObjectEvent subscribeEvent, ObjectEvent publishEvent, ObjectEvent lastEvent, ObjectEvent resetEvent, int startValue, int lastValue, bool isDescending = true)
        {
            _subscribeEvent = subscribeEvent;
            _publishEvent = publishEvent;
            _lastEvent = lastEvent;
            _resetEvent = resetEvent;
            _startValue = startValue;
            _lastValue = lastValue;
            CurrentValue = startValue;
            _isDescending = isDescending;
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
    }
}
