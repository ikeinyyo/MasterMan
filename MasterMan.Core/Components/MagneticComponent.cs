using MasterMan.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.Core.Components
{
    public class MagneticComponent : ComponentBase
    {
        private int points;

        public MagneticComponent(int points = 1)
        {
            this.points = points;
        }
        public override bool Update()
        {
            var player = EntityManager.Instance.Player;
            if(player.Position.Equals(Entity.Position))
            {
                EntityManager.Instance.AddDeadEntity(Entity);
                player.AddPoints(points);
                Entity.IsAlive = false;
            }
            return false;
        }
    }
}
