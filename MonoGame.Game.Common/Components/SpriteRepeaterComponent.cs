using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Events;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Components
{
    public class SpriteRepeaterComponent : IMonoGameComponent
    {
        internal Texture2D Texture;
        private readonly Vector2 _relativeLocation;
        private int _repeat;
        private readonly bool _isVertical;
        private readonly ObjectEvent _eventType;
        private readonly bool _isDescending;
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

        public SpriteRepeaterComponent(Texture2D texture, int repeat, bool isVertical)
        {
            Texture = texture;
            _repeat = repeat;
            _isVertical = isVertical;
            _relativeLocation = Vector2.Zero;
        }

        // TODO: Use optional parameters, introduce gap
        public SpriteRepeaterComponent(Texture2D texture, Vector2 relativeLocation, int repeat, bool isVertical)
        {
            Texture = texture;
            _relativeLocation = relativeLocation;
            _repeat = repeat;
            _isVertical = isVertical;
        }

        public SpriteRepeaterComponent(Texture2D texture, Vector2 relativeLocation, int repeat, bool isVertical, GameObject owner, ObjectEvent eventType, bool isDescending = true, bool isReverse = false)
        {
            Texture = texture;
            _relativeLocation = relativeLocation;
            _repeat = repeat;
            _isVertical = isVertical;
            _eventType = eventType;
            _isDescending = isDescending;
            _isReverse = isReverse;
            owner.ObjectEvent += OwnerOnObjectEvent;
        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs objectEventArgs)
        {
            if (objectEventArgs.Action == _eventType)
            {
                if (_isDescending)
                {
                    _repeat--;
                }
                else
                {
                    _repeat++;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var count = 0; count < _repeat; count++)
            {
                var expansion = _isVertical ? new Vector2(0, 1) : new Vector2(1, 0);
                var newLocation = Owner.TopLeft;
                newLocation += _relativeLocation;

                if (!_isReverse)
                {                                        
                    newLocation += count * new Vector2(Texture.Width, Texture.Height) * expansion;
                }
                else
                {
                    newLocation -= count * new Vector2(Texture.Width, Texture.Height) * expansion;
                }
                spriteBatch.Draw(Texture, newLocation, Color.White);
            }
        }

        private void DrawItem(SpriteBatch spriteBatch, int count)
        {

        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
