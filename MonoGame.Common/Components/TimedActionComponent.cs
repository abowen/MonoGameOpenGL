using Microsoft.Xna.Framework;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class TimedActionComponent : SimpleComponent, ISimpleUpdateable
    {
        private readonly ObjectEvent _eventTypeRaised;
        private readonly double _intervalMilliseconds;

        public TimedActionComponent(ObjectEvent eventTypeRaised, double intervalMilliseconds)
        {
            _eventTypeRaised = eventTypeRaised;
            _intervalMilliseconds = intervalMilliseconds;            
        }

        private double _timeElapsed;

        public void Update(GameTime gameTime)
        {
            _timeElapsed += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_timeElapsed > _intervalMilliseconds)
            {
                _timeElapsed = 0;
                Owner.Event(_eventTypeRaised);
            }
        }
    }
}
