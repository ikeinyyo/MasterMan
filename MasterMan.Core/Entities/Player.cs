using MasterMan.Core.Enums;
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

        public bool Move(Direction direction)
        {
            bool moved = false;
            var destination = Position.NextPosition(direction);
            var world = EntityManager.Instance.World;
            if (world.CanMove(destination))
            {
                moved = true;
                world.MoveEntity(this, destination);
            }

            return moved;
        }
    }
}
