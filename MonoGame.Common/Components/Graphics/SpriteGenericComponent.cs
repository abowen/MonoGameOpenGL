using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SpriteGenericComponent : SimpleComponent, ISimpleDrawable, ISimpleUpdateable
    {
        internal Texture2D[] Textures;
        private List<Vector2> _locations;
        private readonly Vector2 _relativeLocation;
        private int _currentValue;
        private readonly ObjectEvent _subscribeEvent;
        private readonly CounterIncrementComponent _counterIncrementComponent;
        private readonly Func<int, int, IEnumerable<Vector2>> _drawMethod;
        //private readonly int _drawScale = 1;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public override void SetOwner(GameObject owner)
        {
            Owner = owner;
            owner.ObjectEvent += OwnerOnObjectEvent;
        }

        public SpriteGenericComponent(Texture2D[] textures, Vector2 relativeLocation, ObjectEvent subscribeEvent, CounterIncrementComponent counterIncrementComponent, Func<int, int, IEnumerable<Vector2>> drawMethod)
        {
            Textures = textures;

            Width = Textures.Max(t => t.Width);
            Height = Textures.Max(t => t.Height);

            _relativeLocation = relativeLocation;
            _currentValue = counterIncrementComponent.CurrentValue;
            _subscribeEvent = subscribeEvent;
            _counterIncrementComponent = counterIncrementComponent;
            _drawMethod = drawMethod;
            _locations = drawMethod.Invoke(_currentValue, 0).ToList();
        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs objectEventArgs)
        {
            if (objectEventArgs.Action == _subscribeEvent)
            {
                _currentValue = _counterIncrementComponent.CurrentValue;

                var requiredItems = _currentValue;
                var newVectors = _drawMethod(requiredItems, 5).ToList();                
                _locations = newVectors;
            }
        }

        private double _elapsedTimeSpan = 0;
        private double _spriteRefresh = 250;

        public void Draw(SpriteBatch spriteBatch)
        {
            
            //var drawScale = _drawScale * GameConstants.Scale;

            var index = (int) (_elapsedTimeSpan/_spriteRefresh);
            var texture = Textures[index];
            for (var count = 0; count < _currentValue; count++)
            {
                if (_locations.Any())
                {
                    var newLocation = Owner.TopLeft;
                    newLocation += _relativeLocation;
                    newLocation += _locations.ToList()[count];

                    var locationScaled = (newLocation) * GameConstants.Scale;
                    spriteBatch.Draw(texture, locationScaled, Color.White);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            _elapsedTimeSpan += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (((int)(_elapsedTimeSpan / _spriteRefresh)) >= Textures.Count())
            {
                _elapsedTimeSpan = 0;
            }
        }
    }
}
