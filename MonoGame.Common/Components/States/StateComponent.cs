using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.States
{
    public class StateComponent : SimpleComponent, ISimpleUpdateable
    {        
        public void AddComponentState(SimpleComponent component, IStateComponent stateComponent)
        {            
            States.Add(component, stateComponent);
        }

        public Dictionary<SimpleComponent, IStateComponent> States = new Dictionary<SimpleComponent, IStateComponent>();

        public void Update(GameTime gameTime)
        {
            foreach (var state in States)
            {
                if (state.Value.State && !state.Key.IsEnabled)
                {
                    state.Key.Enable();
                }
                else if (!state.Value.State && state.Key.IsEnabled)
                {
                    state.Key.Disable();
                }
            }
        }        
    }
}
