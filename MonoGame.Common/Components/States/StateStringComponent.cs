using System;
using System.Collections.Generic;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.States
{
    public class StateStringComponent : SimpleComponent
    {
        public override void SetOwner(GameObject owner)
        {
            base.SetOwner(owner);
            owner.ObjectState += OwnerOnObjectState;
        }

        private void OwnerOnObjectState(object sender, EventArgs eventArgs)
        {
            foreach (var state in States)
            {
                if (String.Equals(state.EnableState, Owner.State, StringComparison.CurrentCultureIgnoreCase))
                {
                    state.Component.Enable();
                }
                if (String.Equals(state.DisableState, Owner.State, StringComparison.CurrentCultureIgnoreCase))
                {
                    state.Component.Disable();
                }
            }
        }

        public void AddComponentState(SimpleComponent component, string enableState, string disableState)
        {
            var state = new StateString(component, enableState, disableState);
            States.Add(state);
        }

        public List<StateString> States = new List<StateString>(); 
    }

    public class StateString
    {
        public readonly SimpleComponent Component;
        public readonly string EnableState;
        public readonly string DisableState;

        public StateString(SimpleComponent component, string enableState, string disableState)
        {
            Component = component;
            EnableState = enableState;
            DisableState = disableState;
        }
    }
}
