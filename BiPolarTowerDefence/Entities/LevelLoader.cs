using System;
using System.IO;
using System.Net;

namespace BiPolarTowerDefence.Entities
{
    public class LevelLoader
    {
        private readonly Level _level;
        private readonly string _basePath;

        public LevelLoader(Level level, string basePath)
        {
            _level = level;
            _basePath = "Content/" + basePath;

            var tilesPath = _basePath + "/Tiles.csv";
            var waypointsPath = _basePath + "/Waypoints.csv";

            if (!File.Exists(tilesPath))
            {
                throw new FileNotFoundException("Cannot find a file by the name of:" + tilesPath);
            }

            if (!File.Exists(waypointsPath))
            {
                throw new FileNotFoundException("Cannot find a file by the name of:" + waypointsPath);
            }

            this.loadTiles(File.ReadAllLines(tilesPath));
            this.loadWaypoints(File.ReadAllLines(waypointsPath));
        }

        private void loadWaypoints(string[] lines)
        {
            foreach (var line in lines)
            {
                var items = line.Split(',');
                if (items.Length < 2)
                {
                    continue;
                }
                _level.AddWaypoint(Int32.Parse(items[0]), Int32.Parse(items[1]));
            }
        }

        private void loadTiles(string[] lines)
        {
            foreach(var line in lines)
            {
                var items = line.Split(',');
                if (items.Length < 3)
                {
                    continue;
                }

                _level.AddTile(Int32.Parse(items[0]), Int32.Parse(items[1]), (TileType)Int32.Parse(items[2]));
            }
        }
    }
}