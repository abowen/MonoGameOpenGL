using System.Diagnostics.Contracts;
using MonoGame.Common.Entities;

namespace MonoGame.Common.Interfaces
{
    public abstract class SimpleComponent
    {
        public GameObject Owner { get; protected set; }

        public virtual void SetOwner(GameObject owner)
        {
            Contract.Assert(owner != null);

            Owner = owner;
        }

        public bool IsEnabled { get; set; }
    }
}
