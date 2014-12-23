using RenderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RenderService.Services
{
    public class RenderService
    {
        private int tileSize;
        private Canvas world;
        private TextureService textureService;
        private Color backgroundColor;

        public RenderService(Canvas world, string uri, int tileSize, int mapSize)
        {
            this.world = world;
            textureService = new TextureService(uri, tileSize, mapSize);
            backgroundColor = Colors.White;
            this.tileSize = tileSize;
        }

        public void SetBackgroundColor(Color color)
        {
            backgroundColor = color;
        }

        public void Render(List<GraphicNode> tree)
        {
            world.Children.Clear();

            world.Background = new SolidColorBrush(backgroundColor);

            foreach (var node in tree)
            {
                Rectangle tile = new Rectangle();
                tile.Fill = new ImageBrush(textureService.GetTexture(node.Texture));
                tile.Width = tileSize;
                tile.Height = tileSize;

                Canvas.SetLeft(tile, node.Position.X * tileSize);
                Canvas.SetTop(tile, node.Position.Y * tileSize);
                world.Children.Add(tile);

            }
        }
    }
}
