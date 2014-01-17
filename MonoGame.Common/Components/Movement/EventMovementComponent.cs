using Microsoft.Xna.Framework;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Events;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.Movement
{
    public class EventMovementComponent : SimpleComponent
    {        
        private readonly Vector2 _movement;
        private readonly ObjectEvent _objectEvent;

        public EventMovementComponent(Vector2 movement, ObjectEvent objectEvent)
        {
            _movement = movement;
            _objectEvent = objectEvent;
        }

        public override void SetOwner(GameObject owner)
        {
            base.SetOwner(owner);
            Owner.ObjectEvent += OwnerOnObjectEvent;
        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs objectEventArgs)
        {
            if (objectEventArgs.Action == _objectEvent)
            {
                Owner.TopLeft += _movement;    
            }
        }        
    }
}
