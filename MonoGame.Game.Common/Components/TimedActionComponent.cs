using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Components
{
    public class TimedActionComponent : IComponent
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
