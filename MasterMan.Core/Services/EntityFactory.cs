using MasterMan.Core.Enums;
using MasterMan.Core.Models;
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
                entity.AddComponent(new GraphicComponent(new Position(1, 0), Rendertype.FirstLevel));
                entity.AddComponent(new BodyComponent(true, false, true));

                return entity;
            }
        }

        public static Player Player
        {
            get
            {
                Player entity = new Player();
                entity.AddComponent(new GraphicComponent(new Position(2, 0), Rendertype.FirstLevel));
                entity.AddComponent(new BodyComponent(true, false, false));

                return entity;
            }
        }

        public static Entity Skeleton
        {
            get
            {
                Entity entity = new Entity(EntityType.Skeleton);
                entity.AddComponent(new GraphicComponent(new Position(3, 0), Rendertype.Normal));
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
                entity.AddComponent(new GraphicComponent(new Position(7, 0), Rendertype.Background));
                entity.AddComponent(new BodyComponent(false, false, false));
                entity.AddComponent(new MagneticComponent());

                return entity;
            }
        }

        public static Entity FromType(EntityType type)
        {
            Entity entity = null;
            switch(type)
            {
                case EntityType.Dot:
                    entity = Dot;
                    break;
                case EntityType.Player:
                    entity = Player;
                    break;
                case EntityType.Skeleton:
                    entity = Skeleton;
                    break;
                case EntityType.Tree:
                    entity = Tree;
                    break;
            }

            return entity;
        }
    }
}
