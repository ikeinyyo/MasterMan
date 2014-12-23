using MasterMan.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.Core.Entities
{
    public class Player : Entity
    {
        public Player()
        {
            Type = EntityType.Player;
        }
        public void Move(Direction direction)
        {
           // TODO: Implement
        }
    }
}
