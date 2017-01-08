using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BiPolarTowerDefence.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        public List<Wave> Waves = new List<Wave>();
        private SpriteBatch spriteBatch;
        public int WaveNumber = 1;
        public float DifficultyLevel = (float) 1;
        public static TowerButtonMenu TowerMenu = new TowerButtonMenu(Game1.Game, Vector3.Zero);
        public int FieryCountdown = 0;
        public int FrostyCountdown = 0;
        public int EarthyCountdown = 0;


        public int coin = 100;
        private int life = 100;
        private int killCount = 0;
        private int spawnCount = 1;

        private int count = 0;

        public Level(Game1 game, string levelName, int gameHeight, int gameWidth):base(game)
        {
            _game = game;
            _gameHeight = gameHeight;
            _gameWidth = gameWidth;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            TowerMenu = new TowerButtonMenu(Game1.Game, Vector3.Zero);

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

        }


        public void AddTile(int X, int Y, TileType type)
        {
            if (X >= _gameWidth || Y >= _gameHeight)
            {
                return;
            }
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

/*
            Vector3 waveVector = new Vector3(0,0,0);
            waveVector = Waves[WaveNumber].TheWave;
            if(count % 90 == 0)
            {
                SpawnEnemy(this, (int) waveVector.X, 0);
                Console.Write("Spawn1");
            }
            if(count % 90 == 30)
            {
                SpawnEnemy(this, (int) waveVector.Y, 1);
                Console.Write("Spawn2");
            }
            if(count % 90 == 60)
            {
                SpawnEnemy(this, (int)waveVector.Z, 2);
                Console.Write("Spawn3");
            }
*/


            if (spawnCount == 4)
            {
                spawnCount = 1;
                Vector3 waveVector = new Vector3(0,0,0);
                waveVector = Waves[WaveNumber++].TheWave;
                FrostyCountdown = (int) waveVector.X;
                //Console.WriteLine("Frosty count " + FrostyCountdown);
                FieryCountdown = (int) waveVector.Y;
                //Console.WriteLine("Fiery count " + FieryCountdown);
                EarthyCountdown = (int) waveVector.Z;
                //Console.WriteLine("Earthy count " + EarthyCountdown);
            }

            if (count++ % 30 == 0)
            {
                //Console.WriteLine("Spawn count " + spawnCount);
                if (spawnCount == 1 && EarthyCountdown > 0)
                {
                    SpawnEnemy(this, EnemyType.Earthy);
                    EarthyCountdown--;

                }
                if (EarthyCountdown == 0 && spawnCount == 1)
                {
                    spawnCount++;
                }


                if (spawnCount == 2 && FieryCountdown > 0)
                {
                    SpawnEnemy(this, EnemyType.Fiery);
                    FieryCountdown--;
                }
                if (FieryCountdown == 0 && spawnCount == 2)
                {
                    spawnCount++;
                }
                if (spawnCount == 3 && FrostyCountdown > 0)
                {
                    SpawnEnemy(this, EnemyType.Frosty);
                    FrostyCountdown--;
                }
                if (EarthyCountdown == 0 && spawnCount == 3)
                {
                    spawnCount++;
                }
            }

            if (this._game.mouseState.RightButton == ButtonState.Pressed)
            {
                TowerMenu.Active = false;
                DeselectAllTowers();
            }
            if (TowerMenu.Active)
            {
                TowerMenu.Update(gameTime);
            }
            
            base.Update(gameTime);
            count++;
        }

        public void DeselectAllTowers()
        {
            foreach (var item in this._components)
            {
                var tower = item as Tower;
                if (tower != null)
                {
                    tower.Deselect();
                }
            }
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

            // Hér eru GUI hlutir
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

            if (TowerMenu.Active)
            {
                TowerMenu.Draw(gameTime);
            }
        }

        public int DrawOrder { get; }
        public bool Visible { get; }
        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        public void AddWaypoint(int X, int Y)
        {
            this.Waypoints.Add(new Waypoint(X *  Tile.TILE_SIZE,Y *  Tile.TILE_SIZE));
        }

        public void AddWave(int Frosty, int Fiery, int Earthy)
        {
            this.Waves.Add(new Wave(Frosty,Fiery,Earthy));
        }

        public void SpawnEnemy(Level level,EnemyType type)
        {
            var enemy = new Enemy(this,type) {position = this.Waypoints[0].position};
            //Console.WriteLine("Type " + enemyType + " from " + type);
            AddComponent(enemy);
        }


        public void incramentWave()
        {
            WaveNumber += 1;
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

        public bool PayUp(int i)
        {
            if (i <= coin)
            {
                coin -= i;
                return true;
            }
            return false;

        }

        public void addTestTowers()
        {
            AddComponent(new Tower(this, new Vector3(3 * Tile.TILE_SIZE + Tile.TILE_SIZE, 0, 3 * Tile.TILE_SIZE + Tile.TILE_SIZE)));
            AddComponent(new Tower(this, new Vector3(3 * Tile.TILE_SIZE + Tile.TILE_SIZE, 0, 4 * Tile.TILE_SIZE + Tile.TILE_SIZE)));
            AddComponent(new Tower(this, new Vector3(3 * Tile.TILE_SIZE + Tile.TILE_SIZE, 0, 5 * Tile.TILE_SIZE + Tile.TILE_SIZE)));
            AddComponent(new Tower(this, new Vector3(3 * Tile.TILE_SIZE + Tile.TILE_SIZE, 0, 6 * Tile.TILE_SIZE + Tile.TILE_SIZE)));
            AddComponent(new Tower(this, new Vector3(3 * Tile.TILE_SIZE + Tile.TILE_SIZE, 0, 7 * Tile.TILE_SIZE + Tile.TILE_SIZE)));
            AddComponent(new Tower(this, new Vector3(15* Tile.TILE_SIZE + Tile.TILE_SIZE, 0, 10 * Tile.TILE_SIZE + Tile.TILE_SIZE)));
        }


        public void AddTower(int X, int Y)
        {
            AddComponent(new Tower(this, new Vector3(X * Tile.TILE_SIZE, 0, Y * Tile.TILE_SIZE)));
        }
    }
}