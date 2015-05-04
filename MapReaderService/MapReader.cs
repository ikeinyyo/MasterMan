using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapReaderService
{
    public class MapReader
    {
        public List<List<List<object>>> Map { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public MapReader(string mapPath, Dictionary<string, object> entityDictionary)
        {
            if(File.Exists(mapPath))
            {
                var lines = File.ReadAllLines(mapPath);
                int width = -1;
                int height = -1;

                if(lines.Count() > 2)
                {
                    int.TryParse(lines[0], out width);
                    int.TryParse(lines[1], out height);
                }

                if(width > 0 && height > 0)
                {
                    Width = width;
                    Height = height;
                    var map = new List<List<List<object>>>();

                    for (int x = 0; x < width; x++)
                    {
                        var row = new List<List<object>>();

                        for (int y = 0; y < height; y++)
                        {
                            row.Add(new List<object>());
                        }
                        map.Add(row);
                    }

                    int rowCount = 0;
                    foreach(var line in lines.Skip(2))
                    {
                        int columnsCount = 0;
                        foreach (var character in line)
                        {
                            if (entityDictionary.ContainsKey(character.ToString()))
                            {
                                object entity;
                                if (entityDictionary.TryGetValue(character.ToString(), out entity))
                                {
                                    map[columnsCount][rowCount].Add(entity);
                                }
                            }
                            columnsCount++;
                        }
                        rowCount++;
                    }

                    Map = map;
                }
                else
                {
                    throw new ArgumentException("Error in Map File.");
                }
            }
            else
            {
                throw new FileNotFoundException("Map File not found", mapPath);
            }
        }
    }
}
