using MasterMan.Core.Components;
using RenderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.Core.Services
{
    public class SceneManager
    {
        private static SceneManager instance;
        private SceneManager()
        {
            tree = new List<GraphicNode>();
        }

        public static SceneManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneManager();
                }
                return instance;
            }
        }

        private List<GraphicNode> tree;

        public List<GraphicNode> GetUpdatedTree()
        {
            uploadTree();
            return tree;
        }

        private void uploadTree()
        {
            EntityManager entityManager = EntityManager.Instance;
            var entities = entityManager.Entities;

            if (entityManager.Player != null)
            {
                entities.Add(entityManager.Player);
            }

            Clear();
            foreach (var entity in entities)
            {
                var graphicComponents = entity.GetComponent(typeof(GraphicComponent));

                foreach (var component in graphicComponents)
                {
                    if(component != null)
                    {
                        GraphicComponent graphicComponent = component as GraphicComponent;
                        RenderService.Models.Position entityPosition = new Position(entity.Position.X, entity.Position.Y);
                        RenderService.Models.Position texturePosition = new Position(graphicComponent.Texture.X, graphicComponent.Texture.Y);
                        RenderService.Enums.DrawPriority priority;
                        switch(graphicComponent.RenderType)
                        {
                            case Common.Render.Rendertype.FirstLevel:
                                priority = RenderService.Enums.DrawPriority.High;
                                break;
                            case Common.Render.Rendertype.Background:
                                priority = RenderService.Enums.DrawPriority.Low;
                                break;
                            case Common.Render.Rendertype.Normal:
                                priority = RenderService.Enums.DrawPriority.Normal;
                                break;
                            default: 
                                priority = RenderService.Enums.DrawPriority.Normal;
                                break;
                        }

                        GraphicNode node = new GraphicNode(entityPosition, texturePosition, priority);
                        addNode(node);
                    }
                }
            }
        }

        private void addNode(GraphicNode node)
        {
            tree.Add(node);
        }

        public void Clear()
        {
            tree.Clear();
            tree = new List<GraphicNode>();
        }
    }
}
