using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MonoGame.Networking;

namespace MonoGame.Server.Rpg
{
    public class Listener
    {
        public Listener()
        {
            _broadcastClient = new BroadcastClient();      
        }

        private readonly BroadcastClient _broadcastClient;

        public List<byte> Clients = new List<byte>();

        public void Listen()
        {
            while (true)
            {
                if (_broadcastClient.MessagesReceived.Any())
                {
                    var message = _broadcastClient.MessagesReceived.Dequeue();                                        
                    Console.WriteLine(message);
                    if (message.MessageContent.CommandId == Message.CommandRequestClientId)
                    {
                        Console.WriteLine("Client Request");
                        var newId = (byte) (Clients.Count() + 1);
                        _broadcastClient.Send(Message.SendClientId(newId));
                    }
                    
                }
                Thread.Sleep(10);
            }
        }

    }
}
