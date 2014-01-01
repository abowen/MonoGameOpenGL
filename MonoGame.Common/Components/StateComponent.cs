using System;
using System.Collections.Generic;
using MonoGame.Common.Entities;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class StateComponent : SimpleComponent
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
            var state = new State(component, enableState, disableState);
            States.Add(state);
        }

        public List<State> States = new List<State>(); 
    }

    public class State
    {
        public readonly SimpleComponent Component;
        public readonly string EnableState;
        public readonly string DisableState;

        public State(SimpleComponent component, string enableState, string disableState)
        {
            Component = component;
            EnableState = enableState;
            DisableState = disableState;
        }
    }
}
