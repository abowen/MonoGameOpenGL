using System.Linq;
using System.Net;

namespace MonoGame.Server
{
    public class Message
    {
        public Message(IPAddress address, int port, byte[] bytes)
        {
            Address = address;
            Port = port;
            Bytes = bytes;
            //MessageContent = new MessageContent(bytes);
        }

        public IPAddress Address;
        public int Port;
        public byte[] Bytes;
//        public MessageContent MessageContent;


        public override string ToString()
        {
            return string.Format("Address {0, -20} Port: {1, -10} BytesCount: {2, -3}", Address, Port, Bytes.Count());
        }
    }

    //public class MessageContent
    //{
    //    public MessageContent(byte[] bytes)
    //    {
    //        CommandId = bytes[0];
    //        ClientId = bytes[1];
    //        EntityId = bytes[2];
    //    }

    //    public byte CommandId;
    //    public byte ClientId;
    //    public byte EntityId;
    //}
}