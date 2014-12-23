using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.Common.Enums
{
    public enum Direction
    {
        [Description("none")]
        None,
        [Description("up")]
        Up,
        [Description("down")]
        Down,
        [Description("left")]
        Left,
        [Description("right")]
        Right,
        [Description("upleft")]
        UpLeft,
        [Description("downleft")]
        DownLeft,
        [Description("upright")]
        UpRight,
        [Description("downright")]
        DownRight
    }
}
