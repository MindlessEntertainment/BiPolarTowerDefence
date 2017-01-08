using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using BiPolarTowerDefence.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Entities
{
    public class Enemy: BaseObject, IMyGameDrawable, ICollidable
    {
        public Queue<Enemy> enemyReuseQueue = new Queue<Enemy>();
        private readonly Level _level;
        private readonly Game1 _game;
        private Texture2D currentTexture;
        private static Texture2D earthyTexture;
        private static Texture2D fieryTexture;
        private static Texture2D frostyTexture;

        private static Texture2D textureHealth;

        private EnemyType _enemyType;
        public EnemyType EnemyType {
            get
            {
                return _enemyType;
            }
            private set{
                this.setEnemyType(value);
            }
        }
        public Direction myDirection;
        public int Life { get; private set; }
        private int MaxLife;
        private const int lifeBarWidth = 50;
        private const int spriteCellWidth = 600-1;
        private const int spriteCellHeight = 600-1;
        private float speed;
        private const float initialSpeed = 3;
        private Vector3 distanceVector;
        private int WaypointIndex = 0;
        private Vector3 velocityVector;
        public Vector3 VelocityVector { get { return velocityVector; }}

        //Animation variables
        private int animationIndex;
        private int animationFrameCount = 1;
        private int deathAnimationFrameCount = 1;
        private int animationFramesByCell = 1;
        private int deathAnimationFramesByCell = 1;

        public Enemy(Level level,EnemyType enemyType) : base(level._game, Vector3.Zero)
        {
            _game = level._game;
            _level = level;
            this.Initialize();

            this.setEnemyType(enemyType);
            Console.WriteLine("Sawning new " + enemyType);


            MaxLife = (int) Math.Round(level.WaveNumber * level.DifficultyLevel);
            if (MaxLife < 3)
            {
                MaxLife = 3;
            }

            speed = initialSpeed*level.WaveNumber * level.DifficultyLevel;
            if (speed < initialSpeed)
            {
                speed = initialSpeed;
            }
            this.height = Tile.TILE_SIZE;
            this.width = Tile.TILE_SIZE;
        }

        public override void Initialize()
        {
            if (earthyTexture == null)
            {
                //texture = Game.Content.Load<Texture2D>("enemy");
                textureHealth = new Texture2D(_game.GraphicsDevice, 1,1,false, SurfaceFormat.Color);
                textureHealth.SetData(new[]{Color.White});

                earthyTexture = Game.Content.Load<Texture2D>("grasssprite");
                fieryTexture = Game.Content.Load<Texture2D>("fire");
                frostyTexture = Game.Content.Load<Texture2D>("water");

            }
            this.Life = 3;


            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            GoToNextWaypoint();
            this.position += velocityVector;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int spriteAnimationIndex = (animationIndex / animationFramesByCell) % animationFrameCount;

            Debug.Assert(currentTexture != null, "currentTexture != null");
            spriteBatch.Draw(currentTexture, destinationRectangle: this.GetRect(),sourceRectangle: new Rectangle(spriteAnimationIndex * spriteCellWidth,0,spriteCellWidth,spriteCellHeight),color: Color.White);

            spriteBatch.Draw(textureHealth,new Rectangle((int)position.X-1,(int)position.Z-1, lifeBarWidth, 7),Color.Gray);
            spriteBatch.Draw(textureHealth,new Rectangle((int)position.X,(int)position.Z, lifeBarWidth/MaxLife*Life, 5),Color.Green);

            this.animationIndex++;
        }

        public void OnCollision(ICollider collider)
        {
            var bullet = collider as Bullet;
            if (bullet != null)
            {
                if (this.EnemyType.CompareTo(bullet.towerType)==0)
                {
                    this.Life++;
                }
                else
                {
                    this.Life--;
                }
                if (Life < 1)
                {
                    _level.addMoney(5);
                    _level.score(1);
                    this.Enabled = false;
                    this.enemyReuseQueue.Enqueue(this);
                }
                Console.WriteLine("HIT! Life:" + this.Life);
            }
        }

        public Rectangle GetHitbox()
        {
            return this.GetRect();
        }

        public bool Intersects(ICollider collider)
        {
            var thisBox = this.GetHitbox();
            var thatBox = collider.GetHitbox();
            return thisBox.Intersects(thatBox);
        }

        public void GoToNextWaypoint()
        {

            distanceVector = _level.Waypoints[WaypointIndex % _level.Waypoints.Count].position - this.position;
            if (distanceVector.Length() < speed)
            {
                WaypointIndex++;
                if (WaypointIndex % _level.Waypoints.Count == 0)
                {
                    this.position = _level.Waypoints[0].position;
                    WaypointIndex = 1;
                    _level.loselife(1);
                    this.ChangeEnemyTypeIfEnemyGoesInCircle();
                }
                distanceVector = _level.Waypoints[WaypointIndex % _level.Waypoints.Count].position - this.position;
            }
            distanceVector.Normalize();
            velocityVector = distanceVector * speed;
        }

        public void ChangeEnemyTypeIfEnemyGoesInCircle()
        {
            switch (_enemyType)
            {
                case EnemyType.Frosty:
                    _enemyType = EnemyType.Earthy;
                    break;
                case EnemyType.Fiery:
                    _enemyType = EnemyType.Frosty;
                    break;
                case EnemyType.Earthy:
                    _enemyType = EnemyType.Fiery;
                    break;
                case EnemyType.Normal:
                    _enemyType = (EnemyType)Enum.ToObject(typeof(EnemyType) , _game.random.Next(0,3));
                    break;
            }
        }

        public void setEnemyType(EnemyType enemyType)
        {
            _enemyType = enemyType;

            switch (this._enemyType)
            {
                case EnemyType.Earthy:
                    currentTexture = earthyTexture;
                    this.animationFrameCount = 6;
                    this.animationFramesByCell = 4;
                    this.deathAnimationFrameCount = 7;
                    this.deathAnimationFramesByCell = 4;
                    break;

                case EnemyType.Fiery:
                    currentTexture = fieryTexture;
                    this.animationFrameCount = 10;
                    this.animationFramesByCell = 4;
                    this.deathAnimationFrameCount = 6;
                    this.deathAnimationFramesByCell = 4;
                    break;
                case EnemyType.Frosty:
                    currentTexture = frostyTexture;
                    this.animationFrameCount = 6;
                    this.animationFramesByCell = 4;
                    this.deathAnimationFrameCount = 5;
                    this.deathAnimationFramesByCell = 4;
                    break;
            }
        }
    }
}