using System;
using System.Collections.Generic;
using System.Linq;
using BiPolarTowerDefence.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Entities
{
    public class Level:GameComponent, IDrawable
    {
        public readonly Game1 _game;
        private readonly int _gameHeight;
        private readonly int _gameWidth;
        private Tile[,] tiles;
        private List<GameComponent> _components = new List<GameComponent>();
        public List<Waypoint> Waypoints = new List<Waypoint>();
        private SpriteBatch spriteBatch;
        public int WaveNumber = 1;
        public float DifficultyLevel = (float) 0.25;
        int coin = 100;
        private int life = 10;
        private int killCount = 0;

        private int count = 0;

        public Level(Game1 game, string levelName, int gameHeight, int gameWidth):base(game)
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
                    this._components.Add(tile);
                }
            }

            new LevelLoader(this, levelName);



            var tower = new Tower(this, new Vector3(3 * Tile.TILE_SIZE + Tile.TILE_SIZE/2, 0, 3 * Tile.TILE_SIZE + Tile.TILE_SIZE/2));
            this._components.Add(tower);
            AddComponent(new Tower(this, new Vector3(3 * Tile.TILE_SIZE + Tile.TILE_SIZE/2, 0, 4 * Tile.TILE_SIZE + Tile.TILE_SIZE/2)));
            AddComponent(new Tower(this, new Vector3(3 * Tile.TILE_SIZE + Tile.TILE_SIZE/2, 0, 5 * Tile.TILE_SIZE + Tile.TILE_SIZE/2)));
            AddComponent(new Tower(this, new Vector3(3 * Tile.TILE_SIZE + Tile.TILE_SIZE/2, 0, 6 * Tile.TILE_SIZE + Tile.TILE_SIZE/2)));
            AddComponent(new Tower(this, new Vector3(3 * Tile.TILE_SIZE + Tile.TILE_SIZE/2, 0, 7 * Tile.TILE_SIZE + Tile.TILE_SIZE/2)));
            AddComponent(new Tower(this, new Vector3(3 * Tile.TILE_SIZE + Tile.TILE_SIZE/2, 0, 8 * Tile.TILE_SIZE + Tile.TILE_SIZE/2)));

            AddComponent(new Tower(this, new Vector3(10* Tile.TILE_SIZE + Tile.TILE_SIZE/2, 0, 3 * Tile.TILE_SIZE + Tile.TILE_SIZE/2)));
            //AddComponent(new Tower(this, new Vector3(8 * Tile.TILE_SIZE + Tile.TILE_SIZE/2, 0, 5 * Tile.TILE_SIZE + Tile.TILE_SIZE/2)));
            //AddComponent(new Tower(this, new Vector3(8 * Tile.TILE_SIZE + Tile.TILE_SIZE/2, 0, 6 * Tile.TILE_SIZE + Tile.TILE_SIZE/2)));
            //AddComponent(new Tower(this, new Vector3(8 * Tile.TILE_SIZE + Tile.TILE_SIZE/2, 0, 7 * Tile.TILE_SIZE + Tile.TILE_SIZE/2)));
            //AddComponent(new Tower(this, new Vector3(8 * Tile.TILE_SIZE + Tile.TILE_SIZE/2, 0, 8 * Tile.TILE_SIZE + Tile.TILE_SIZE/2)));



            ///this._components.Add(new Enemy(_game,new Vector3(5 * Tile.TILE_SIZE + Tile.TILE_SIZE/2, 0, 3 * Tile.TILE_SIZE + Tile.TILE_SIZE/2)));

            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }


        public void AddTile(int X, int Y, TileType type)
        {
            var tile = this.tiles[X, Y];
            tile.Type = type;
        }



        public override void Update(GameTime gameTime)
        {
            for (int i = _components.Count-1; i > 0; i--)
            {
                var item = _components[i];
                if (item == null) throw new ArgumentNullException(nameof(item));
                if (item.Enabled == false)
                {
                    continue;
                }
                item.Update(gameTime);

                var collider = item as ICollider;
                if (collider != null)
                {
                    foreach (var itemInner in _components)
                    {
                        if (itemInner.Enabled == false)
                        {
                            continue;
                        }
                        var collidable = itemInner as ICollidable;
                        if (collidable != null)
                        {
                            if (collidable.Intersects(collider))
                            {
                                collidable.OnCollision(collider);
                                collider.OnCollision(collidable);
                            }
                        }
                    }
                }
            }
            if (life < 1)
            {
                _game.ScreenManager.ActivateScreen(GameScreens.GameOver);
            }

            if (count++ % 30 == 0)
            {
                SpawnEnemy(this);
            }
            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (var item in _components)
            {
                if (item.Enabled == false)
                {
                    continue;
                }
                var drawable = item as IMyGameDrawable;
                if (drawable != null)
                {
                    drawable.Draw(gameTime, spriteBatch);
                }
            }

            // Draw Hello World
            var Font1 = _game.Font;
            string output1 =  coin + " : Coins";
            Vector2 FontOrigin1 = new Vector2(Font1.MeasureString(output1).X,0);
            spriteBatch.DrawString(Font1, output1, new Vector2(900 , 15), Color.White,
                0, FontOrigin1, 1.0f, SpriteEffects.None, 0.5f);

            string output2 = life + " :    Life";
            Vector2 FontOrigin2 = new Vector2(Font1.MeasureString(output2).X,0);
            spriteBatch.DrawString(Font1, output2, new Vector2(900 , 40), Color.White,
                0, FontOrigin2, 1.0f, SpriteEffects.None, 0.5f);


            string output3 = killCount + " :   Kills";
            Vector2 FontOrigin3 = new Vector2(Font1.MeasureString(output3).X,0);
            spriteBatch.DrawString(Font1, output3, new Vector2(900 , 60), Color.White,
                0, FontOrigin3, 1.0f, SpriteEffects.None, 0.5f);


            spriteBatch.End();
        }

        public int DrawOrder { get; }
        public bool Visible { get; }
        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        public void AddWaypoint(int X, int Y)
        {
            this.Waypoints.Add(new Waypoint(X *  Tile.TILE_SIZE,Y *  Tile.TILE_SIZE));
        }

        public void SpawnEnemy(Level level)
        {

            AddComponent(new Enemy(this,(EnemyType)_game.random.Next(0,3)){position = new Vector3(-10,0,0)});

        }

        public void AddComponent(GameComponent component)
        {
            this._components.Add(component);
        }

        public void addMoney(int i)
        {
            coin +=i;
        }

        public void loselife(int i)
        {
            life -= i;
        }

        public void score(int i)
        {
            killCount += i;
        }

        public IEnumerable<GameComponent> getComponents()
        {
            return _components;
        }
    }
}