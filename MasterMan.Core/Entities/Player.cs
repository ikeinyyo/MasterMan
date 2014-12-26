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
        private int points;

        public Player()
        {
            Type = EntityType.Player;
            points = 0;
        }

        public void AddPoints(int points)
        {
            if(points > 0)
            {
                this.points += points;
            }
        }

        public void Move(Direction direction)
        {
            var destination = Position.NextPosition(direction);
            var world = EntityManager.Instance.World;
            if (world.CanMove(destination))
            {
                world.MoveEntity(this, Position, destination);
            }
        }
    }
}
