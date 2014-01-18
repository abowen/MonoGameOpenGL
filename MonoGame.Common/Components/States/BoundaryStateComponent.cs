using Microsoft.Xna.Framework;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.States
{
    public class BoundaryStateComponent : SimpleComponent, ISimpleUpdateable
    {
        private readonly Rectangle _boundaryRectangle;
        private readonly string _trueState;
        private readonly string _falseState;

        public BoundaryStateComponent(Rectangle boundaryRectangle, string trueState, string falseState)
        {
            _boundaryRectangle = boundaryRectangle;
            _trueState = trueState;
            _falseState = falseState;
        }

        public override void SetOwner(GameObject owner)
        {
            Owner = owner;
            var ownerPoint = new Point((int)owner.TopLeft.X, (int)owner.TopLeft.Y);
            var startState = _boundaryRectangle.Contains(ownerPoint) ? _trueState : _falseState;
            Owner.SetState(startState);
        }

        public void Update(GameTime gameTime)
        {
            var ownerPoint = new Point((int)Owner.TopLeft.X, (int)Owner.TopLeft.Y);
            if (_boundaryRectangle.Contains(ownerPoint) && Owner.State == _falseState)
            {
                Owner.SetState(_trueState);
            }
            else if (!_boundaryRectangle.Contains(ownerPoint) && Owner.State == _trueState)
            {
                Owner.SetState(_falseState);
            }
        }
    }
}
