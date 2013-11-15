using MonoGame.Game.Rpg;
using MonoGame.Server;

namespace MonoGame.Input.Rpg
{
    class KeyboardUdp
    {
        static void Main()
        {
            var listener = new UdpController(15123, KeyboardMapping.ConvertKeysToBytes);
            listener.Run();            
        }      
    }
}
