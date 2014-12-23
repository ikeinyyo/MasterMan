using MasterMan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.Core.Components
{
    public abstract class ComponentBase
    {
        public Entity Entity { get; set; }
        public abstract bool Update();

        public ComponentBase()
        {
            Entity = null;
        }

        public ComponentBase(Entity entity)
        {
            this.Entity = entity;
        }
    }
}
