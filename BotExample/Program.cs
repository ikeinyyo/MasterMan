using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotExample
{
    class Program
    {
        static void Main(string[] args)
        {
            while (MasterMan.IsRun())
            {
                if (MasterMan.IsType(MasterMan.Right, MasterMan.Dot))
                {
                    MasterMan.Move(MasterMan.Right);
                }
                else if (MasterMan.IsType(MasterMan.Left, MasterMan.Dot))
                {
                    MasterMan.Move(MasterMan.Left);
                }
                else if (MasterMan.IsType(MasterMan.Up, MasterMan.Dot))
                {
                    MasterMan.Move(MasterMan.Up);
                }
                else if (MasterMan.IsType(MasterMan.Down, MasterMan.Dot))
                {
                    MasterMan.Move(MasterMan.Down);
                }
            }
        }
    }
}
