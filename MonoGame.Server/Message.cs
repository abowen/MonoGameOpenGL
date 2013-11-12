using System.Net;

namespace MonoGame.Server
{
    public class Message
    {        
        public IPAddress Address;
        public int Port;
        public byte ClientId;
        public int EntityId;
        public byte Command;
        public byte[] Bytes;

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4}", Address, Port, ClientId, EntityId, Command);
        }
    }
}