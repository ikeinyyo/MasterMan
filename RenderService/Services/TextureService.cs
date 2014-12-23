using RenderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RenderService.Services
{
    public class TextureService
    {
        private  ImageSource[,] tiles;
        private int mapSize;
        private int tileSize;

        // @"pack://application:,,,/MasterMan.Client;component/Assets/Textures/texture-map.png"
        public TextureService(string uri, int tileSize, int mapSize)
        {
            this.tileSize = tileSize;
            this.mapSize = mapSize;

            loadTilemap(uri);
        }

        private void loadTilemap(string uri)
        {
            
            if (mapSize > 0 && tileSize > 0)
            {
                int rows, columns;
                rows = columns = mapSize / tileSize;
                tiles = new ImageSource[rows, columns];

                BitmapSource originalImage = new BitmapImage(new Uri(uri, UriKind.Absolute));

                for (int x = 0; x < rows; x++)
                {
                    for (int y = 0; y < columns; y++)
                    {
                        Int32Rect cropRect = new Int32Rect(x * tileSize, y * tileSize, tileSize, tileSize);
                        CroppedBitmap croppedBitmap = new CroppedBitmap(originalImage, cropRect);

                        tiles[x, y] = croppedBitmap;
                    }
                }
            }
        }

        public ImageSource GetTexture(Position position)
        {
            ImageSource texture = null;
            int rows, columns;
            rows = columns = mapSize / tileSize;

            if (position.X >= 0 && position.X < columns && position.Y >= 0 && position.Y < rows)
            {
                texture = tiles[position.X, position.Y];
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }

            return texture;
        }

    }
}
