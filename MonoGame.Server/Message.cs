using System;
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
            MessageContent = new MessageContent(bytes);
        }

        public IPAddress Address;
        public int Port;
        public byte[] Bytes;
        public MessageContent MessageContent;


        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4}", Address, Port, MessageContent.CommandId, MessageContent.ClientId, MessageContent.EntityId);
        }

        // Move to mapping?
        public static byte CommandRequestClientId = 1;

        public static byte[] RequestClientId()
        {
            return new byte[] { CommandRequestClientId, 0, 0 };
        }
    }

    public class MessageContent
    {
        public MessageContent(byte[] bytes)
        {
            CommandId = bytes[0];
            ClientId = bytes[1];
            EntityId = bytes[2];
        }

        public byte[] ToBytes
        {
            get
            {
                return new byte[] { CommandId, ClientId, EntityId };
            }
        }

        public byte CommandId;
        public byte ClientId;
        public byte EntityId;
    }
}