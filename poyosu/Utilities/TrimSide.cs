using System;
using System.Collections.Generic;
using System.Text;

namespace poyosu.Utilities
{
    [Flags]
    public enum TrimSide
    {
        Left = 1 << 0,
        Right = 1 << 1,
        Top = 1 << 2,
        Bottom = 1 << 3,
        Horizontal = Left | Right,
        Vertical = Top | Bottom,
        All = Horizontal | Vertical,
    }
}
