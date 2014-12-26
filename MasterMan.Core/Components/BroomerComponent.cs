using MasterMan.Common.Enums;
using MasterMan.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.Core.Components
{
    public class BroomerComponent : ComponentBase
    {
        private Direction direction;
        public BroomerComponent()
        {
            direction = Direction.Left;
        }



        public override bool Update()
        {
            bool finish = false;
            var world = EntityManager.Instance.World;
            var destination = Entity.Position.NextPosition(direction);
            if (world.CanMove(destination))
            {
                world.MoveEntity(Entity, destination);
                if(Entity.Position.Equals(EntityManager.Instance.Player.Position))
                {
                    finish = true;
                    EntityManager.Instance.AddDeadEntity(EntityManager.Instance.Player);
                }
            }
            
            if(!world.CanMove(destination.NextPosition(direction)))
            {
                direction = DirectionHelper.GetOppositeDirection(direction);
            }

            

            return finish;
        }
    }
}
