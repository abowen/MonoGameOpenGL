using System.Net;

namespace MonoGame.Networking
{
    public class Message
    {
        public IPAddress Address;
        public int Port;
        public byte[] Bytes;
    }
}