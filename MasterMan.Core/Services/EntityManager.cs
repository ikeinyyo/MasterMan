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
        #endregion

        #region Public Methods
        public void Clear()
        {
            entities.Clear();
        }

        public bool Update()
        {
            bool finish = false;

            if (player != null)
            {
                player.Update();
            }

            foreach (var entity in entities)
            {
                if (entity != null)
                {
                    finish = finish || entity.Update();
                }
            }

            return finish;
        }
        #endregion
    }
}
