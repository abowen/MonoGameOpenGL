using MonoGame.Common.Entities;

namespace MonoGame.Common.Interfaces
{
    public interface ISimpleComponent
    {
        GameObject Owner { get; set; }        
    }
}
