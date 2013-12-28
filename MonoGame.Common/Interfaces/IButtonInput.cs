using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Common.Interfaces
{
    public interface IButtonInput
    {
        IEnumerable<Buttons> ButtonsPressed { get; }
    }
}
