using System.Diagnostics.Contracts;
using MonoGame.Common.Entities;

namespace MonoGame.Common.Interfaces
{
    public abstract class SimpleComponent
    {
        protected SimpleComponent()
        {
            Enable();
        }

        public GameObject Owner { get; protected set; }

        public virtual void SetOwner(GameObject owner)
        {
            Contract.Assert(owner != null);
//            Contract.Assert(owner.GameLayer != null);
            Owner = owner;
          
        }

        public void Enable()
        {
            IsEnabled = true;
        }

        public void Disable()
        {
            IsEnabled = false;
        }

        public bool IsEnabled { get; private set; }
    }
}
