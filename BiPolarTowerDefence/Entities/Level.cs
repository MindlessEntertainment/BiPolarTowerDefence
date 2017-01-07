using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Entities
{
    public class Level:GameComponent, IDrawable
    {
        private readonly Game1 _game;
        private List<GameComponent> Components = new List<GameComponent>();
        private List<Waypoint> Waypoints = new List<Waypoint>();

        private SpriteBatch spriteBatch;

        public Level(Game1 game, string level):base(game)
        {
            _game = game;
            new LevelLoader(this, level);

            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public void AddTile(int X, int Y, TileType type)
        {
            var tile = new Tile(this._game,new Vector3(X*Tile.TILE_SIZE,0,Y*Tile.TILE_SIZE),type );
            this.Components.Add(tile);
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

    internal class Waypoint
    {
        public Vector3 position;

        public Waypoint(Vector3 position)
        {
            this.position = position;
        }

        public Waypoint(float X, float Z)
        {
            this.position = new Vector3(X,0,Z);
        }

        public Waypoint(float X, float Y, float Z)
        {
            this.position = new Vector3(X,Y,Z);
        }
    }
}