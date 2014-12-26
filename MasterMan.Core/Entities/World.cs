using MasterMan.Common.Enums;
using MasterMan.Common.Models;
using MasterMan.Core.Components;
using MasterMan.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.Core.Entities
{
    public class World
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public List<List<List<Entity>>> WorldMap { get; set; }

        public World(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Argument <= 0", "Width");
            }

            if (height <= 0)
            {
                throw new ArgumentException("Argument <= 0", "Height");
            }


            Width = width;
            Height = height;

            WorldMap = new List<List<List<Entity>>>();

            for (int x = 0; x < Width; x++)
            {
                var row = new List<List<Entity>>();

                for (int y = 0; y < Height; y++)
                {
                    row.Add(new List<Entity>());
                }

                WorldMap.Add(row);
            }

            EntityManager.Instance.Clear();
            EntityManager.Instance.World = this;
        }

        #region Public Methods
        public void SetPlayer(Player player, Position position)
        {
            if (position != null && position.Validate(0, Width - 1, 0, Height - 1))
            {
                if (player != null)
                {
                    player.Position = position;
                    WorldMap[position.X][position.Y].Add(player);
                    EntityManager.Instance.Player = player;
                }
                else
                {
                    throw new ArgumentException("Entity is null", "entity");
                }
            }
            else
            {
                throw new ArgumentException("Position out of range", "position");
            }
        }

        public void SetEntity(Entity entity, Position position)
        {
            if (position != null && position.Validate(0, Width - 1, 0, Height - 1))
            {
                if (entity != null)
                {
                    entity.Position = position;
                    WorldMap[position.X][position.Y].Add(entity);
                }
                else
                {
                    throw new ArgumentException("Entity is null", "entity");
                }
            }
            else
            {
                throw new ArgumentException("Position out of range", "position");
            }
        }

        public void AddEntity(Entity entity, Position position)
        {
            if (position != null && position.Validate(0, Width - 1, 0, Height - 1))
            {
                if (entity != null)
                {
                    SetEntity(entity, position);
                    EntityManager.Instance.Entities.Add(entity);
                }
                else
                {
                    throw new ArgumentException("Entity is null", "entity");
                }
            }
            else
            {
                throw new ArgumentException("Position out of range", "position");
            }
        }

        public void RemoveEntity(Entity entity)
        {
            if (entity != null && entity.Position != null && entity.Position.Validate(0, Width - 1, 0, Height - 1))
            {
                WorldMap[entity.Position.X][entity.Position.Y].Remove(entity);
            }
            else
            {
                throw new ArgumentException("Position out of range", "position");
            }
        }

        public void EraseEntity(Entity entity)
        {
            if (entity != null && entity.Position != null && entity.Position.Validate(0, Width - 1, 0, Height - 1))
            {
                WorldMap[entity.Position.X][entity.Position.Y].Remove(entity);
                EntityManager.Instance.Entities.Remove(entity);
            }
            else
            {
                throw new ArgumentException("Position out of range", "position");
            }
        }

        public void MoveEntity(Entity entity, Position origin, Position destination)
        {
            if (origin != null && origin.Validate(0, Width - 1, 0, Height - 1) &&
                destination != null && destination.Validate(0, Width - 1, 0, Height - 1))
            {
                if (entity != null && ExistEntity(entity, origin))
                {
                    RemoveEntity(entity);
                    SetEntity(entity, destination);

                    entity.Position.X = destination.X;
                    entity.Position.Y = destination.Y;
                }
                else
                {
                    throw new ArgumentException("Entity is null", "entity");
                }
            }
        }

        public bool ExistEntity(Entity entity, Position position)
        {
            bool exist = false;

            if (position != null && position.Validate(0, Width - 1, 0, Height - 1))
            {
                if (entity != null)
                {
                    exist = WorldMap[position.X][position.Y].Exists(e => e == entity);
                }
                else
                {
                    throw new ArgumentException("Entity is null", "entity");
                }
            }
            else
            {
                throw new ArgumentException("Position out of range", "position");
            }

            return exist;
        }

        public bool HasComponent(Type type, Position position)
        {
            bool hasComponent = false;

            if (position != null && position.Validate(0, Width - 1, 0, Height - 1))
            {
                hasComponent = WorldMap[position.X][position.Y].Exists(e => e.HasComponent(type));
            }
            else
            {
                throw new ArgumentException("Position out of range", "position");
            }

            return hasComponent;
        }

        public bool IsType(EntityType type, Position position)
        {
            bool hasComponent = false;

            if (position != null && position.Validate(0, Width - 1, 0, Height - 1))
            {
                hasComponent = WorldMap[position.X][position.Y].Exists(e => e.Type.Equals(type));
            }
            else
            {
                throw new ArgumentException("Position out of range", "position");
            }

            return hasComponent;
        }

        public bool CanMove(Position position)
        {
            bool canMove = true;

            if (position != null && position.Validate(0, Width - 1, 0, Height - 1))
            {
                var entities = WorldMap[position.X][position.Y];

                foreach (var entity in entities)
                {
                    List<ComponentBase> components = entity.GetComponent(typeof(BodyComponent));

                    foreach (BodyComponent component in components)
                    {
                        if (component.Large)
                        {
                            canMove = false;
                            break;
                        }
                    }

                    if (!canMove)
                    {
                        break;
                    }
                }
            }
            else
            {
                canMove = false;
            }

            return canMove;
        }

        #endregion
    }
}
