using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Events;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Components
{
    public class SpriteGenericComponent : IComponent
    {
        internal Texture2D[] Textures;
        private List<Vector2> _locations;
        private readonly Vector2 _relativeLocation;
        private int _currentValue;
        private readonly ObjectEvent _subscribeEvent;
        private readonly CounterComponent _counterComponent;
        private readonly Func<int, int, IEnumerable<Vector2>> _drawMethod;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public GameObject Owner { get; set; }


        public SpriteGenericComponent(Texture2D[] textures, Vector2 relativeLocation, GameObject owner, ObjectEvent subscribeEvent, CounterComponent counterComponent, Func<int, int, IEnumerable<Vector2>> drawMethod)
        {
            Textures = textures;

            Width = Textures.Max(t => t.Width);
            Height = Textures.Max(t => t.Height);

            _relativeLocation = relativeLocation;
            _currentValue = counterComponent.CurrentValue;
            _subscribeEvent = subscribeEvent;
            _counterComponent = counterComponent;
            _drawMethod = drawMethod;
            owner.ObjectEvent += OwnerOnObjectEvent;

            _locations = drawMethod.Invoke(_currentValue, 0).ToList();
        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs objectEventArgs)
        {
            if (objectEventArgs.Action == _subscribeEvent)
            {
                _currentValue = _counterComponent.CurrentValue;

                var requiredItems = _currentValue;// - _locations.Count();
                var newVectors = _drawMethod(requiredItems, 5).ToList();
                //_locations.AddRange(newVectors);
                _locations = newVectors;
            }
        }

        private double _elapsedTimeSpan = 0;
        private double _spriteRefresh = 250;

        public void Draw(SpriteBatch spriteBatch)
        {
            var index = (int) (_elapsedTimeSpan/_spriteRefresh);
            var texture = Textures[index];
            for (var count = 0; count < _currentValue; count++)
            {
                if (_locations.Any())
                {
                    var newLocation = Owner.TopLeft;
                    newLocation += _relativeLocation;
                    newLocation += _locations.ToList()[count];
                    spriteBatch.Draw(texture, newLocation, Color.White);
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
