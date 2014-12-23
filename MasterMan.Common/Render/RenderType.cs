using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.Common.Render
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
