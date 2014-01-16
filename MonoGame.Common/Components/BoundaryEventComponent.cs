using Microsoft.Xna.Framework;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class BoundaryEventComponent : SimpleComponent, ISimpleUpdateable
    {        
        private readonly Rectangle _boundaryRectangle;
        private readonly ObjectEvent _onEnterBoundary;
        private readonly ObjectEvent _onExitBoundary;        

        public BoundaryEventComponent(Rectangle boundaryRectangle, ObjectEvent onEnterBoundary, ObjectEvent onExitBoundary)
        {            
            _boundaryRectangle = boundaryRectangle;
            _onEnterBoundary = onEnterBoundary;
            _onExitBoundary = onExitBoundary;
        }

        private ObjectEvent _currentEvent;

        public override void SetOwner(GameObject owner)
        {
            Owner = owner;
            var ownerPoint = new Point((int)owner.TopLeft.X, (int)owner.TopLeft.Y);
            _currentEvent = _boundaryRectangle.Contains(ownerPoint) ? _onEnterBoundary : _onExitBoundary;
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
