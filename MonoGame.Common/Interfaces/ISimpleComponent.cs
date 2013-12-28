using MonoGame.Common.Entities;

namespace MonoGame.Common.Interfaces
{
    public interface ISimpleComponent
    {
        GameObject Owner { get; }

        void SetOwner(GameObject owner);
    }
}
