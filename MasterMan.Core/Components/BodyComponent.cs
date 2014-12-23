using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.Core.Components
{
    class BodyComponent: ComponentBase
    {
        public bool Solid { get; private set;}
        public bool Mortal { get; private set; }
        // Only one Entity in a cell
        public bool Large { get; private set; }


        public BodyComponent() : base()
        {
        }

        public BodyComponent(bool solid, bool mortal, bool large) : base()
        {
            Solid = solid;
            Mortal = mortal;
            Large = large;
        }

        public override bool Update()
        {
            return false;
        }
    }
}
