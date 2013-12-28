using System;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Events;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class ObjectEventComponent : ISimpleComponent
    {
        private readonly ObjectEvent _subscribeEvent;
        private readonly Action<GameObject> _action;

        public GameObject Owner { get; private set; }

        public void SetOwner(GameObject owner)
        {
            Owner = owner;
            owner.ObjectEvent += OwnerOnObjectEvent;
        }

        public ObjectEventComponent(ObjectEvent subscribeEvent, Action<GameObject> action)
        {
            _subscribeEvent = subscribeEvent;
            _action = action;            
        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs objectEventArgs)
        {
            if (objectEventArgs.Action == _subscribeEvent)
            {
                _action.Invoke(Owner);
            }
        }
    }
}
