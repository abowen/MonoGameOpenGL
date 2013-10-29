using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Events;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Components
{
    public class SpriteGenericComponent : IMonoGameComponent
    {
        internal Texture2D Texture;
        private readonly Vector2 _relativeLocation;
        private int _currentValue;
        private readonly bool _isVertical;
        private readonly ObjectEvent _subscribeEvent;
        private readonly CounterComponent _counterComponent;
        private readonly Func<Vector2> _drawMethod;
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


        public SpriteGenericComponent(Texture2D texture, Vector2 relativeLocation, GameObject owner, ObjectEvent subscribeEvent, CounterComponent counterComponent, Func<Vector2> drawMethod)
        {
            Texture = texture;
            _relativeLocation = relativeLocation;
            _currentValue = counterComponent.CurrentValue;
            _subscribeEvent = subscribeEvent;
            _counterComponent = counterComponent;
            _drawMethod = drawMethod;
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
                var newLocation = Owner.TopLeft;
                newLocation += _relativeLocation;
                newLocation += _drawMethod.Invoke();
                spriteBatch.Draw(Texture, newLocation, Color.White);
            }
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
