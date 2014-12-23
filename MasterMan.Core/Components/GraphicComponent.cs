using MasterMan.Common.Models;
using MasterMan.Common.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.Core.Components
{
    public class GraphicComponent : ComponentBase
    {
        public Position Texture { get; set; }
        public Rendertype RenderType { get; set; }

        public GraphicComponent(Position texture)
        {
            Texture = texture;
            RenderType = Rendertype.Normal;
        }

        public GraphicComponent(Position texture, Rendertype renderType)
        {
            Texture = texture;
            RenderType = renderType;
        }

        public override bool Update()
        {
            return false;
        }
    }
}
