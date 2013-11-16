﻿using System;
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
        public GameObject Owner { get; set; }

        public ObjectEventComponent(GameObject owner, ObjectEvent subscribeEvent, Action<GameObject> action)
        {
            _subscribeEvent = subscribeEvent;
            _action = action;
            owner.ObjectEvent += OwnerOnObjectEvent;
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
