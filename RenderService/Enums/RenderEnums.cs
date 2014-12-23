using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderService.Enums
{
    public enum DrawPriority
    {
        [Description("high")]
        High,
        [Description("normal")]
        Normal,
        [Description("low")]
        Low
    }
}
