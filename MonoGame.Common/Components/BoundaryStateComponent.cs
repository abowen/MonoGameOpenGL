using Microsoft.Xna.Framework;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class BoundaryStateComponent : SimpleComponent, ISimpleUpdateable
    {
        private readonly Rectangle _boundaryRectangle;
        private readonly string _onEnterBoundary;
        private readonly string _onExitBoundary;

        public BoundaryStateComponent(Rectangle boundaryRectangle, string onEnterBoundary, string onExitBoundary)
        {
            _boundaryRectangle = boundaryRectangle;
            _onEnterBoundary = onEnterBoundary;
            _onExitBoundary = onExitBoundary;
        }

        public override void SetOwner(GameObject owner)
        {
            Owner = owner;
            var ownerPoint = new Point((int)owner.TopLeft.X, (int)owner.TopLeft.Y);
            var startState = _boundaryRectangle.Contains(ownerPoint) ? _onEnterBoundary : _onExitBoundary;
            Owner.SetState(startState);
        }

        public void Update(GameTime gameTime)
        {
            var ownerPoint = new Point((int)Owner.TopLeft.X, (int)Owner.TopLeft.Y);
            if (_boundaryRectangle.Contains(ownerPoint) && Owner.State == _onExitBoundary)
            {
                Owner.SetState(_onEnterBoundary);
            }
            else if (!_boundaryRectangle.Contains(ownerPoint) && Owner.State == _onEnterBoundary)
            {
                Owner.SetState(_onExitBoundary);
            }
        }
    }
}
