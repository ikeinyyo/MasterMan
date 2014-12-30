using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.Core.Enums
{
    public enum Status
    {
        [Description("run")]
        Run,
        [Description("win")]
        Win,
        [Description("gameover")]
        GameOver
    }
}
