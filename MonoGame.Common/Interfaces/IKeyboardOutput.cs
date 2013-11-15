using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Common.Interfaces
{
    public interface IKeyboardOuput
    {
        IEnumerable<Keys> PressedKeys { get; }
    }
}
