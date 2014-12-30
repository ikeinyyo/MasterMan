using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.Core.Enums
{
    public enum Rendertype
    {
        [Description("firstLevel")]
        FirstLevel,
        [Description("normal")]
        Normal,
        [Description("background")]
        Background
    }
}
