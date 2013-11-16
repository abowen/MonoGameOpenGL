using System.Linq;
using System.Net;

namespace MonoGame.Common.Networking
{
    public class NetworkMessage
    {
        public NetworkMessage(IPAddress address, int port, byte[] bytes)
        {
            Address = address;
            Port = port;
            Bytes = bytes;
        }

        public IPAddress Address;
        public int Port;
        public byte[] Bytes;

        public override string ToString()
        {
            return string.Format("Address {0, -20} Port: {1, -10} BytesCount: {2, -3}", Address, Port, Bytes.Count());
        }
    }
}