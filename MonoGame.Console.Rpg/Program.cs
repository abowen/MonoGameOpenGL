using MonoGame.Game.Common.Helpers;
using MonoGame.Server.Rpg;

namespace MonoGame.Console.Rpg
{
    class Program
    {
        static void Main()
        {            
            var listener = new UdpController(InputHelper.KeyboardMappedKey(), 15123);
            listener.Run();            
        }
    }
}
