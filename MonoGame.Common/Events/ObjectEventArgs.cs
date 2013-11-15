using System;
using MonoGame.Common.Enums;

namespace MonoGame.Common.Events
{
    public class ObjectEventArgs : EventArgs
    {
        public ObjectEvent Action { get; set; }
    }
}
