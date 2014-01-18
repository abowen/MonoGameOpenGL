using Microsoft.Xna.Framework;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.States
{
    public class MovementStateComponent : SimpleComponent, ISimpleUpdateable
    {
        private readonly IMovementComponent _movementComponent;
        private readonly string _trueState;
        private readonly string _falseState;
        private readonly string _defaultState;

        public MovementStateComponent(IMovementComponent movementComponent, string trueState, string falseState, string defaultState = null)
        {
            _movementComponent = movementComponent;
            _trueState = trueState;
            _falseState = falseState;
            _defaultState = defaultState;
        }

        //public override void SetOwner(GameObject owner)
        //{
        //    base.SetOwner(owner);
        //    //Owner.State = _defaultState ?? _trueState;
        //}

        public void Update(GameTime gameTime)
        {

            if ((_movementComponent.InputDirection != Vector2.Zero) && Owner.State == _falseState)
            {
                Owner.SetState(_trueState);
            }
            else if ((_movementComponent.InputDirection == Vector2.Zero) && Owner.State == _trueState)
            {
                Owner.SetState(_falseState);
            }
        }
    }
}
