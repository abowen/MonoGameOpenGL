using System.Collections.Generic;
using MonoGame.Game.Common.Enums;

namespace MonoGame.Server.Common
{
    public class Mapping    
    {
        static Mapping()
        {
            NetworkMapping.Add(InputAction.Up, 1);
            NetworkMapping.Add(InputAction.Down, 2);
            NetworkMapping.Add(InputAction.Left, 3);
            NetworkMapping.Add(InputAction.Right, 4);
            NetworkMapping.Add(InputAction.Fire, 5);          
        }                
        
        public static Dictionary<InputAction, byte> NetworkMapping = new Dictionary<InputAction, byte>();
    }
}
