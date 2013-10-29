using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Events;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Components
{
    public class CounterComponent : IMonoGameComponent
    {
        private readonly ObjectEvent _actionEvent;
        private readonly ObjectEvent _updateEvent;
        private readonly ObjectEvent _lastEvent;
        private readonly ObjectEvent _resetEvent;
        private readonly int _startValue;
        private readonly int _lastValue;
        public int CurrentValue { get; private set; }
        private readonly bool _isDescending;

        public GameObject Owner { get; set; }


        public CounterComponent(GameObject owner, ObjectEvent actionEvent, ObjectEvent updateEvent, ObjectEvent lastEvent, ObjectEvent resetEvent, int startValue, int lastValue, bool isDescending = true)
        {
            Owner = owner;
            _actionEvent = actionEvent;
            _updateEvent = updateEvent;
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
            if (objectEventArgs.Action == _actionEvent)
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
                    Owner.Event(_updateEvent);
                }
                else
                {
                    Owner.Event(_lastEvent);
                }
            }
            if (objectEventArgs.Action == _resetEvent)
            {
                CurrentValue = _startValue;
                Owner.Event(_updateEvent);
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
