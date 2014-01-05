using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Events;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.Logic
{
    public class CounterComponent : SimpleComponent, ICounterComponent
    {
        private readonly ObjectEvent _decreaseEvent;
        private readonly ObjectEvent _increseEvent;
        private readonly int _minValue;
        private readonly int _maxValue;
        public int CurrentValue { get; private set; }

        public override void SetOwner(GameObject owner)
        {
            Owner = owner;
            owner.ObjectEvent += OwnerOnObjectEvent;
        }

        public CounterComponent(ObjectEvent decreaseEvent, ObjectEvent increseEvent, int minValue, int maxValue)
        {
            _decreaseEvent = decreaseEvent;
            _increseEvent = increseEvent;
            _minValue = minValue;
            _maxValue = maxValue;
            CurrentValue = minValue;
        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs objectEventArgs)
        {
            if (objectEventArgs.Action == _decreaseEvent && CurrentValue != _minValue)
            {
                CurrentValue--;
            }
            else if (objectEventArgs.Action == _increseEvent && CurrentValue != _maxValue)
            {
                CurrentValue++;
            }
        }
    }
}
