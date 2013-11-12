using System;
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

        public void Listen()
        {
            while (true)
            {
                if (_broadcastClient.MessagesReceived.Any())
                {
                    var message = _broadcastClient.MessagesReceived.Dequeue();                                        
                    Console.WriteLine(message);
                }
                Thread.Sleep(10);
            }
        }

    }
}
