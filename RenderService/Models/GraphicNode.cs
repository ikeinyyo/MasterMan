using RenderService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderService.Models
{
    public class GraphicNode : IComparable
    {
        public Position Position { get; set; }

        public Position Texture { get; set; }

        public DrawPriority Priority { get; set; }

        public GraphicNode(Position position, Position texture, DrawPriority priority)
        {
            Position = position;
            Texture = texture;
            Priority = priority;
        }

        public int CompareTo(object obj)
        {
            int value = 0;

            if (obj.GetType().Equals(typeof(GraphicNode)))
            {
                value = Priority - ((GraphicNode)obj).Priority;
            }

            return value;
        }
    }
}
