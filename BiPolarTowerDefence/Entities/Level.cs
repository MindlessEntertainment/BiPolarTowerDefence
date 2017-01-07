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

        private SpriteBatch spriteBatch;

        public Level(Game1 game):base(game)
        {
            _game = game;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public void NewTile(int X, int Y, TileType type)
        {
            var tile = new Tile(this._game,new Vector3(X,0,Y),type );
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
    }
}