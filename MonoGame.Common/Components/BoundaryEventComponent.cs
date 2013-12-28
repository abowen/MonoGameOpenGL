using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class BoundaryEventComponent : ISimpleComponent, ISimpleDrawable, ISimpleUpdateable
    {
        private readonly Texture2D _texture;
        private readonly Rectangle _boundaryRectangle;
        private readonly ObjectEvent _onEnterBoundary;
        private readonly ObjectEvent _onExitBoundary;        

        public BoundaryEventComponent(Texture2D texture, Rectangle boundaryRectangle, ObjectEvent onEnterBoundary, ObjectEvent onExitBoundary)
        {
            _texture = texture;
            _boundaryRectangle = boundaryRectangle;
            _onEnterBoundary = onEnterBoundary;
            _onExitBoundary = onExitBoundary;
        }

        private ObjectEvent _currentEvent;

        public GameObject Owner { get; private set; }

        public void SetOwner(GameObject owner)
        {
            Owner = owner;
            var ownerPoint = new Point((int)owner.TopLeft.X, (int)owner.TopLeft.Y);
            _currentEvent = _boundaryRectangle.Contains(ownerPoint) ? _onEnterBoundary : _onExitBoundary;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameConstants.ShowObjectBoundary)
            {
            //    spriteBatch.Draw(_texture, new Vector2(Owner.TopLeft.X, Owner.TopLeft.Y), null, Color.White, 0, Vector2.Zero, new Vector2(Height, Width), SpriteEffects.None, 0 );
            }
        }

        public void Update(GameTime gameTime)
        {
            var ownerPoint = new Point((int)Owner.TopLeft.X, (int)Owner.TopLeft.Y);
            if (_boundaryRectangle.Contains(ownerPoint) && _currentEvent == _onExitBoundary)
            {
                _currentEvent = _onEnterBoundary;
                Owner.Event(_onEnterBoundary);
            }
            else if (!_boundaryRectangle.Contains(ownerPoint) && _currentEvent == _onEnterBoundary)
            {
                _currentEvent = _onExitBoundary;
                Owner.Event(_onExitBoundary);
            }
        }
    }
}
