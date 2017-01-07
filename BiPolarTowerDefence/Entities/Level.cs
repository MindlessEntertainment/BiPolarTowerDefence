using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Entities
{
    public class Level:GameComponent, IDrawable
    {
        private readonly Game1 _game;
        private readonly int _gameHeight;
        private readonly int _gameWidth;
        private Tile[,] tiles;
        private List<GameComponent> Components = new List<GameComponent>();
        private List<Waypoint> Waypoints = new List<Waypoint>();

        private SpriteBatch spriteBatch;

        public Level(Game1 game, string level, int gameHeight, int gameWidth):base(game)
        {
            _game = game;
            _gameHeight = gameHeight;
            _gameWidth = gameWidth;

            tiles = new Tile[_gameWidth,_gameHeight];
            for (int x = 0; x < _gameWidth; x++)
            {
                for (int y = 0; y < _gameHeight; y++)
                {
                    var tile = new Tile(_game, new Vector3(x*Tile.TILE_SIZE,0,y*Tile.TILE_SIZE),TileType.Grass);
                    tiles[x, y] = tile;
                    this.Components.Add(tile);
                }
            }

            new LevelLoader(this, level);

            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public void AddTile(int X, int Y, TileType type)
        {
            var tile = this.tiles[X, Y];
            tile.Type = type;
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (var item in Components)
            {
                var drawable = item as IMyGameDrawable;
                if (drawable != null)
                {
                    drawable.Draw(gameTime, spriteBatch);
                }
            }
            spriteBatch.End();
        }

        public int DrawOrder { get; }
        public bool Visible { get; }
        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        public void AddWaypoint(int X, int Y)
        {
            this.Waypoints.Add(new Waypoint(X,Y));
        }
    }
}