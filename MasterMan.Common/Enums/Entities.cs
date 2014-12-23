using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.Common.Enums
{
    public enum EntityType
    {
        [Description("none")]
        None,
        [Description("player")]
        Player,
        [Description("tree")]
        Tree,
        [Description("dot")]
        Dot,
        [Description("skeleton")]
        Skeleton
    }
}
