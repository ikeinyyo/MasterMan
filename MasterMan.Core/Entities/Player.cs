using MasterMan.Common.Enums;
using MasterMan.Core.Services;
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
            var destination = Position.NextPosition(direction);
            EntityManager.Instance.World.MoveEntity(this, Position, destination);
        }
    }
}
