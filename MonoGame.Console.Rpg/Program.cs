using MonoGame.Server.Rpg;

namespace MonoGame.Console.Rpg
{
    class Program
    {
        static void Main(string[] args)
        {
            // Could be hosted in a service / IIS
            var listener = new Listener();
            listener.Listen();
        }
    }
}
