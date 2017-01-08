using System;
using System.Collections.Generic;
using System.Collections;
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
        private static Texture2D texture;
        private static Texture2D earthyTexture;
        private static Texture2D fieryTexture;
        private static Texture2D frostyTexture;

        private static Texture2D textureHealth;

        private EnemyType _enemyType;
        public Direction myDirection;
        public int Life { get; private set; }
        public int MaxLife;
        public int LifeWidth = 50;
        public const int defaultSpriteWidth = 50;
        public const int defaultSpriteHeight = 50;
        private float speed;
        private const float initialSpeed = 3;
        private Vector3 distanceVector;
        private int WaypointIndex = 0;
        private Vector3 velocityVector;

        public Enemy(Level level,EnemyType enemyType) : base(level._game, Vector3.Zero)
        {
            _level = level;
            _enemyType = enemyType;
            _game = level._game;

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
            this.height = 32;
            this.width = 32;
            this.Initialize();
        }

        public override void Initialize()
        {
            if (texture == null)
            {
                texture = Game.Content.Load<Texture2D>("enemy");
                textureHealth = new Texture2D(_game.GraphicsDevice, 1,1,false, SurfaceFormat.Color);
                textureHealth.SetData(new[]{Color.White});

                earthyTexture = Game.Content.Load<Texture2D>("grasssprite");


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
            Texture2D currentTexture = texture;
            var spriteFrameHeight = defaultSpriteWidth;
            var spriteFrameWidth = defaultSpriteHeight;
            switch (this._enemyType)
            {
                case EnemyType.Earthy:
                    currentTexture = earthyTexture;
                    spriteFrameHeight = currentTexture.Height / 2;
                    spriteFrameWidth = currentTexture.Width / 8;
                    break;

            }

            spriteBatch.Draw(currentTexture, this.GetRect(),new Rectangle(0,0,spriteFrameWidth,spriteFrameHeight),Color.White);

            spriteBatch.Draw(textureHealth,new Rectangle((int)position.X-1,(int)position.Z-1, LifeWidth, 7),Color.Gray);
            spriteBatch.Draw(textureHealth,new Rectangle((int)position.X,(int)position.Z, LifeWidth/MaxLife*Life, 5),Color.Green);
        }

        public void OnCollision(ICollider collider)
        {
            var bullet = collider as Bullet;
            if (bullet != null)
            {
                this.Life--;
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

                }
                distanceVector = _level.Waypoints[WaypointIndex % _level.Waypoints.Count].position - this.position;
            }
            distanceVector.Normalize();
            velocityVector = distanceVector * speed;
        }

        public void ChangeEnemyTypeIfCircle()
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
                    _enemyType = EnemyType.Earthy;
                    break;
            }
        }
    }
}