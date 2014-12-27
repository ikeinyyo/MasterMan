using MasterMan.Common.Enums;
using MasterMan.Common.Models;
using MasterMan.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.Core.Entities
{
    public class Entity
    {
        public Position Position { get; set; }
        private List<ComponentBase> components;

        public EntityType Type { get; set; }

        public bool IsAlive { get; set; }


        public Entity()
        {
            components = new List<ComponentBase>();
            Position = new Position();
            Type = EntityType.None;
            IsAlive = true;
        }

        public Entity(EntityType type)
        {
            components = new List<ComponentBase>();
            Position = new Position();
            Type = type;
            IsAlive = true;
        }

        public bool Update()
        {
            bool finish = false;
            foreach (var item in components)
            {
                finish = finish || item.Update();
            }

            return finish;
        }

        public bool HasComponent(Type type)
        {
            bool hasComponent = false;

            foreach (var item in components)
            {
                if (item.GetType().Equals(type))
                {
                    hasComponent = true;
                    break;
                }
            }

            return hasComponent;
        }
        public void AddComponent(ComponentBase component)
        {
            if (component != null)
            {
                component.Entity = this;
                components.Add(component);
            }
        }

        public List<ComponentBase> GetComponent(Type type)
        {
            List<ComponentBase> components = new List<ComponentBase>();

            components = this.components.Where(c => c.GetType().Equals(type)).ToList();

            return components;
        }
    }
}
