using System;
using MonoGame.Game.Common.Enums;

namespace MonoGame.Game.Common.Events
{
    public class ObjectEventArgs : EventArgs
    {
        public ObjectEvent Action { get; set; }
    }
}
