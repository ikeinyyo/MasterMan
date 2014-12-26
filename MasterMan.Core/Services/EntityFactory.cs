using MasterMan.Common.Enums;
using MasterMan.Common.Models;
using MasterMan.Core.Components;
using MasterMan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.Core.Services
{
    public static class EntityFactory
    {
        public static Entity Tree
        {
            get
            {
                Entity entity = new Entity(EntityType.Tree);
                entity.AddComponent(new GraphicComponent(new Position(1, 0), Common.Render.Rendertype.FirstLevel));
                entity.AddComponent(new BodyComponent(true, false, true));

                return entity;
            }
        }

        public static Player Player
        {
            get
            {
                Player entity = new Player();
                entity.AddComponent(new GraphicComponent(new Position(2, 0), Common.Render.Rendertype.FirstLevel));
                entity.AddComponent(new BodyComponent(true, false, false));

                return entity;
            }
        }

        public static Entity Skeleton
        {
            get
            {
                Entity entity = new Entity(EntityType.Skeleton);
                entity.AddComponent(new GraphicComponent(new Position(3, 0), Common.Render.Rendertype.Normal));
                entity.AddComponent(new BodyComponent(true, true, false));
                entity.AddComponent(new BroomerComponent());
                return entity;
            }
        }

        public static Entity Dot
        {
            get
            {
                Entity entity = new Entity(EntityType.Dot);
                entity.AddComponent(new GraphicComponent(new Position(7, 0), Common.Render.Rendertype.Background));
                entity.AddComponent(new BodyComponent(false, false, false));
                entity.AddComponent(new MagneticComponent());

                return entity;
            }
        }
    }
}
