using MonoGame.Common.Networking;

namespace MonoGame.Common.Interfaces
{
    public interface ISimpleNetworking
    {
        void Update(NetworkMessage message);
    }
}
