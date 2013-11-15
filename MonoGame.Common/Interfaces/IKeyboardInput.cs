using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Common.Interfaces
{
    public interface IKeyboardInput
    {
        IEnumerable<Keys> PressedKeys { get; }
    }
}
