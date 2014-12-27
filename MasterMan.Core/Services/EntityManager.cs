using MasterMan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.Core.Services
{
    public class EntityManager
    {
        #region Fields
        private List<Entity> entities;

        private List<Entity> deadEntities;

        public List<Entity> Entities
        {
            get { return entities; }
            set { entities = value; }
        }
        private Player player; 

        public Player Player
        {
            get { return player; }
            set { player = value; }
        }
        private World world;

        public World World
        {
            get { return world; }
            set { world = value; }
        }
        #endregion

        #region Constructor
        private static EntityManager instance;

        private EntityManager()
        {
            entities = new List<Entity>();
            deadEntities = new List<Entity>();
        }

        public static EntityManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EntityManager();
                }
                return instance;
            }
        }

        public void AddDeadEntity(Entity entity)
        {
            deadEntities.Add(entity);
        }
        #endregion

        #region Public Methods
        public void Clear()
        {
            entities.Clear();
        }

        public bool Update()
        {
            bool finish = false;
            deadEntities.Clear();

            if (player != null)
            {
                player.Update();
            }

            foreach (var entity in entities)
            {
                if (entity != null && entity.IsAlive)
                {
                    if(entity.Update())
                    {
                        finish = true;
                        World.EndedGame = true;
                        break;
                    }
                }
            }

            foreach (var entity in deadEntities)
            {
                World.EraseEntity(entity);
            }

            return finish;
        }
        #endregion
    }
}
