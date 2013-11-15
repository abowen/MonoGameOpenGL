using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class TimedActionComponent : ISimpleComponent
    {
        private readonly ObjectEvent _eventTypeRaised;
        private readonly double _intervalMilliseconds;
        public GameObject Owner { get; set; }

        public TimedActionComponent(GameObject owner, ObjectEvent eventTypeRaised, double intervalMilliseconds)
        {
            _eventTypeRaised = eventTypeRaised;
            _intervalMilliseconds = intervalMilliseconds;
            Owner = owner;
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

        public void Draw(SpriteBatch gameTime)
        {
            
        }
    }
}
