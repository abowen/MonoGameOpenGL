using MonoGame.Common.Presets;
using MonoGame.Server;

namespace MonoGame.Input.Rpg
{
    class KeyboardUdp
    {
        static void Main()
        {
            var listener = new UdpController(15123, KeyboardPresets.BasicKeyboardMapping);
            listener.Run();            
        }
    }
}
