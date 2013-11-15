using System.Collections.Generic;

namespace MonoGame.Server
{
    public interface INetworkComponent
    {
        void Update(NetworkMessage message);
    }
}
